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
    public class Command_list : ModuleBase<SocketCommandContext>
    {

        #region 說明系列

        [Command("測試", RunMode = RunMode.Async)]
        public async Task textcommmmmmd()
        {
            Log_text("測試", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("測試成功!!");

            await Task.Delay(10000);
            await Context.Message.DeleteAsync();
            await text_complete.DeleteAsync();

        }

        [Command("說明", RunMode = RunMode.Async)]
        public async Task get_help1() { await get_help(); }
        [Command("幫助", RunMode = RunMode.Async)]
        public async Task get_help3() { await get_help(); }
        [Command("help", RunMode = RunMode.Async)]
        public async Task get_help2() { await get_help(); }
        [Command("?", RunMode = RunMode.Async)]
        public async Task get_help()
        {
            Log_text("顯示指令清單", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            String textallcomm = "";
            /*
            textallcomm += "!說明 | 查詢所有指令\n";
            textallcomm += "!身分 | 查詢您的身分\n";
            textallcomm += "!身分 @<User> | 查詢某使用者的身分\n";
            textallcomm += "!重複 <內容> | 重複您的內容\n";
            textallcomm += "!重複 #<頻道> <內容> | 重複您的內容\n";
            textallcomm += "!歡迎 | 顯示歡迎內容\n";
            textallcomm += "!歡迎 @<User>  | 顯示歡迎某使用者的內容\n";
            textallcomm += "!更新歡迎 <內容> | 更新歡迎內容；{User}=使用者名稱、{Guild}=群名稱\n";
            textallcomm += "!紀錄 <幾條> | 查詢最近的幾條訊息\n";
            textallcomm += "!紀錄 @<User> <幾條> | 查詢某使用者最近的幾條訊息\n";
            textallcomm += "!強調 <訊息ID> | 顯示某個使用者說過的話\n";
            textallcomm += "!強調 #<頻道> <訊息ID> | 顯示在某個頻道的某個使用者說過的話\n";
            textallcomm += "!清除 <幾條> | 刪除最近的幾條訊息\n";
            textallcomm += "!清除 @<User> <幾條> | 刪除某使用者最近的幾條訊息\n";
            textallcomm += "!清除 #<頻道> <幾條> | 刪除某頻道最近的幾條訊息\n";
            textallcomm += "!清除 #<頻道> @<User> <幾條> | 刪除某頻道的某個人的最近的幾條訊息\n";
            textallcomm += "!修改 <文字ID> <內容> | 修改某條訊息\n";
            */
            textallcomm += "請參閱：http://go.skyey.tw/sscld";


            IUserMessage text_complete = await ReplyAsync(textallcomm);
            await Context.Message.DeleteAsync();

            await Task.Delay(10000);
            await text_complete.DeleteAsync();
        }

        #endregion

        #region 重複系列

        //重複 內容
        [Command("重複")]
        public async Task repeat_text([Remainder]String the_text) { await repeat_text(Context.Channel as SocketTextChannel, the_text); }

        //重複 頻道 內容
        [Command("重複")]
        public async Task repeat_text(SocketTextChannel textChannel, [Remainder]String the_text)
        {
            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            Log_text("重複", Context.User);
            await textChannel.SendMessageAsync(the_text);
            await Context.Message.DeleteAsync();
        }

        #endregion

        #region 更新 GTA 的 Line 通知系列

        //更新GTA提醒
        [Command("修改GTA提醒", RunMode = RunMode.Async)]
        public async Task update_GTA_line2([Remainder]String the_text) { await update_welcome_personal(the_text); }
        [Command("更新GTA提醒", RunMode = RunMode.Async)]
        public async Task update_GTA_line([Remainder]String the_text)
        {
            Log_text("更新GTA提醒", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("GTA_to_line.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新GTA的通知訊息）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            await show_GTA_line();
            /*
            String textfileall = "以下是您更新後的預覽 (GTA通知)\n------------------------\n";
            String[] textfile = System.IO.File.ReadAllLines("GTA_to_line.txt");

            for (int i = 0; i < textfile.Length; i++)
            {
                textfileall += textfile[i].Replace("{User}", Context.Message.Author.Mention).Replace("{Guild}", (Context.Message.Author as SocketGuildUser).Guild.Name) + "\n";
            }

            await Context.Channel.SendMessageAsync(textfileall);*/
        }

        //顯示GTA提醒
        [Command("顯示GTA提醒", RunMode = RunMode.Async)]
        public async Task show_GTA_line()
        {
            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            String textfileall = "以下是您更新後的預覽 (GTA通知)\n------------------------\n";
            String[] textfile = System.IO.File.ReadAllLines("GTA_to_line.txt");

            for (int i = 0; i < textfile.Length; i++)
            {
                textfileall += textfile[i].Replace("{User}", Context.Message.Author.Mention).Replace("{Guild}", (Context.Message.Author as SocketGuildUser).Guild.Name) + "\n";
            }

            await Context.Channel.SendMessageAsync(textfileall);
        }

        #endregion

        #region 更新歡迎私訊系列

        //更新歡迎
        [Command("修改歡迎私訊", RunMode = RunMode.Async)]
        public async Task update_welcome_personal2([Remainder]String the_text) { await update_welcome_personal(the_text); }
        [Command("更新歡迎私訊", RunMode = RunMode.Async)]
        public async Task update_welcome_personal([Remainder]String the_text)
        {
            Log_text("更新歡迎私訊", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("wwwwpersonal.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新歡迎私人訊息）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            String textfileall = "以下是您更新後的預覽 (私人訊息)\n------------------------\n";
            String[] textfile = System.IO.File.ReadAllLines("wwwwpersonal.txt");

            for (int i = 0; i < textfile.Length; i++)
            {
                textfileall += textfile[i].Replace("{User}", Context.Message.Author.Mention).Replace("{Guild}", (Context.Message.Author as SocketGuildUser).Guild.Name) + "\n";
            }

            await Context.Channel.SendMessageAsync(textfileall);
        }

        #endregion

        #region 更新 隨機罵人 清單

        //更新隨機罵人
        [Command("修改罵人", RunMode = RunMode.Async)]
        public async Task update_random_fuck2([Remainder]String the_text) { await update_welcome_personal(the_text); }
        [Command("更新罵人", RunMode = RunMode.Async)]
        public async Task update_random_fuck([Remainder]String the_text)
        {
            Log_text("更新罵人清單", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("fuck_list.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新罵人的清單）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            await show_random_fuck();

        }

        //顯示GTA提醒
        [Command("顯示罵人", RunMode = RunMode.Async)]
        public async Task show_random_fuck()
        {
            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            String textfileall = "以下是您更新後的預覽 (罵人清單)\n------------------------\n";
            String[] textfile = System.IO.File.ReadAllLines("fuck_list.txt");

            for (int i = 0; i < textfile.Length; i++)
            {
                textfileall += textfile[i] + "\n";
            }

            await Context.Channel.SendMessageAsync(textfileall);
        }

        #endregion

        #region 更新歡迎系列

        //更新歡迎
        [Command("修改歡迎", RunMode = RunMode.Async)]
        public async Task update_welcome2([Remainder]String the_text) { await update_welcome(the_text); }
        [Command("更新歡迎", RunMode = RunMode.Async)]
        public async Task update_welcome([Remainder]String the_text)
        {
            Log_text("更新歡迎", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("wwww.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新歡迎訊息）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            String textfileall = "以下是您更新後的預覽 (頻道訊息)\n------------------------\n";
            String[] textfile = System.IO.File.ReadAllLines("wwww.txt");

            for (int i = 0; i < textfile.Length; i++)
            {
                textfileall += textfile[i].Replace("{User}", Context.Message.Author.Mention).Replace("{Guild}", (Context.Message.Author as SocketGuildUser).Guild.Name) + "\n";
            }

            await Context.Channel.SendMessageAsync(textfileall);
        }

        #endregion

        #region 歡迎系列

        [Command("歡迎")]
        public async Task get_Welcome() { await get_Welcome(Context.User as SocketGuildUser); }
        [Command("歡迎")]
        public async Task get_Welcome(SocketGuildUser userName)
        {
            Log_text("顯示歡迎", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("wwww.txt");

            for (int i = 0; i < textfile.Length; i++)
            {
                textfileall += textfile[i].Replace("{User}", userName.Mention).Replace("{Guild}", (userName as SocketGuildUser).Guild.Name) + "\n";
            }

            IUserMessage text_complete = await ReplyAsync(textfileall);

            await Context.Message.DeleteAsync();

        }

        #endregion

        #region 強調系列

        //強調
        [Command("強調")]
        public async Task Reference_text(ulong text_id) { await Reference_text(Context.Channel as SocketTextChannel, text_id); }

        //修改 頻道
        [Command("強調")]
        public async Task Reference_text(SocketTextChannel textChannel, ulong text_id)
        {
            Log_text("強調", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            IUserMessage text_message = await textChannel.GetMessageAsync(text_id) as IUserMessage;

            await textChannel.SendMessageAsync(text_message.Author.Mention + " 曾經說過了一句話：\n" + text_message.Content);

            //await ReplyAsync("（已處裡訊息修改）");

        }

        #endregion

        #region 修改系列

        //修改
        [Command("修改")]
        public async Task edit_text(ulong text_id, [Remainder]String the_text) { await edit_text(Context.Channel as SocketTextChannel, text_id, the_text); }

        //修改 頻道
        [Command("修改")]
        public async Task edit_text(SocketTextChannel textChannel, ulong text_id, [Remainder]String the_text)
        {
            Log_text("修改訊息", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;


            IUserMessage text_message = await textChannel.GetMessageAsync(text_id) as IUserMessage;

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription("凱凱好帥");
            await text_message.ModifyAsync(m => { m.Content = the_text; });   //m.Embed = eb.Build();

            await ReplyAsync("（已處裡訊息修改）");

        }

        #endregion

        #region 清除系列

        //清除
        [Command("刪除", RunMode = RunMode.Async)]
        public async Task get_delmessage1() { await get_delmessage(); }
        [Command("清除", RunMode = RunMode.Async)]
        public async Task get_delmessage()
        {
            Log_text("清除", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            IUserMessage text_complete = await ReplyAsync("!清除 <幾行>");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

        }

        //清除 頻道 幾行
        [Command("刪除", RunMode = RunMode.Async)]
        public async Task get_delmessage1(SocketTextChannel textChannel, int line_count) { await get_delmessage(textChannel, line_count); }
        [Command("清除", RunMode = RunMode.Async)]
        public async Task get_delmessage(SocketTextChannel textChannel, int line_count)
        {
            Log_text("清除 帶頻道", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            if (line_count > 1000 && line_count > 0)
            {
                IUserMessage text_temp = await ReplyAsync("（訊息清除失敗！！！！！）\n（最多刪除1000則訊息）");
                await Context.Message.DeleteAsync();
                await Task.Delay(3000);
                await text_temp.DeleteAsync();
                return;
            }

            IEnumerable<IMessage> messages = await textChannel.GetMessagesAsync(line_count).FlattenAsync(); //選定的頻道 不包含自己的訊息 所以沒有+1
            await ((ITextChannel)textChannel).DeleteMessagesAsync(messages);



            //IUserMessage text_complete = await ReplyAsync("（清除了 " + textChannel.Name + " 頻道的 " + (messages.Count()).ToString() + " 個訊息）");
            IUserMessage text_complete = await ReplyAsync("（" + textChannel.Mention + " 刪除 " + (messages.Count()).ToString() + " 個訊息）");

            Console.WriteLine("刪除了 " + textChannel.Name + " 頻道的" + (messages.Count()).ToString() + "個訊息");

            //await Task.Delay(5000);
            //await text_complete.DeleteAsync();
        }

        //清除 幾行
        [Command("刪除", RunMode = RunMode.Async)]
        public async Task get_delmessage1(int line_count) { await get_delmessage(line_count); }
        [Command("清除", RunMode = RunMode.Async)]
        public async Task get_delmessage(int line_count)
        {
            Log_text("清除", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            if (line_count > 1000 && line_count > 0)
            {
                IUserMessage text_temp = await ReplyAsync("（訊息清除失敗！！！！！）\n（最多刪除1000則訊息）");
                await Context.Message.DeleteAsync();
                await Task.Delay(3000);
                await text_temp.DeleteAsync();
                return;
            }

            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(line_count + 1).FlattenAsync();  //+1是指令的那一行
            await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);

            IUserMessage text_complete = await ReplyAsync("（清除了 " + (messages.Count() - 1).ToString() + " 個訊息）");

            Console.WriteLine("刪除了 " + (messages.Count() - 1).ToString() + "個訊息");

            await Task.Delay(3000);
            await text_complete.DeleteAsync();
        }

        //清除 使用者
        [Command("刪除", RunMode = RunMode.Async)]
        public async Task get_delmessage1(SocketGuildUser userName) { await get_delmessage(); }
        [Command("清除", RunMode = RunMode.Async)]
        public async Task get_delmessage(SocketGuildUser userName) { await get_delmessage(); }


        //清除 使用者 幾行
        [Command("刪除", RunMode = RunMode.Async)]
        public async Task get_delmessage1(SocketGuildUser userName, int line_count) { await get_delmessage(userName, line_count); }
        [Command("清除", RunMode = RunMode.Async)]
        public async Task get_delmessage(SocketGuildUser userName, int line_count)
        {
            Log_text("清除", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            if (line_count > 1000 && line_count > 0)
            {
                IUserMessage text_temp = await ReplyAsync("（訊息清除失敗！！！！！）\n（最多刪除1000則訊息）");
                await Context.Message.DeleteAsync();
                await Task.Delay(3000);
                await text_temp.DeleteAsync();
                return;
            }

            await Context.Message.DeleteAsync();  //因為是刪除別人的 所以自己的先刪掉

            //讀取訊息，然後篩選
            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync().FlattenAsync();
            IEnumerable<IMessage> messages2 = messages.Where(m => m.Author == userName).Take(line_count);

            await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages2);

            IUserMessage text_complete = await ReplyAsync("（清除了 " + userName.Nickname + " 的 **" + messages2.Count().ToString() + "** 則訊息：）");

            Console.WriteLine("刪除了 " + userName.Nickname + " 的 " + (messages2.Count()).ToString() + " 個訊息");

            await Task.Delay(3000);
            await text_complete.DeleteAsync();
        }

        //清除 頻道 使用者 幾行
        [Command("刪除", RunMode = RunMode.Async)]
        public async Task get_delmessage1(SocketTextChannel textChannel, SocketGuildUser userName, int line_count) { await get_delmessage(textChannel, userName, line_count); }
        [Command("清除", RunMode = RunMode.Async)]
        public async Task get_delmessage(SocketTextChannel textChannel, SocketGuildUser userName, int line_count)
        {
            Log_text("清除 帶頻道", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            if (line_count > 1000 && line_count > 0)
            {
                IUserMessage text_temp = await ReplyAsync("（訊息清除失敗！！！！！）\n（最多刪除1000則訊息）");
                await Context.Message.DeleteAsync();
                await Task.Delay(3000);
                await text_temp.DeleteAsync();
                return;
            }

            //讀取訊息，然後篩選
            IEnumerable<IMessage> messages = await textChannel.GetMessagesAsync().FlattenAsync();
            IEnumerable<IMessage> messages2 = messages.Where(m => m.Author == userName).Take(line_count);
            await ((ITextChannel)textChannel).DeleteMessagesAsync(messages2);



            //IUserMessage text_complete = await ReplyAsync("（清除了 " + textChannel.Name + " 頻道的 " + (messages.Count()).ToString() + " 個訊息）");
            IUserMessage text_complete = await ReplyAsync("（" + textChannel.Mention + " 刪除 " + userName.Mention + " 的 " + (messages2.Count()).ToString() + " 個訊息）");

            Console.WriteLine("刪除了 " + textChannel.Name + " 頻道的 " + userName.Nickname + " 的 " + (messages.Count()).ToString() + "個訊息");

            //await Task.Delay(5000);
            //await text_complete.DeleteAsync();
        }

        #endregion

        #region 查看系列

        //查看
        [Command("查看", RunMode = RunMode.Async)]
        public async Task get_hhhmessage1() { await get_hhhmessage(); }
        [Command("歷史", RunMode = RunMode.Async)]
        public async Task get_hhhmessage2() { await get_hhhmessage(); }
        [Command("紀錄", RunMode = RunMode.Async)]
        public async Task get_hhhmessage()
        {
            Log_text("查紀錄", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            IUserMessage text_complete = await ReplyAsync("!紀錄 <誰> <幾行>");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

        }

        //查看 使用者
        [Command("查看", RunMode = RunMode.Async)]
        public async Task get_hhhmessage1(SocketGuildUser userName) { await get_hhhmessage(); }
        [Command("歷史", RunMode = RunMode.Async)]
        public async Task get_hhhmessage2(SocketGuildUser userName) { await get_hhhmessage(); }
        [Command("紀錄", RunMode = RunMode.Async)]
        public async Task get_hhhmessage(SocketGuildUser userName) { await get_hhhmessage(); }

        //查看 使用者 行數
        [Command("查看", RunMode = RunMode.Async)]
        public async Task get_hhhmessage1(SocketGuildUser userName, int line_count) { await get_hhhmessage(userName, line_count); }
        [Command("歷史", RunMode = RunMode.Async)]
        public async Task get_hhhmessage2(SocketGuildUser userName, int line_count) { await get_hhhmessage(userName, line_count); }
        [Command("紀錄", RunMode = RunMode.Async)]
        public async Task get_hhhmessage(SocketGuildUser userName, int line_count)
        {
            Log_text("查紀錄", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            if (line_count > 1000 && line_count > 0)
            {
                IUserMessage text_temp = await ReplyAsync("（訊息搜尋失敗！！！！！）\n（最多搜尋1000則訊息）");
                await Context.Message.DeleteAsync();
                await Task.Delay(3000);
                await text_temp.DeleteAsync();
                return;
            }


            await Context.Message.DeleteAsync();  //收到指令後直接刪除

            //讀取訊息，然後篩選
            IEnumerable<IMessage> viewmessages = await Context.Channel.GetMessagesAsync().FlattenAsync();
            IEnumerable<IMessage> viewmessages2 = viewmessages.Where(m => m.Author == userName).Take(line_count);
            IMessage[] mmmviewarr = viewmessages2.ToArray();


            String asdsads = userName.Nickname + "的 **" + mmmviewarr.Length.ToString() + "** 則訊息：\n";
            for (int i = mmmviewarr.Length - 1; i >= 0; i--)
                asdsads += "【" + mmmviewarr[i].CreatedAt.AddHours(8).ToString("MM/dd tt hh:mm:ss") + "】" + (mmmviewarr[i].Author as SocketGuildUser).Nickname + " : " + mmmviewarr[i].Content + "\n";


            IUserMessage text_complete = await ReplyAsync(asdsads);

            Console.WriteLine("查看了 " + (viewmessages2.Count()).ToString() + " 個訊息");
        }

        #endregion

        #region 群組操作系列

        [Command("我的群組")]
        public async Task ergehtrhgerge() { await get_roles(); }
        [Command("群組")]
        public async Task ergehtrhgerge2() { await get_roles(); }
        [Command("身分")]
        public async Task ergehtrhgerge3() { await get_roles(); }
        [Command("我的身分")]
        public async Task get_roles()
        {
            Log_text("我的身分", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            IRole[] role_arr = (Context.User as SocketGuildUser).Roles.ToArray();

            String roletext = Context.User.Mention + " 的身分為：\n";
            for (int i = 0; i < role_arr.Length; i++)
            {
                if (role_arr[i].Name.Equals("@everyone"))
                    continue;
                roletext += role_arr[i].Name + ", id: " + role_arr[i].Id + "\n";
            }

            await Context.Message.DeleteAsync();

            await Context.Channel.SendMessageAsync(roletext);

        }



        [Command("他的群組")]
        public async Task ergehtrhgerge(SocketGuildUser userName) { await get_roles(userName); }
        [Command("群組")]
        public async Task ergehtrhgerge2(SocketGuildUser userName) { await get_roles(userName); }
        [Command("身分")]
        public async Task ergehtrhgerge3(SocketGuildUser userName) { await get_roles(userName); }
        [Command("他的身分")]
        public async Task get_roles(SocketGuildUser userName)
        {
            Log_text("查詢身分", Context.User);


            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            IRole[] role_arr = (userName as SocketGuildUser).Roles.ToArray();

            String roletext = userName.Mention + " 的身分為：\n";
            for (int i = 0; i < role_arr.Length; i++)
            {
                if (role_arr[i].Name.Equals("@everyone"))
                    continue;
                roletext += role_arr[i].Name + ", id: " + role_arr[i].Id + "\n";
            }

            await Context.Message.DeleteAsync();

            await Context.Channel.SendMessageAsync(roletext);

        }




        [Command("新增身分")]
        public async Task qwe_Setrolo(SocketGuildUser userName, SocketRole roleName)
        {
            Log_text("新增身分", Context.User);


            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            await Context.Message.DeleteAsync();

            SocketGuildUser user = Context.User as SocketGuildUser;

            Console.WriteLine("發指令者是 Super OP");
            if (user.GuildPermissions.ManageRoles)
            {
                Console.WriteLine("變更群組");
                await userName.AddRoleAsync(roleName);
            }

        }

        #endregion

        #region 踢人系列

        [Command("踢人")]
        public async Task KickUser(SocketGuildUser userName)
        {
            Log_text("踢人", Context.User);



            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            await Context.Message.DeleteAsync();


            Console.WriteLine("發指令者是 Super OP");
            // Do Stuff
            if ((Context.User as SocketGuildUser).GuildPermissions.KickMembers)
            {
                Console.WriteLine("把人踢掉");
                await userName.KickAsync();
            }

        }

        #endregion

        #region 更新接待設定系列

        [Command("更新接待設定", RunMode = RunMode.Async)]
        public async Task set_auto_role([Remainder]String the_text)
        {
            Log_text("更新接待設定", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("auto_role.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新接待設定）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            String textfileall = "以下是您更新後的預覽 (接待設定)\n------------------------\n";
            String[] textfile = System.IO.File.ReadAllLines("auto_role.txt");

            for (int i = 0; i < textfile.Length; i++)
            {
                textfileall += textfile[i] + "\n";
            }

            await Context.Channel.SendMessageAsync(textfileall);
        }

        #endregion

        #region 重新指向系列

        [Command("加入", RunMode = RunMode.Async)]
        public async Task enter_what(String the_type, SocketGuildUser userName)
        {
            if (!(check_Super_OP(Context.User) || check_GTAOP(Context.User)))  //驗證是不是 Super OP 跟 GTA OP
                return;

            if (the_type.Equals("GTA"))
                await OKGTA(userName);

        }

        [Command("修改", RunMode = RunMode.Async)]
        public async Task update_Onmyoji2(String the_type, [Remainder]String the_text) { await update_Onmyoji(the_type, the_text); }
        [Command("更新", RunMode = RunMode.Async)]
        public async Task update_Onmyoji(String the_type, [Remainder]String the_text)
        {
            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            if (the_type.Equals("SP"))
                await update_Onmyoji_SP(the_text);
            if (the_type.Equals("SSR"))
                await update_Onmyoji_SSR(the_text);
            if (the_type.Equals("SR"))
                await update_Onmyoji_SR(the_text);
            if (the_type.Equals("R"))
                await update_Onmyoji_R(the_text);
            if (the_type.Equals("N"))
                await update_Onmyoji_N(the_text);


        }

        [Command("卡池", RunMode = RunMode.Async)]
        public async Task show_Onmyoji2(String the_type) { await show_Onmyoji(the_type); }
        [Command("顯示", RunMode = RunMode.Async)]
        public async Task show_Onmyoji(String the_type)
        {
            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            if (the_type.Equals("SP"))
                await show_Onmyoji_SP();
            if (the_type.Equals("SSR"))
                await show_Onmyoji_SSR();
            if (the_type.Equals("SR"))
                await show_Onmyoji_SR();
            if (the_type.Equals("R"))
                await show_Onmyoji_R();
            if (the_type.Equals("N"))
                await show_Onmyoji_N();


        }


        #endregion

        #region GTA的OP專用

        [Command("加入GTA", RunMode = RunMode.Async)]
        public async Task OKGTA(SocketGuildUser userName)
        {
            Log_text("加人進GTA", Context.User);


            if (!(check_Super_OP(Context.User) || check_GTAOP(Context.User)))  //驗證是不是 Super OP 跟 GTA OP
                return;


            

            SocketGuildUser user = Context.User as SocketGuildUser;

            /*
           //驗證這個人的群組可不可以變更他人權限
            if (user.GuildPermissions.ManageRoles)
            {
                Console.WriteLine("變更群組");
                //await userName.AddRoleAsync(roleName);
            }
            */
            Console.WriteLine("權限已確認");

            IRole role = user.Guild.Roles.FirstOrDefault(x => x.Id == 509599640824315908);  //俠盜獵車手的身分ID
            IRole role2 = user.Guild.Roles.FirstOrDefault(x => x.Id == 521813093551177738);  //俠盜獵車手(待審核)的身分ID
            await userName.AddRoleAsync(role);
            await userName.RemoveRoleAsync(role2);
            Console.WriteLine("身分已修改");

            String re_message = Context.User.Mention + " 將 " + userName.Mention + " 加入GTA幫會。";

            
            //傳送line
            try
            {
                String sss = "";
                sss += Context.User + " 將 " + userName + " 加入「GTA」幫會。";
                //sss += "Discord 名稱為：" + reaction.User.Value;

                isRock.LineBot.Utility.PushMessage("C7b17951555f37fa81ccc497796b39db4", sss, "7sh6efcRpyWNHThgbpAH9F28vhjys9yLwBPcy5DouzKiREg0vjzrU6ojViIxU6FoloqCa2QRbMvzQ2zzvnIy4NUubxBdEvbNS61yxCVzi5EPK5tSEzbmHUjJRCgDqhF0jkOxDhR63OZ5amB4SuDC4QdB04t89/1O/w1cDnyilFU=");
            }
            catch { }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(re_message);

            //指令執行顯示
            SocketTextChannel channel = user.Guild.GetChannel(515518372532846592) as SocketTextChannel;
            await channel.SendMessageAsync("", false, eb.Build());


            //回應當前的OP
            await ReplyAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();

        }




        #endregion

        #region 召喚系列

        #region 設定卡池


        [Command("更新SP", RunMode = RunMode.Async)]
        public async Task update_Onmyoji_SP([Remainder]String the_text)
        {
            Log_text("更新SP卡池", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("Onmyoji_SP.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新陰陽師卡池-SP）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_SP.txt");

            textfileall = "目前的 " + get_emote("SP") + " 卡池共 " + textfile.Length + " 隻，如下：\n";

            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());
        }

        [Command("更新SSR", RunMode = RunMode.Async)]
        public async Task update_Onmyoji_SSR([Remainder]String the_text)
        {
            Log_text("更新SSR卡池", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("Onmyoji_SSR.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新陰陽師卡池-SSR）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_SSR.txt");

            textfileall = "目前的 " + get_emote("SSR") + " 卡池共 " + textfile.Length + " 隻，如下：\n";

            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());
        }

        [Command("更新SR", RunMode = RunMode.Async)]
        public async Task update_Onmyoji_SR([Remainder]String the_text)
        {
            Log_text("更新SR卡池", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("Onmyoji_SR.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新陰陽師卡池-SR）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_SR.txt");

            textfileall = "目前的 " + get_emote("SR") + " 卡池共 " + textfile.Length + " 隻，如下：\n";

            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());
        }

        [Command("更新R", RunMode = RunMode.Async)]
        public async Task update_Onmyoji_R([Remainder]String the_text)
        {
            Log_text("更新R卡池", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("Onmyoji_R.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新陰陽師卡池-R）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_R.txt");

            textfileall = "目前的 " + get_emote("R") + " 卡池共 " + textfile.Length + " 隻，如下：\n";

            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());
        }

        [Command("更新N", RunMode = RunMode.Async)]
        public async Task update_Onmyoji_N([Remainder]String the_text)
        {
            Log_text("更新N卡池", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            System.IO.File.WriteAllText("Onmyoji_N.txt", the_text);

            IUserMessage text_complete = await Context.Channel.SendMessageAsync("（已更新陰陽師卡池-N）");
            await Context.Message.DeleteAsync();
            await Task.Delay(3000);
            await text_complete.DeleteAsync();

            //存好以後顯示

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_N.txt");

            textfileall = "目前的 " + get_emote("N") + " 卡池共 " + textfile.Length + " 隻，如下：\n";

            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());
        }

        #endregion

        #region 顯示卡池


        [Command("顯示SP", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_SP1() { await show_Onmyoji_SP(); }
        [Command("卡池SP", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_SP()
        {
            Log_text("顯示卡池SP", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_SP.txt");

            textfileall = "目前的 " + get_emote("SP") + " 卡池共 " + textfile.Length + " 隻，如下：\n";

            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());
        }


        [Command("顯示SSR", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_SSR1() { await show_Onmyoji_SSR(); }
        [Command("卡池SSR", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_SSR()
        {
            Log_text("顯示卡池SSR", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;


            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_SSR.txt");

            textfileall = "目前的 " + get_emote("SSR") + " 卡池共 " + textfile.Length + " 隻，如下：\n";

            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());


        }


        [Command("顯示SR", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_SR1() { await show_Onmyoji_SR(); }
        [Command("卡池SR", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_SR()
        {
            Log_text("顯示卡池SR", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_SR.txt");

            textfileall = "目前的 " + get_emote("SR") + " 卡池共 " + textfile.Length + " 隻，如下：\n";

            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());
        }

        [Command("顯示R", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_R1() { await show_Onmyoji_R(); }
        [Command("卡池R", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_R()
        {
            Log_text("顯示卡池R", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_R.txt");

            textfileall = "目前的 " + get_emote("R") + " 卡池共 " + textfile.Length + " 隻，如下：\n";

            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());
        }

        [Command("顯示N", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_N1() { await show_Onmyoji_R(); }
        [Command("卡池N", RunMode = RunMode.Async)]
        public async Task show_Onmyoji_N()
        {
            Log_text("顯示卡池N", Context.User);

            if (!check_Super_OP(Context.User))  //驗證是不是 Super OP
                return;

            String textfileall = "";
            String[] textfile = System.IO.File.ReadAllLines("Onmyoji_N.txt");

            textfileall = "目前的 " + get_emote("N") + " 卡池共 " + textfile.Length + " 隻，如下：\n";


            String em_text = "";
            for (int i = 0; i < textfile.Length; i++)
            {
                if (i != 0 & i % 3 == 0)
                    em_text += "\n";
                else if (i != 0)
                    em_text += "｜";
                em_text += c_space(textfile[i], 4);
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(em_text);

            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync(textfileall, false, eb.Build());
        }



        #endregion

        #region 召喚中

        [Command("召喚", RunMode = RunMode.Async)]
        public async Task get_SSR()
        {
            if (!(check_Super_OP(Context.User) || check_Onmyoji(Context.User)))  //驗證是不是 Super OP 跟 陰陽師
                return;

            //510499589527306250  陰陽師的頻道ID
            if (check_Onmyoji(Context.User) && !check_Super_OP(Context.User))  //只是陰陽師 但是沒有 Super OP
            {
                //限制頻道，不是下面這頻道就跳掉
                if (Context.Message.Channel.Id != 510499589527306250)
                    return;
            }

            //await get_SSR(1);
            String the_message = "";

            the_message += "!召喚 <數量>\n";
            the_message += "    超過10抽只會顯示SP與SSR\n";
            the_message += "!召喚 <數量> 顯示\n";
            the_message += "    顯示包含SR與R卡的式神\n";

            the_message += "請參閱：http://go.skyey.tw/sscld";

            await Context.Message.DeleteAsync();
            await ReplyAsync(the_message);
        }

        [Command("召喚", RunMode = RunMode.Async)]
        public async Task get_SSR(int replay)
        {
            await get_SSR(replay, "");
        }


        [Command("召喚", RunMode = RunMode.Async)]
        public async Task get_SSR(int replay, string is_show)
        {
            Log_text("陰陽師 " + replay.ToString() + " 抽", Context.User);

            #region 權限驗證

            if (!(check_Super_OP(Context.User) || check_Onmyoji(Context.User)))  //驗證是不是 Super OP 跟 陰陽師
                return;

            //510499589527306250  陰陽師的頻道ID
            if (check_Onmyoji(Context.User) && !check_Super_OP(Context.User))  //只是陰陽師 但是沒有 Super OP
            {
                //限制頻道，不是下面這頻道就跳掉
                if (Context.Message.Channel.Id != 510499589527306250)
                    return;
            }

            #endregion

            #region 錯誤排除

            if (replay > 1000)
            {
                await ReplyAsync("請不要貪心，你知道1000抽課金差不多要6萬嗎?");
                return;
            }

            if (replay < 1)
            {
                return;
            }

            #endregion

            String the_message = Context.User.Mention + "\n召喚 " + replay + " 隻式神：\n";

            #region 召喚與文字排版

            Onmyoji_card Onmyoji_pool = new Onmyoji_card();
            Onmyoji_pool.read_card_pool(); //載入卡池，避免抽卡狂讀檔案，所以寫成這樣
            
            if (replay <= 10)
            {
                //****************
                //10抽以下全部顯示
                //****************

                Onmyoji_card[] aaa = new Onmyoji_card[10];

                Console.WriteLine("準備開抽");

                try
                {
                    for (int i = 0; i < replay; i++)
                    {
                        aaa[i] = Onmyoji_pool.new_card(get_rate(Context.User.Id));
                        the_message += get_emote(aaa[i].rarity) + " " + aaa[i].name + "\n";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                
            }
            else 
            {
                //*****************
                //超過10抽 全部整理
                //*****************

                List<Onmyoji_card> card_list = new List<Onmyoji_card>();
                for (int i = 0; i < replay; i++)
                {
                    card_list.Add(Onmyoji_pool.new_card(get_rate(Context.User.Id)));
                }


                //幫不同稀有度的是神分類
                IEnumerable<Onmyoji_card> card_SP = card_list.Where(x => x.rarity == "SP");
                IEnumerable<Onmyoji_card> card_SSR = card_list.Where(x => x.rarity == "SSR");
                IEnumerable<Onmyoji_card> card_SR = card_list.Where(x => x.rarity == "SR");
                IEnumerable<Onmyoji_card> card_R = card_list.Where(x => x.rarity == "R");


                


                //顯示不同稀有度的式神有幾隻

                the_message += get_emote("SP") + " " + card_SP.Count().ToString() + " 隻\n";
                the_message += get_emote("SSR") + " " + card_SSR.Count().ToString() + " 隻\n";
                the_message += get_emote("SR") + " " + card_SR.Count().ToString() + " 隻\n";
                the_message += get_emote("R") + " " + card_R.Count().ToString() + " 隻\n";


                //顯示 SP 的數量
                if (card_SP.Count() > 0)
                {
                    IEnumerable<IGrouping<string, Onmyoji_card>> result = card_SP.GroupBy(x => x.name);
                    the_message += "\nSP 有 " + result.Count() + " 種：\n";
                    result = result.OrderByDescending(x => x.Count()); //排序
                    String temp_card_list = "";
                    foreach (IGrouping<string, Onmyoji_card> group in result)
                    {
                        int count = 0;
                        foreach (Onmyoji_card qwefqwefqwefsdfasdfefqewf in group)
                            count++;
                        temp_card_list += get_emote("SP") + " " + group.Key + " × " + count + "\n";
                    }
                    the_message += temp_card_list;

                    //result = null;
                    //card_SP = null;
                }

                //顯示 SSR 的數量
                if (card_SSR.Count() > 0)
                {
                    IEnumerable<IGrouping<string, Onmyoji_card>> result = card_SSR.GroupBy(x => x.name);
                    the_message += "\nSSR 有 " + result.Count() + " 種：\n";
                    result = result.OrderByDescending(x => x.Count()); //排序
                    String temp_card_list = "";
                    foreach (IGrouping<string, Onmyoji_card> group in result)
                    {
                        int count = 0;
                        foreach (Onmyoji_card qwefqwefqwefqwefsdfasdfasdf in group)
                            count++;
                        temp_card_list += get_emote("SSR") + " " + group.Key + " × " + count + "\n";
                    }
                    the_message += temp_card_list;

                    //result = null;
                    //card_SSR = null;
                }

                //有條件的顯示
                if (is_show.Equals("顯示"))
                {
                    //顯示 SR 的數量
                    if (card_SR.Count() > 0)
                    {
                        IEnumerable<IGrouping<string, Onmyoji_card>> result = card_SR.GroupBy(x => x.name);
                        the_message += "\n" + get_emote("SR") + " 有 " + result.Count() + " 種：\n";
                        result = result.OrderByDescending(x => x.Count()); //排序
                        String temp_card_list = "";
                        foreach (IGrouping<string, Onmyoji_card> group in result)
                        {
                            int count = 0;
                            foreach (Onmyoji_card ergrwergwergwergwergwregqergqewf in group)
                                count++;
                            temp_card_list += group.Key + " × " + count + "\n";
                        }
                        the_message += temp_card_list;

                        //result = null;
                        //card_SR = null;
                    }

                    //顯示 R 的數量
                    if (card_R.Count() > 0)
                    {
                        IEnumerable<IGrouping<string, Onmyoji_card>> result = card_R.GroupBy(x => x.name);
                        the_message += "\n" + get_emote("R") + " 有 " + result.Count() + " 種：\n";
                        result = result.OrderByDescending(x => x.Count()); //排序
                        String temp_card_list = "";
                        foreach (IGrouping<string, Onmyoji_card> group in result)
                        {
                            int count = 0;
                            foreach (Onmyoji_card wefewfegretherhewrtgwergeqr in group)
                                count++;
                            temp_card_list += group.Key + " × " + count + "\n";
                        }
                        the_message += temp_card_list;

                        //result = null;
                        //card_R = null;

                    }
                }
                //card_list = null;
                //card_list.Clear();
               
            }

            #endregion



            await Context.Message.DeleteAsync();


            EmbedBuilder eb = new EmbedBuilder();
            eb.WithDescription(the_message);

            await ReplyAsync("", false, eb.Build());


            //GC.Collect(); //手動清除記憶體的廢物

            //await ReplyAsync(the_message);
        }

        #endregion

        #region 找伺服器的 Emoji ， 機率偷改 ， 名稱補空白

        String c_space(String the_text,int len)
        {
            if (the_text.Length < len)
            {
                int lost_count = len - the_text.Length;
                for (int i = 0; i < lost_count; i++)
                {
                    the_text += "　";
                }
            }

            return the_text;
        }


        /// <summary>
        /// 找伺服器的 Emoji
        /// </summary>
        /// <param name="the_text">圖案名稱</param>
        /// <returns></returns>
        GuildEmote get_emote(String the_text)
        {
            if (the_text.Equals("R"))
                the_text = "Rc";
            if (the_text.Equals("N"))
                the_text = "Nc";
            return Context.Guild.Emotes.First(e => e.Name == the_text);
        }

        /// <summary>
        /// 機率偷改
        /// </summary>
        /// <param name="the_user">使用者ID</param>
        /// <returns></returns>
        double get_rate(ulong the_user)
        {
            //464807640346918942  多多綠
            //338898570969219076  我自己
            //364978943620677635  凱凱

            List<ulong> double_user = new List<ulong>();
            List<ulong> Reduce_user = new List<ulong>();

            double_user.Add(464807640346918942);  //多多綠
            //double_user.Add(364978943620677635);  //凱凱


            //Reduce_user.Add(338898570969219076);  //我自己

            if (double_user.Where(x => x == the_user).Count() > 0)
                return 2.5;

            if (Reduce_user.Where(x => x == the_user).Count() > 0)
                return 0.5;

            return 1;
        }

        #endregion

        #endregion

        #region 驗證身分 寫log 到小黑窗

        private Boolean check_Super_OP(SocketUser the_user)
        {
            IRole role = ((the_user as SocketGuildUser) as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Id == 509050587242037258);  //x.Name == "Super OP"
            //Console.WriteLine("群組" + role.Name + "，ID：" + role.Id);
            if ((the_user as SocketGuildUser).Roles.Contains(role))
                return true;
            else
                return false;
        }

        private Boolean check_Onmyoji(SocketUser the_user)
        {
            IRole role = ((the_user as SocketGuildUser) as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Id == 510493763232202752);  //x.Name == "🎮陰陽師"
            //Console.WriteLine(role.Name);
            if ((the_user as SocketGuildUser).Roles.Contains(role))
                return true;
            else
                return false;
        }

        private Boolean check_GTAOP(SocketUser the_user)
        {
            IRole role = ((the_user as SocketGuildUser) as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Id == 509052082217549831);  //x.Name == "👑俠盜獵車手 OP"
            //Console.WriteLine(role.Name);
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

        #endregion

    }
}
