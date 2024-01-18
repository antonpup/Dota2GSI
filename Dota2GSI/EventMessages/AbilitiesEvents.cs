using Dota2GSI.Nodes;
using Dota2GSI.Nodes.AbilitiesProvider;
using Dota2GSI.Nodes.Helpers;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Hero Abilities update. 
    /// </summary>
    public class AbilitiesUpdated : UpdateEvent<Abilities>
    {
        public AbilitiesUpdated(Abilities new_value, Abilities previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero Ability Details change.
    /// </summary>
    public class AbilityDetailsChanged : PlayerUpdateEvent<AbilityDetails>
    {
        public AbilityDetailsChanged(AbilityDetails new_value, AbilityDetails previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero Ability addition.
    /// </summary>
    public class AbilityAdded : PlayerValueEvent<Ability>
    {
        public AbilityAdded(Ability value, FullPlayerDetails player) : base(value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero Ability removal.
    /// </summary>
    public class AbilityRemoved : PlayerValueEvent<Ability>
    {
        public AbilityRemoved(Ability value, FullPlayerDetails player) : base(value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero Ability update.
    /// </summary>
    public class AbilityUpdated : PlayerUpdateEvent<Ability>
    {
        public AbilityUpdated(Ability new_value, Ability previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }
}
