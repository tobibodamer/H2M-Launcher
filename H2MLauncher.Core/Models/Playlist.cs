﻿namespace H2MLauncher.Core.Models
{
    public record Playlist
    {
        private int _serverCount = 0;

        public required string Id { get; init; }

        public required string Name { get; init; }

        public string? Description { get; init; }

        public List<string> GameModes { get; init; } = [];

        public List<string> MapPacks { get; init; } = [];

        public List<string>? Servers { get; init; } = [];
        
        public int ServerCount
        {
            get => Servers?.Count ?? _serverCount;
            set => _serverCount = ServerCount;
        }
    }
}
