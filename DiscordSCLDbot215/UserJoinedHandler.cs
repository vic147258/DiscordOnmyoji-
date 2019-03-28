using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DiscordSCLDbot215.Modules;

namespace DiscordSCLDbot215
{
    class UserJoinedHandler
    {

        private DiscordSocketClient _cliemt;


        public UserJoinedHandler(DiscordSocketClient cliemt)
        {
            _cliemt = cliemt;

            _cliemt.UserJoined += AnnounceJoinedUser;

            //_cliemt.MessageUpdated += MessageUpdated; //訊息修改

            _cliemt.ReactionAdded += _cliemt_ReactionAdded;  //點讚

            _cliemt.ReactionRemoved += _cliemt_ReactionRemoved;  //移除讚

        }


        #region 判斷表情符號系列

        #region 新增系列

        /// <summary>
        /// 新增系列
        /// </summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <param name="reaction"></param>
        /// <returns></returns>
        private async Task _cliemt_ReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            /*
            :video_game:陰陽師, id: 510493763232202752
            :video_game:英雄聯盟, id: 510664965720637440
            :video_game:創世神, id: 510665083995553798
            🧢觀光客, id: 509434019520184327
            :heavy_check_mark:️, id: 523870195928924170
            :video_game:俠盜獵車手, id: 509599640824315908
            :beginner:俠盜獵車手(待審核), id: 521813093551177738
            */



            String[] textfile = System.IO.File.ReadAllLines("auto_role.txt");


            SocketGuild the_Guild = (channel as SocketGuildChannel).Guild;



