﻿using System.Collections.Generic;

namespace Pugster
{
    public class Lobby
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ulong RoleId { get; set; }

        public List<LobbyPlayer> Players { get; set; }
    }
}
