using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;


/*
 * 有裝的 Nuget
 * Discord.Net
 * Discord.Net.Commands
 * Discord.Net.Core
 * Discord.Net.WebSocket
 * 
 * 
 * 
 */


namespace DiscordSCLDbot215
{
    class Program
    {
        static void Main(string[] args)
       => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        private CommandHandler _Chandler;

        private UserJoinedHandler _UJhandler;


        private async Task StartAsync()
        {

            Console.WriteLine("SCLD 正在登入");

            _client = new DiscordSocketClient();
            await _client.LoginAsync(TokenType.Bot, "AAAAAAA");
            
            await _client.StartAsync();
            //await _client.StopAsync();
            Console.WriteLine("SCLD 登入成功");

            Console.WriteLine("SCLD 開始招待");

            await _client.SetGameAsync("esports.skyey.tw");

            //附加文字擷取
            _Chandler = new CommandHandler(_client);


            _UJhandler = new UserJoinedHandler(_client);


            await Task.Delay(-1);
        }
    }
}
