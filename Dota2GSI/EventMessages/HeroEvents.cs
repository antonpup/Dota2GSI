using Dota2GSI.Nodes;
using Dota2GSI.Nodes.HeroProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Hero update. 
    /// </summary>
    public class HeroUpdated : UpdateEvent<Hero>
    {
        public HeroUpdated(Hero new_value, Hero previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero Details update.
    /// </summary>
    public class HeroDetailsChanged : PlayerUpdateEvent<HeroDetails>
    {
        public HeroDetailsChanged(HeroDetails new_value, HeroDetails previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero level update.
    /// </summary>
    public class HeroLevelChanged : PlayerUpdateEvent<int>
    {
        public HeroLevelChanged(int new_value, int previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero health update.
    /// </summary>
    public class HeroHealthChanged : PlayerUpdateEvent<int>
    {
        public HeroHealthChanged(int new_value, int previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero death.
    /// </summary>
    public class HeroDied : HeroHealthChanged
    {
        public HeroDied(int new_value, int previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero respawn.
    /// </summary>
    public class HeroRespawned : HeroHealthChanged
    {
        public HeroRespawned(int new_value, int previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero taking damage.
    /// </summary>
    public class HeroTookDamage : HeroHealthChanged
    {
        public HeroTookDamage(int new_value, int previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero mana update.
    /// </summary>
    public class HeroManaChanged : PlayerUpdateEvent<int>
    {
        public HeroManaChanged(int new_value, int previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero state update.
    /// </summary>
    public class HeroStateChanged : PlayerUpdateEvent<HeroState>
    {
        public HeroStateChanged(HeroState new_value, HeroState previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero mute update.
    /// </summary>
    public class HeroMuteStateChanged : PlayerUpdateEvent<bool>
    {
        public HeroMuteStateChanged(bool new_value, bool previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero selection.
    /// </summary>
    public class HeroSelectedChanged : PlayerUpdateEvent<bool>
    {
        public HeroSelectedChanged(bool new_value, bool previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero Talent Tree update.
    /// </summary>
    public class HeroTalentTreeChanged : PlayerUpdateEvent<TalentTreeSpec[]>
    {
        public HeroTalentTreeChanged(TalentTreeSpec[] new_value, TalentTreeSpec[] previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Hero Attributes Level update.
    /// </summary>
    public class HeroAttributesLevelChanged : PlayerUpdateEvent<int>
    {
        public HeroAttributesLevelChanged(int new_value, int previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }
}
