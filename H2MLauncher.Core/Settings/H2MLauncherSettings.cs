﻿using H2MLauncher.Core.Models;

namespace H2MLauncher.Core.Settings
{
    public record H2MLauncherSettings
    {
        public string MWRLocation { get; init; } = string.Empty;

        public string IW4MMasterServerUrl { get; init; } = string.Empty;

        public List<SimpleServerInfo> FavouriteServers { get; init; } = [];

        public List<RecentServerInfo> RecentServers { get; init; } = [];

        public ServerFilterSettings ServerFilter { get; init; } = new();

        public bool AutomaticGameDetection { get; init; } = true;

        public bool GameMemoryCommunication { get; init; } = false;

        public bool WatchGameDirectory { get; init; }
    }
}
