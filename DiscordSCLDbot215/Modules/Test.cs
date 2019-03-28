using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSCLDbot215.Modules
{
    public class Test : ModuleBase<SocketCommandContext>
    {


        [Command("測試2")]
        public async Task textcom343mmmmmd()
        {
            Log_text("測試", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("測試成功!!");

            await Task.Delay(3000);
            await Context.Message.DeleteAsync();
            await text_complete.DeleteAsync();

        }




        [Command("測試歡迎")]
        public async Task werfgergvesgvzredgr()
        {
            Log_text("測試歡迎", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("wwww.txt");

            for (int i = 0; i < textfile.Length; i ++)
            {
                textfileall += textfile[i].Replace("{User}", Context.User.Mention).Replace("{Guild}", Context.Guild.Name) + "\n";
            }

            IUserMessage text_complete = await ReplyAsync(textfileall);



            //await Task.Delay(3000);
            //await text_complete.DeleteAsync();

        }



        private Boolean check_Super_OP(SocketUser the_user)
        {
            IRole role = ((the_user as SocketGuildUser) as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Super OP");
            if ((the_user as SocketGuildUser).Roles.Contains(role))
                return true;
            else
                return false;

        }

        private void Log_text(String com_name, SocketUser loguser)
        {
            //IRole role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Super OP");  //抓到 Super OP 的身分
            Console.WriteLine("收到 " + com_name + " 來自 " + (loguser as SocketGuildUser).Nickname + " (" + loguser.Username + ") (" + loguser.ToString() + ")");
        }
    }
}
