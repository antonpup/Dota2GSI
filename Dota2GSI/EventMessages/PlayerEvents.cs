using Dota2GSI.Nodes;
using Dota2GSI.Nodes.Helpers;
using Dota2GSI.Nodes.PlayerProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Player update. 
    /// </summary>
    public class PlayerUpdated : UpdateEvent<Player>
    {
        public PlayerUpdated(Player new_value, Player previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Player Details update.
    /// </summary>
    public class PlayerDetailsChanged : PlayerUpdateEvent<PlayerDetails>
    {
        public PlayerDetailsChanged(PlayerDetails new_value, PlayerDetails previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Kills count change.
    /// </summary>
    public class PlayerKillsChanged : PlayerUpdateEvent<int>
    {
        public PlayerKillsChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Deaths count change.
    /// </summary>
    public class PlayerDeathsChanged : PlayerUpdateEvent<int>
    {
        public PlayerDeathsChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Assists count change.
    /// </summary>
    public class PlayerAssistsChanged : PlayerUpdateEvent<int>
    {
        public PlayerAssistsChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Last Hits count change.
    /// </summary>
    public class PlayerLastHitsChanged : PlayerUpdateEvent<int>
    {
        public PlayerLastHitsChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Denies count change.
    /// </summary>
    public class PlayerDeniesChanged : PlayerUpdateEvent<int>
    {
        public PlayerDeniesChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Kill Streak count change.
    /// </summary>
    public class PlayerKillStreakChanged : PlayerUpdateEvent<int>
    {
        public PlayerKillStreakChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Gold count change.
    /// </summary>
    public class PlayerGoldChanged : PlayerUpdateEvent<int>
    {
        public PlayerGoldChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Purchased Wards count change.
    /// </summary>
    public class PlayerWardsPurchasedChanged : PlayerUpdateEvent<int>
    {
        public PlayerWardsPurchasedChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Placed Wards count change.
    /// </summary>
    public class PlayerWardsPlacedChanged : PlayerUpdateEvent<int>
    {
        public PlayerWardsPlacedChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Destroyed Wards count change.
    /// </summary>
    public class PlayerWardsDestroyedChanged : PlayerUpdateEvent<int>
    {
        public PlayerWardsDestroyedChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Runes Activated count change.
    /// </summary>
    public class PlayerRunesActivatedChanged : PlayerUpdateEvent<int>
    {
        public PlayerRunesActivatedChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Stacked Camps count change.
    /// </summary>
    public class PlayerCampsStackedChanged : PlayerUpdateEvent<int>
    {
        public PlayerCampsStackedChanged(int new_value, int previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }
}
