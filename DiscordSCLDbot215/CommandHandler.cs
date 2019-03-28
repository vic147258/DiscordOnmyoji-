using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSCLDbot215
{
    class CommandHandler
    {
        private DiscordSocketClient _cliemt;

        private CommandService _service;

        public CommandHandler(DiscordSocketClient cliemt)
        {
            _cliemt = cliemt;

            _service = new CommandService();

            _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _cliemt.MessageReceived += HandleCommandAsync;


        }


        /// <summary>
        /// 指令事件
        /// </summary>
        /// <param name="s">訊息的物件</param>
        /// <returns></returns>
        private async Task HandleCommandAsync(SocketMessage s)
        {

            SocketUserMessage msg = s as SocketUserMessage;
            if (msg == null) return;


            //if (msg.Author.Id == 511469478391971851 && msg.Content.ToString().Equals("測試成功!!"))
            //    await msg.DeleteAsync();

            if (msg.Author.Id == 511469478391971851) return;  //自己的訊息忽略



            var context = new SocketCommandContext(_cliemt, msg);

            //測試區
            if (msg.Author.Id == 338898570969219076 || msg.Author.Id == 364978943620677635) //vic147258 的ID 還有凱開的
            {
                //await context.Channel.SendMessageAsync(msg.Channel.Id.ToString()); //回傳當前文字頻道的ID


            }





            //用戶設置
            SocketGuildUser the_Author = s.Author as SocketGuildUser;
            IRole roleSuperOP = (the_Author as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Super OP");


            //重複別人的話  Super OP 才能用
            if (the_Author.Roles.Contains(roleSuperOP))
            {
                #region 之前不會寫含空白的資訊的部分
                /*
                if (msg.ToString().IndexOf("!重複 ") == 0)
                {
                    String seandmessage = msg.ToString().Replace("!重複 ", "");

                    if (seandmessage.IndexOf("<#") == 0) //第一個字原是 Discord 自己的判斷字元 所以要檢查第二個字元
                    {
                        //有輸入頻道ID  //目標頻道的ID  -2去掉<#    -3是前面兩個字元 後面還有一個 >
                        ulong target_Channel_id = ulong.Parse(seandmessage.Split(" ")[0].Substring(2, seandmessage.Split(" ")[0].Length - 3));  

                        SocketTextChannel newtextChannel = _cliemt.GetChannel(target_Channel_id) as SocketTextChannel;  //設定頻道

                        seandmessage = seandmessage.Replace(seandmessage.Split(" ")[0] + " ", ""); //把頻道的ID從訊息中移除

                        await newtextChannel.SendMessageAsync(seandmessage);
                        await s.DeleteAsync();
                    }
                    else
                    {
                        //沒輸入頻道ID
                        await context.Channel.SendMessageAsync(seandmessage);
                        await s.DeleteAsync();
                    }
                }
                */

                /*
                if (msg.ToString().IndexOf("!更新歡迎 ") == 0)
                {
                    System.IO.File.WriteAllText("wwww.txt", msg.ToString().Replace("!更新歡迎 ", ""));

                    IUserMessage text_complete = await context.Channel.SendMessageAsync("（已更新歡迎訊息）");
                    await s.DeleteAsync();
                    await Task.Delay(3000);
                    await text_complete.DeleteAsync();

                    //存好以後顯示

                    String textfileall = "以下是您更新後的預覽\n------------------------\n";
                    String[] textfile = System.IO.File.ReadAllLines("wwww.txt");

                    for (int i = 0; i < textfile.Length; i++)
                    {
                        textfileall += textfile[i].Replace("{User}", msg.Author.Mention).Replace("{Guild}", (msg.Author as SocketGuildUser).Guild.Name) + "\n";
                    }

                    await context.Channel.SendMessageAsync(textfileall);

                }
                */
                #endregion
            }



            #region 執行指令 + 打錯指令的回應
            int argPos = 0;
            if (msg.HasCharPrefix('!', ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    //await context.Channel.SendMessageAsync(result.ErrorReason);
                }

                if (!result.IsSuccess)
                {
                    //await context.Channel.SendMessageAsync(result.ErrorReason);
                }

            }
            #endregion

        }
    }
}
