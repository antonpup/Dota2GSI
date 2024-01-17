using Dota2GSI.Nodes.Helpers;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Full Player Details update.
    /// </summary>
    public class FullPlayerDetailsUpdated : UpdateEvent<FullPlayerDetails>
    {
        public FullPlayerDetailsUpdated(FullPlayerDetails new_value, FullPlayerDetails previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for overall Full Team Details update.
    /// </summary>
    public class FullTeamDetailsUpdated : UpdateEvent<FullTeamDetails>
    {
        public FullTeamDetailsUpdated(FullTeamDetails new_value, FullTeamDetails previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Player Disconnected event.
    /// </summary>
    public class PlayerDisconnected : ValueEvent<FullPlayerDetails>
    {
        public PlayerDisconnected(FullPlayerDetails value) : base(value)
        {
        }
    }

    /// <summary>
    /// Event for Player Connected event.
    /// </summary>
    public class PlayerConnected : ValueEvent<FullPlayerDetails>
    {
        public PlayerConnected(FullPlayerDetails value) : base(value)
        {
        }
    }
}
