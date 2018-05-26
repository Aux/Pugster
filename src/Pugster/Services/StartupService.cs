﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using NTwitch.Chat;
using NTwitch.Rest;
using System.Reflection;
using System.Threading.Tasks;

namespace Pugster
{
    public class StartupService
    {
        private readonly DiscordSocketClient _discord;
        private readonly TwitchChatClient _twitchBot;
        private readonly TwitchRestClient _twitchChannel;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;

        public StartupService(
            DiscordSocketClient discord,
            TwitchChatClient twitchBot,
            TwitchRestClient twitchChannel,
            CommandService commands,
            IConfiguration config)
        {
            _config = config;
            _discord = discord;
            _twitchBot = twitchBot;
            _twitchChannel = twitchChannel;
            _commands = commands;
        }

        public async Task StartAsync()
        {
            await _discord.LoginAsync(TokenType.Bot, _config["tokens:discord"]);
            await _discord.StartAsync();

            //await _twitchBot.LoginAsync(_config["tokens:twitch_bot"]);
            //await _twitchBot.StartAsync();

            //await _twitchChannel.LoginAsync(_config["tokens:twitch_channel"]);

            _commands.AddTypeReader<Hero>(new HeroTypeReader());
            _commands.AddTypeReader<Profile>(new ProfileTypeReader());
            _commands.AddTypeReader<BattleTag>(new BattleTagTypeReader());
            _commands.AddTypeReader<Lobby>(new LobbyTypeReader());

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }
    }
}