            //開始處裡文件
            foreach (String the_line in textfile)
            {
                String[] FFF = the_line.Split(",");
                //判斷是按下的事件才處裡
                if (FFF[2].Equals("on"))
                {
                    //指定句子才判斷
                    if (message.Id == ulong.Parse(FFF[0]))
                    {
                        //如果 不是-號 就是有例外身分處裡
                        if (!FFF[5].Equals("-"))
                        {
                            IRole role = ((reaction.User.Value as SocketGuildUser) as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Id == ulong.Parse(FFF[5]));
                            if (!(reaction.User.Value as SocketGuildUser).Roles.Contains(role))  //如果已經是某個身分 就不再處裡
                            {
                                if (is_emoji_pair(reaction, FFF[1]))
                                {
                                    if (FFF[3].Equals("add"))
                                    {
                                        await add_role(the_Guild, reaction, ulong.Parse(FFF[4]));
                                        if (FFF[6].Equals("line"))
                                            Send_line(reaction);
                                    }
                                    if (FFF[3].Equals("remove"))
                                    {
                                        await remove_role(the_Guild, reaction, ulong.Parse(FFF[4]));
                                        if (FFF[6].Equals("line"))
                                            Send_line(reaction);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (is_emoji_pair(reaction, FFF[1]))
                            {
                                if (FFF[3].Equals("add"))
                                {
                                    await add_role(the_Guild, reaction, ulong.Parse(FFF[4]));
                                    if (FFF[6].Equals("line"))
                                        Send_line(reaction);
                                }
                                if (FFF[3].Equals("remove"))
                                {
                                    await remove_role(the_Guild, reaction, ulong.Parse(FFF[4]));
                                    if (FFF[6].Equals("line"))
                                        Send_line(reaction);
                                }
                            }
                        }
                    }
                }
            }



            /*

            //指令執行的測試句子
            if (message.Id == 531147022653325313)
            {
                //await set_role(the_Guild, reaction.User.Value as SocketUser, reaction.Emote, "Onmyouji", 510493763232202752);  //陰陽師圖示

                if (is_emoji_pair(reaction, "Onmyouji"))
                    await add_role(the_Guild, reaction, 510493763232202752);
                

                if (is_emoji_pair(reaction, "League_of_Legends"))
                    await add_role(the_Guild, reaction, 510664965720637440);
                

            }


            //新人佈告欄的選 O X 訊息
            if (message.Id == 524065758435737610)
            {
                if (is_emoji_pair(reaction, "⭕"))
                {
                    await remove_role(the_Guild, reaction, 509434019520184327);  //移除觀光客
                    await add_role(the_Guild, reaction, 523870195928924170); //增加打勾
                }
            }


            //群組選擇局 的 陰陽師 LOL  GTA 訊息
            if (message.Id == 524064825517539340)
            {
                if (is_emoji_pair(reaction, "Onmyouji"))
                    await add_role(the_Guild, reaction, 510493763232202752);

                if (is_emoji_pair(reaction, "League_of_Legends"))
                    await add_role(the_Guild, reaction, 510664965720637440);


                IRole role = ((reaction.User.Value as SocketGuildUser) as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Id == 509599640824315908);
                if (!(reaction.User.Value as SocketGuildUser).Roles.Contains(role))  //如果已經是俠盜列車手的 就不再處裡
                    if (is_emoji_pair(reaction, "GTA"))
                    {
                        await add_role(the_Guild, reaction, 521813093551177738);
                        try
                        {
                            String sss = "";
                            sss += "有新玩家已經到了 Discord 頻道\n";
                            sss += "等待各位的審核\n";
                            sss += "Discord 名稱為：" + reaction.User.Value;

                            isRock.LineBot.Utility.PushMessage("C7b17951555f37fa81ccc497796b39db4", sss, "7sh6efcRpyWNHThgbpAH9F28vhjys9yLwBPcy5DouzKiREg0vjzrU6ojViIxU6FoloqCa2QRbMvzQ2zzvnIy4NUubxBdEvbNS61yxCVzi5EPK5tSEzbmHUjJRCgDqhF0jkOxDhR63OZ5amB4SuDC4QdB04t89/1O/w1cDnyilFU=");
                        }
                        catch { }
                    }
            }
            

            */

        }

        #endregion

        #region 移除系列

        private async Task _cliemt_ReactionRemoved(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            /*
            :video_game:陰陽師, id: 510493763232202752
            :video_game:英雄聯盟, id: 510664965720637440
            :video_game:創世神, id: 510665083995553798
            🧢觀光客, id: 509434019520184327
            :heavy_check_mark:️, id: 523870195928924170
            :video_game:俠盜獵車手, id: 509599640824315908
            :beginner:俠盜獵車手(待審核), id: 521813093551177738
            */

            String[] textfile = System.IO.File.ReadAllLines("auto_role.txt");

            SocketGuild the_Guild = (channel as SocketGuildChannel).Guild;


            //開始處裡文件
            foreach (String the_line in textfile)
            {
                String[] FFF = the_line.Split(",");
                //判斷是按下的事件才處裡
                if (FFF[2].Equals("off"))
                {
                    //指定句子才判斷
                    if (message.Id == ulong.Parse(FFF[0]))
                    {
                        //如果 不是-號 就是有例外身分處裡
                        if (!FFF[5].Equals("-"))
                        {
                            IRole role = ((reaction.User.Value as SocketGuildUser) as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Id == ulong.Parse(FFF[5]));
                            if (!(reaction.User.Value as SocketGuildUser).Roles.Contains(role))  //如果已經是某個身分 就不再處裡
                            {
                                if (is_emoji_pair(reaction, FFF[1]))
                                {
                                    if (FFF[3].Equals("add"))
                                    {
                                        await add_role(the_Guild, reaction, ulong.Parse(FFF[4]));
                                        if (FFF[6].Equals("line"))
                                            Send_line(reaction);
                                    }
                                    if (FFF[3].Equals("remove"))
                                    {
                                        await remove_role(the_Guild, reaction, ulong.Parse(FFF[4]));
                                        if (FFF[6].Equals("line"))
                                            Send_line(reaction);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (is_emoji_pair(reaction, FFF[1]))
                            {
                                if (FFF[3].Equals("add"))
                                {
                                    await add_role(the_Guild, reaction, ulong.Parse(FFF[4]));
                                    if (FFF[6].Equals("line"))
                                        Send_line(reaction);
                                }
                                if (FFF[3].Equals("remove"))
                                {
                                    await remove_role(the_Guild, reaction, ulong.Parse(FFF[4]));
                                    if (FFF[6].Equals("line"))
                                        Send_line(reaction);
                                }
                            }
                        }
                    }
                }
            }


            /*
            //指令執行的測試句子
            if (message.Id == 531147022653325313)
            {

                if (is_emoji_pair(reaction, "Onmyouji"))
                    await remove_role(the_Guild, reaction, 510493763232202752);


                if (is_emoji_pair(reaction, "League_of_Legends"))
                    await remove_role(the_Guild, reaction, 510664965720637440);
            }


            //群組選擇局 的 陰陽師 LOL  GTA 訊息
            if (message.Id == 524064825517539340)
            {
                if (is_emoji_pair(reaction, "Onmyouji"))
                    await remove_role(the_Guild, reaction, 510493763232202752);

                if (is_emoji_pair(reaction, "League_of_Legends"))
                    await remove_role(the_Guild, reaction, 510664965720637440);


                IRole role = ((reaction.User.Value as SocketGuildUser) as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Id == 509599640824315908);
                if (!(reaction.User.Value as SocketGuildUser).Roles.Contains(role))  //如果已經是俠盜列車手的 就不再處裡
                    if (is_emoji_pair(reaction, "GTA"))
                        await remove_role(the_Guild, reaction, 521813093551177738);
                    
            }
            */

        }

        #endregion

        #region 判斷 移除權限 增加權限 傳line

        Boolean is_emoji_pair(SocketReaction reaction, String emoli_text)
        {
            if (reaction.Emote.Name == emoli_text)
                return true;
            else
                return false;
        }

        async Task add_role(SocketGuild sg, SocketReaction reaction, ulong role_id)
        {
            IRole role = sg.Roles.FirstOrDefault(x => x.Id == role_id);
            await (reaction.User.Value as IGuildUser).AddRoleAsync(role);

            Console.WriteLine($"{reaction.User.Value} 增加了 {role.Name} 權限"); //小黑窗
        }

        async Task remove_role(SocketGuild sg, SocketReaction reaction, ulong role_id)
        {
            IRole role = sg.Roles.FirstOrDefault(x => x.Id == role_id);
            await (reaction.User.Value as IGuildUser).RemoveRoleAsync(role);

            Console.WriteLine($"{reaction.User.Value} 移除了 {role.Name} 權限"); //小黑窗
        }

        void Send_line(SocketReaction reaction)
        {
            try
            {
                /*
                String sss = "";
                sss += "有新玩家選擇了「GTA」請立即處裡\n";
                sss += "Discord 名稱為：" + reaction.User.Value;
                */
                String textfileall = "";
                String[] textfile = System.IO.File.ReadAllLines("GTA_to_line.txt");

                Random rnd;
                rnd = new Random(Guid.NewGuid().GetHashCode());
                String[] fuck_text =  System.IO.File.ReadAllLines("fuck_list.txt");
                


                for (int i = 0; i < textfile.Length; i++)
                {
                    textfileall += textfile[i].Replace("{User}", reaction.User.Value.ToString()).Replace("{Guild}", (reaction.User.Value as SocketGuildUser).Guild.Name) + "\n";

                }

                int noran = 0;
                while (textfileall.IndexOf("{罵人}") >= 0)
                {
                    int idx = textfileall.IndexOf("{罵人}");
                    textfileall = textfileall.Remove(idx, 4);
                    textfileall = textfileall.Insert(idx, fuck_text[(int)(rnd.NextDouble() * fuck_text.Length)]);
                }

                //C7b17951555f37fa81ccc497796b39db4   //招生群組
                //U5d8a651e5fa44976ac212c02d4273b01   //vic的line
                isRock.LineBot.Utility.PushMessage("C7b17951555f37fa81ccc497796b39db4", textfileall, "7sh6efcRpyWNHThgbpAH9F28vhjys9yLwBPcy5DouzKiREg0vjzrU6ojViIxU6FoloqCa2QRbMvzQ2zzvnIy4NUubxBdEvbNS61yxCVzi5EPK5tSEzbmHUjJRCgDqhF0jkOxDhR63OZ5amB4SuDC4QdB04t89/1O/w1cDnyilFU=");
            }
            catch { }
        }

        #endregion

        #endregion

        #region 判斷有人修改自己文章系列

        /// <summary>
        /// 有人修改自己貼文時
        /// </summary>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            //515518372532846592  指令執行  頻道
            //531147245702348801  要測試的那段文字


            Console.WriteLine("有人修改");

            if (channel.Id != 515518372532846592)
                return;

            //if (after.Id != 531147022653325313)
            //   return;




            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }

        #endregion

        #region 新進人員歡迎

        /// <summary>
        /// 新玩家進入的事件
        /// </summary>
        /// <param name="user">近來的使用者</param>
        /// <returns></returns>
        private async Task AnnounceJoinedUser(SocketGuildUser user)
        {

            Console.WriteLine("新玩家進入頻道");

            var channel = _cliemt.GetChannel(510783985689821187) as SocketTextChannel;
            //var channel = _cliemt.GetChannel(user.Guild.Id) as SocketTextChannel;

            IRole role = user.Guild.Roles.FirstOrDefault(x => x.Id == 509434019520184327);
            await (user as IGuildUser).AddRoleAsync(role);

            Console.WriteLine("給新玩家觀光客權限");


            //讀取歡迎檔案的內文
            String Welcome_word = "";
            String[] textfile = System.IO.File.ReadAllLines("wwww.txt");
            for (int i = 0; i < textfile.Length; i++)
                Welcome_word += textfile[i].Replace("{User}", user.Mention).Replace("{Guild}", channel.Guild.Name) + "\n";

            //讀取歡迎檔案的內文
            String Welcome_word_p = "";
            String[] textfile2 = System.IO.File.ReadAllLines("wwwwpersonal.txt");
            for (int i = 0; i < textfile2.Length; i++)
                Welcome_word_p += textfile2[i].Replace("{User}", user.Mention).Replace("{Guild}", channel.Guild.Name) + "\n";




            //只有加入SCLD才會跳訊息
            if (user.Guild.Id == 509026245519081498)
                await user.SendMessageAsync(Welcome_word_p); //傳送給使用者訊息


            //系統問題 剛改群組是看不到訊息的 所以等待40秒才傳送
            int waitcount = 2;
            for (int i = 0; i < waitcount; i++)
                await Task.Delay(1000);




            //只有加入SCLD才會跳訊息
            if (user.Guild.Id == 509026245519081498)
            {
                await channel.SendMessageAsync(Welcome_word);//直接在頻道傳送訊息
                Console.WriteLine("送出了招待訊息");
            }


        }

        #endregion

    }
}
