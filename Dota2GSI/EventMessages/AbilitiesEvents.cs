using Dota2GSI.Nodes;
using Dota2GSI.Nodes.AbilitiesProvider;

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
    /// Event for specific Hero Ability Details change.
    /// </summary>
    public class AbilityDetailsChanged : PlayerUpdateEvent<AbilityDetails>
    {
        public AbilityDetailsChanged(AbilityDetails new_value, AbilityDetails previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific Hero Ability addition.
    /// </summary>
    public class AbilityAdded : PlayerValueEvent<Ability>
    {
        public AbilityAdded(Ability value, int player_id = -1) : base(value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific Hero Ability removal.
    /// </summary>
    public class AbilityRemoved : PlayerValueEvent<Ability>
    {
        public AbilityRemoved(Ability value, int player_id = -1) : base(value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific Hero Ability update.
    /// </summary>
    public class AbilityUpdated : PlayerUpdateEvent<Ability>
    {
        public AbilityUpdated(Ability new_value, Ability previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }
}
