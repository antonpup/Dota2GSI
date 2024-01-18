using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class Dota2EventsInterface : EventsInterface<DotaGameEvent>
    {
        #region AbilitiesEvents

        public delegate void AbilitiesStateUpdatedHandler(AbilitiesUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.AbilitiesUpdated" />
        public event AbilitiesStateUpdatedHandler AbilitiesUpdated = delegate { };

        public delegate void AbilityDetailsChangedHandler(AbilityDetailsChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.AbilityDetailsChanged" />
        public event AbilityDetailsChangedHandler AbilityDetailsChanged = delegate { };

        public delegate void AbilityAddedHandler(AbilityAdded game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.AbilityAdded" />
        public event AbilityAddedHandler AbilityAdded = delegate { };

        public delegate void AbilityRemovedHandler(AbilityRemoved game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.AbilityRemoved" />
        public event AbilityRemovedHandler AbilityRemoved = delegate { };

        public delegate void AbilityUpdatedHandler(AbilityUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.AbilityUpdated" />
        public event AbilityUpdatedHandler AbilityUpdated = delegate { };

        #endregion

        #region AuthEvents

        public delegate void AuthUpdatedHandler(AuthUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.AuthUpdated" />
        public event AuthUpdatedHandler AuthUpdated = delegate { };

        #endregion

        #region BuildingsEvents

        public delegate void BuildingsUpdatedHandler(BuildingsUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.BuildingsUpdated" />
        public event BuildingsUpdatedHandler BuildingsUpdated = delegate { };

        public delegate void BuildingsLayoutUpdatedHandler(BuildingsLayoutUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.BuildingsLayoutUpdated" />
        public event BuildingsLayoutUpdatedHandler BuildingsLayoutUpdated = delegate { };

        public delegate void BuildingUpdatedHandler(BuildingUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.BuildingUpdated" />
        public event BuildingUpdatedHandler BuildingUpdated = delegate { };

        public delegate void TeamBuildingUpdatedHandler(TeamBuildingUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TeamBuildingUpdated" />
        public event TeamBuildingUpdatedHandler TeamBuildingUpdated = delegate { };

        public delegate void TeamBuildingDestroyedHandler(TeamBuildingDestroyed game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TeamBuildingDestroyed" />
        public event TeamBuildingDestroyedHandler TeamBuildingDestroyed = delegate { };

        public delegate void TowerUpdatedHandler(TowerUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TowerUpdated" />
        public event TowerUpdatedHandler TowerUpdated = delegate { };

        public delegate void TowerDestroyedHandler(TowerDestroyed game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TowerDestroyed" />
        public event TowerDestroyedHandler TowerDestroyed = delegate { };

        public delegate void RacksUpdatedHandler(RacksUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.RacksUpdated" />
        public event RacksUpdatedHandler RacksUpdated = delegate { };

        public delegate void RacksDestroyedHandler(RacksDestroyed game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.RacksDestroyed" />
        public event RacksDestroyedHandler RacksDestroyed = delegate { };

        public delegate void AncientUpdatedHandler(AncientUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.AncientUpdated" />
        public event AncientUpdatedHandler AncientUpdated = delegate { };

        public delegate void AncientDestroyedHandler(AncientDestroyed game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.AncientDestroyed" />
        public event AncientDestroyedHandler AncientDestroyed = delegate { };

        #endregion

        #region CouriersEvents

        public delegate void CouriersUpdatedHandler(CouriersUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.CouriersUpdated" />
        public event CouriersUpdatedHandler CouriersUpdated = delegate { };

        public delegate void CourierUpdatedHandler(CourierUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.CourierUpdated" />
        public event CourierUpdatedHandler CourierUpdated = delegate { };

        public delegate void TeamCourierUpdatedHandler(TeamCourierUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TeamCourierUpdated" />
        public event TeamCourierUpdatedHandler TeamCourierUpdated = delegate { };

        #endregion

        #region DraftEvents

        public delegate void DraftUpdatedHandler(DraftUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.DraftUpdated" />
        public event DraftUpdatedHandler DraftUpdated = delegate { };

        public delegate void TeamDraftDetailsUpdatedHandler(TeamDraftDetailsUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TeamDraftDetailsUpdated" />
        public event TeamDraftDetailsUpdatedHandler TeamDraftDetailsUpdated = delegate { };

        #endregion

        #region EventsEvents

        public delegate void EventsUpdatedHandler(EventsUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.EventsUpdated" />
        public event EventsUpdatedHandler EventsUpdated = delegate { };

        public delegate void GameplayEventHandler(GameplayEvent game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.GameplayEvent" />
        public event GameplayEventHandler GameplayEvent = delegate { };

        public delegate void TeamGameplayEventHandler(TeamGameplayEvent game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TeamGameplayEvent" />
        public event TeamGameplayEventHandler TeamGameplayEvent = delegate { };

        public delegate void PlayerGameplayEventHandler(PlayerGameplayEvent game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerGameplayEvent" />
        public event PlayerGameplayEventHandler PlayerGameplayEvent = delegate { };

        #endregion

        #region HeroEvents

        public delegate void HeroUpdatedHandler(HeroUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroUpdated" />
        public event HeroUpdatedHandler HeroUpdated = delegate { };

        public delegate void HeroDetailsChangedHandler(HeroDetailsChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroDetailsChanged" />
        public event HeroDetailsChangedHandler HeroDetailsChanged = delegate { };

        public delegate void HeroLevelChangedHandler(HeroLevelChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroLevelChanged" />
        public event HeroLevelChangedHandler HeroLevelChanged = delegate { };

        public delegate void HeroHealthChangedHandler(HeroHealthChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroHealthChanged" />
        public event HeroHealthChangedHandler HeroHealthChanged = delegate { };

        public delegate void HeroDiedHandler(HeroDied game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroDied" />
        public event HeroDiedHandler HeroDied = delegate { };

        public delegate void HeroRespawnedHandler(HeroRespawned game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroRespawned" />
        public event HeroRespawnedHandler HeroRespawned = delegate { };

        public delegate void HeroTookDamageHandler(HeroTookDamage game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroTookDamage" />
        public event HeroTookDamageHandler HeroTookDamage = delegate { };

        public delegate void HeroManaChangedHandler(HeroManaChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroManaChanged" />
        public event HeroManaChangedHandler HeroManaChanged = delegate { };

        public delegate void HeroStateChangedHandler(HeroStateChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroStateChanged" />
        public event HeroStateChangedHandler HeroStateChanged = delegate { };

        public delegate void HeroMuteStateChangedHandler(HeroMuteStateChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroMuteStateChanged" />
        public event HeroMuteStateChangedHandler HeroMuteStateChanged = delegate { };

        public delegate void HeroSelectedChangedHandler(HeroSelectedChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroSelectedChanged" />
        public event HeroSelectedChangedHandler HeroSelectedChanged = delegate { };

        public delegate void HeroTalentTreeChangedHandler(HeroTalentTreeChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroTalentTreeChanged" />
        public event HeroTalentTreeChangedHandler HeroTalentTreeChanged = delegate { };

        public delegate void HeroAttributesLevelChangedHandler(HeroAttributesLevelChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.HeroAttributesLevelChanged" />
        public event HeroAttributesLevelChangedHandler HeroAttributesLevelChanged = delegate { };

        #endregion

        #region ItemsEvents

        public delegate void ItemsUpdatedHandler(ItemsUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.ItemsUpdated" />
        public event ItemsUpdatedHandler ItemsUpdated = delegate { };

        public delegate void ItemDetailsChangedHandler(ItemDetailsChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.ItemDetailsChanged" />
        public event ItemDetailsChangedHandler ItemDetailsChanged = delegate { };

        public delegate void ItemUpdatedHandler(ItemUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.ItemUpdated" />
        public event ItemUpdatedHandler ItemUpdated = delegate { };

        public delegate void InventoryItemAddedHandler(InventoryItemAdded game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.InventoryItemAdded" />
        public event InventoryItemAddedHandler InventoryItemAdded = delegate { };

        public delegate void InventoryItemRemovedHandler(InventoryItemRemoved game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.InventoryItemRemoved" />
        public event InventoryItemRemovedHandler InventoryItemRemoved = delegate { };

        public delegate void InventoryItemUpdatedHandler(InventoryItemUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.InventoryItemUpdated" />
        public event InventoryItemUpdatedHandler InventoryItemUpdated = delegate { };

        public delegate void StashItemAddedHandler(StashItemAdded game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.StashItemAdded" />
        public event StashItemAddedHandler StashItemAdded = delegate { };

        public delegate void StashItemRemovedHandler(StashItemRemoved game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.StashItemRemoved" />
        public event StashItemRemovedHandler StashItemRemoved = delegate { };

        public delegate void StashItemUpdatedHandler(StashItemUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.StashItemUpdated" />
        public event StashItemUpdatedHandler StashItemUpdated = delegate { };

        #endregion

        #region LeagueEvents

        public delegate void LeagueUpdatedHandler(LeagueUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.LeagueUpdated" />
        public event LeagueUpdatedHandler LeagueUpdated = delegate { };

        #endregion

        #region MapEvents

        public delegate void MapUpdatedHandler(MapUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.MapUpdated" />
        public event MapUpdatedHandler MapUpdated = delegate { };

        public delegate void TimeOfDayChangedHandler(TimeOfDayChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TimeOfDayChanged" />
        public event TimeOfDayChangedHandler TimeOfDayChanged = delegate { };

        public delegate void TeamScoreChangedHandler(TeamScoreChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TeamScoreChanged" />
        public event TeamScoreChangedHandler TeamScoreChanged = delegate { };

        public delegate void GameStateChangedHandler(GameStateChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.GameStateChanged" />
        public event GameStateChangedHandler GameStateChanged = delegate { };

        public delegate void PauseStateChangedHandler(PauseStateChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PauseStateChanged" />
        public event PauseStateChangedHandler PauseStateChanged = delegate { };

        public delegate void GamePausedHandler(GamePaused game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.GamePaused" />
        public event GamePausedHandler GamePaused = delegate { };

        public delegate void GameResumedHandler(GameResumed game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.GameResumed" />
        public event GameResumedHandler GameResumed = delegate { };

        public delegate void TeamVictoryHandler(TeamVictory game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TeamVictory" />
        public event TeamVictoryHandler TeamVictory = delegate { };

        public delegate void TeamDefeatHandler(TeamDefeat game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TeamDefeat" />
        public event TeamDefeatHandler TeamDefeat = delegate { };

        public delegate void RoshanStateChangedHandler(RoshanStateChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.RoshanStateChanged" />
        public event RoshanStateChangedHandler RoshanStateChanged = delegate { };

        #endregion

        #region MinimapEvents

        public delegate void MinimapUpdatedHandler(MinimapUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.MinimapUpdated" />
        public event MinimapUpdatedHandler MinimapUpdated = delegate { };

        public delegate void MinimapElementUpdatedHandler(MinimapElementUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.MinimapElementUpdated" />
        public event MinimapElementUpdatedHandler MinimapElementUpdated = delegate { };

        public delegate void TeamMinimapElementUpdatedHandler(TeamMinimapElementUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.TeamMinimapElementUpdated" />
        public event TeamMinimapElementUpdatedHandler TeamMinimapElementUpdated = delegate { };

        #endregion

        #region NeutralItemsEvents

        public delegate void NeutralItemsUpdatedHandler(NeutralItemsUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.NeutralItemsUpdated" />
        public event NeutralItemsUpdatedHandler NeutralItemsUpdated = delegate { };

        public delegate void TeamNeutralItemsUpdatedHandler(TeamNeutralItemsUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.NeutralItemsUpdated" />
        public event TeamNeutralItemsUpdatedHandler TeamNeutralItemsUpdated = delegate { };

        #endregion

        #region ProviderEvents

        public delegate void PlayerUpdatedHandler(PlayerUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerUpdated" />
        public event PlayerUpdatedHandler PlayerUpdated = delegate { };

        public delegate void PlayerDetailsChangedHandler(PlayerDetailsChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerDetailsChanged" />
        public event PlayerDetailsChangedHandler PlayerDetailsChanged = delegate { };

        public delegate void PlayerKillsChangedHandler(PlayerKillsChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerKillsChanged" />
        public event PlayerKillsChangedHandler PlayerKillsChanged = delegate { };

        public delegate void PlayerDeathsChangedHandler(PlayerDeathsChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerDeathsChanged" />
        public event PlayerDeathsChangedHandler PlayerDeathsChanged = delegate { };

        public delegate void PlayerAssistsChangedHandler(PlayerAssistsChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerAssistsChanged" />
        public event PlayerAssistsChangedHandler PlayerAssistsChanged = delegate { };

        public delegate void PlayerLastHitsChangedHandler(PlayerLastHitsChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerLastHitsChanged" />
        public event PlayerLastHitsChangedHandler PlayerLastHitsChanged = delegate { };

        public delegate void PlayerDeniesChangedHandler(PlayerDeniesChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerLastHitsChanged" />
        public event PlayerDeniesChangedHandler PlayerDeniesChanged = delegate { };

        public delegate void PlayerKillStreakChangedHandler(PlayerKillStreakChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerLastHitsChanged" />
        public event PlayerKillStreakChangedHandler PlayerKillStreakChanged = delegate { };

        public delegate void PlayerGoldChangedHandler(PlayerGoldChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerLastHitsChanged" />
        public event PlayerGoldChangedHandler PlayerGoldChanged = delegate { };

        public delegate void PlayerWardsPurchasedChangedHandler(PlayerWardsPurchasedChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerWardsPurchasedChanged" />
        public event PlayerWardsPurchasedChangedHandler PlayerWardsPurchasedChanged = delegate { };

        public delegate void PlayerWardsPlacedChangedHandler(PlayerWardsPlacedChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerWardsPlacedChanged" />
        public event PlayerWardsPlacedChangedHandler PlayerWardsPlacedChanged = delegate { };

        public delegate void PlayerWardsDestroyedChangedHandler(PlayerWardsDestroyedChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerWardsDestroyedChanged" />
        public event PlayerWardsDestroyedChangedHandler PlayerWardsDestroyedChanged = delegate { };

        public delegate void PlayerRunesActivatedChangedHandler(PlayerRunesActivatedChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerRunesActivatedChanged" />
        public event PlayerRunesActivatedChangedHandler PlayerRunesActivatedChanged = delegate { };

        public delegate void PlayerCampsStackedChangedHandler(PlayerCampsStackedChanged game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerCampsStackedChanged" />
        public event PlayerCampsStackedChangedHandler PlayerCampsStackedChanged = delegate { };

        #endregion

        #region ProviderEvents

        public delegate void ProviderUpdatedHandler(ProviderUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.ProviderUpdated" />
        public event ProviderUpdatedHandler ProviderUpdated = delegate { };

        #endregion

        #region RoshanEvents

        public delegate void RoshanUpdatedHandler(RoshanUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.RoshanUpdated" />
        public event RoshanUpdatedHandler RoshanUpdated = delegate { };

        #endregion

        #region WearablesEvents

        public delegate void WearablesUpdatedHandler(WearablesUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.WearablesUpdated" />
        public event WearablesUpdatedHandler WearablesUpdated = delegate { };

        public delegate void PlayerWearablesUpdatedHandler(PlayerWearablesUpdated game_event);

        /// <inheritdoc cref="Dota2GSI.EventMessages.PlayerWearablesUpdated" />
        public event PlayerWearablesUpdatedHandler PlayerWearablesUpdated = delegate { };

        #endregion

        public Dota2EventsInterface()
        {
        }

        public override void OnNewGameEvent(DotaGameEvent e)
        {
            base.OnNewGameEvent(e);

            if (e is AbilitiesUpdated)
            {
                RaiseEvent(AbilitiesUpdated, e);
            }

            if (e is AbilityDetailsChanged)
            {
                RaiseEvent(AbilityDetailsChanged, e);
            }

            if (e is AbilityAdded)
            {
                RaiseEvent(AbilityAdded, e);
            }

            if (e is AbilityRemoved)
            {
                RaiseEvent(AbilityRemoved, e);
            }

            if (e is AbilityUpdated)
            {
                RaiseEvent(AbilityUpdated, e);
            }

            if (e is AuthUpdated)
            {
                RaiseEvent(AuthUpdated, e);
            }

            if (e is BuildingsUpdated)
            {
                RaiseEvent(BuildingsUpdated, e);
            }

            if (e is BuildingsLayoutUpdated)
            {
                RaiseEvent(BuildingsLayoutUpdated, e);
            }

            if (e is BuildingUpdated)
            {
                RaiseEvent(BuildingUpdated, e);
            }

            if (e is TeamBuildingUpdated)
            {
                RaiseEvent(TeamBuildingUpdated, e);
            }

            if (e is TeamBuildingDestroyed)
            {
                RaiseEvent(TeamBuildingDestroyed, e);
            }

            if (e is TowerUpdated)
            {
                RaiseEvent(TowerUpdated, e);
            }

            if (e is TowerDestroyed)
            {
                RaiseEvent(TowerDestroyed, e);
            }

            if (e is RacksUpdated)
            {
                RaiseEvent(RacksUpdated, e);
            }

            if (e is RacksDestroyed)
            {
                RaiseEvent(RacksDestroyed, e);
            }

            if (e is AncientUpdated)
            {
                RaiseEvent(AncientUpdated, e);
            }

            if (e is AncientDestroyed)
            {
                RaiseEvent(AncientDestroyed, e);
            }

            if (e is CouriersUpdated)
            {
                RaiseEvent(CouriersUpdated, e);
            }

            if (e is CourierUpdated)
            {
                RaiseEvent(CourierUpdated, e);
            }

            if (e is TeamCourierUpdated)
            {
                RaiseEvent(TeamCourierUpdated, e);
            }

            if (e is DraftUpdated)
            {
                RaiseEvent(DraftUpdated, e);
            }

            if (e is TeamDraftDetailsUpdated)
            {
                RaiseEvent(TeamDraftDetailsUpdated, e);
            }

            if (e is EventsUpdated)
            {
                RaiseEvent(EventsUpdated, e);
            }

            if (e is GameplayEvent)
            {
                RaiseEvent(GameplayEvent, e);
            }

            if (e is TeamGameplayEvent)
            {
                RaiseEvent(TeamGameplayEvent, e);
            }

            if (e is PlayerGameplayEvent)
            {
                RaiseEvent(PlayerGameplayEvent, e);
            }

            if (e is HeroUpdated)
            {
                RaiseEvent(HeroUpdated, e);
            }

            if (e is HeroDetailsChanged)
            {
                RaiseEvent(HeroDetailsChanged, e);
            }

            if (e is HeroLevelChanged)
            {
                RaiseEvent(HeroLevelChanged, e);
            }

            if (e is HeroHealthChanged)
            {
                RaiseEvent(HeroHealthChanged, e);
            }

            if (e is HeroDied)
            {
                RaiseEvent(HeroDied, e);
            }

            if (e is HeroRespawned)
            {
                RaiseEvent(HeroRespawned, e);
            }

            if (e is HeroTookDamage)
            {
                RaiseEvent(HeroTookDamage, e);
            }

            if (e is HeroManaChanged)
            {
                RaiseEvent(HeroManaChanged, e);
            }

            if (e is HeroStateChanged)
            {
                RaiseEvent(HeroStateChanged, e);
            }

            if (e is HeroMuteStateChanged)
            {
                RaiseEvent(HeroMuteStateChanged, e);
            }

            if (e is HeroSelectedChanged)
            {
                RaiseEvent(HeroSelectedChanged, e);
            }

            if (e is HeroTalentTreeChanged)
            {
                RaiseEvent(HeroTalentTreeChanged, e);
            }

            if (e is HeroAttributesLevelChanged)
            {
                RaiseEvent(HeroAttributesLevelChanged, e);
            }

            if (e is ItemsUpdated)
            {
                RaiseEvent(ItemsUpdated, e);
            }

            if (e is ItemDetailsChanged)
            {
                RaiseEvent(ItemDetailsChanged, e);
            }

            if (e is ItemUpdated)
            {
                RaiseEvent(ItemUpdated, e);
            }

            if (e is InventoryItemAdded)
            {
                RaiseEvent(InventoryItemAdded, e);
            }

            if (e is InventoryItemRemoved)
            {
                RaiseEvent(InventoryItemRemoved, e);
            }

            if (e is InventoryItemUpdated)
            {
                RaiseEvent(InventoryItemUpdated, e);
            }

            if (e is StashItemAdded)
            {
                RaiseEvent(StashItemAdded, e);
            }

            if (e is StashItemRemoved)
            {
                RaiseEvent(StashItemRemoved, e);
            }

            if (e is StashItemUpdated)
            {
                RaiseEvent(StashItemUpdated, e);
            }

            if (e is LeagueUpdated)
            {
                RaiseEvent(LeagueUpdated, e);
            }

            if (e is MapUpdated)
            {
                RaiseEvent(MapUpdated, e);
            }

            if (e is TimeOfDayChanged)
            {
                RaiseEvent(TimeOfDayChanged, e);
            }

            if (e is TeamScoreChanged)
            {
                RaiseEvent(TeamScoreChanged, e);
            }

            if (e is GameStateChanged)
            {
                RaiseEvent(GameStateChanged, e);
            }

            if (e is PauseStateChanged)
            {
                RaiseEvent(PauseStateChanged, e);
            }

            if (e is GamePaused)
            {
                RaiseEvent(GamePaused, e);
            }

            if (e is GameResumed)
            {
                RaiseEvent(GameResumed, e);
            }

            if (e is TeamVictory)
            {
                RaiseEvent(TeamVictory, e);
            }

            if (e is TeamDefeat)
            {
                RaiseEvent(TeamDefeat, e);
            }

            if (e is RoshanStateChanged)
            {
                RaiseEvent(RoshanStateChanged, e);
            }

            if (e is MinimapUpdated)
            {
                RaiseEvent(MinimapUpdated, e);
            }

            if (e is MinimapElementUpdated)
            {
                RaiseEvent(MinimapElementUpdated, e);
            }

            if (e is TeamMinimapElementUpdated)
            {
                RaiseEvent(TeamMinimapElementUpdated, e);
            }

            if (e is NeutralItemsUpdated)
            {
                RaiseEvent(NeutralItemsUpdated, e);
            }

            if (e is TeamNeutralItemsUpdated)
            {
                RaiseEvent(TeamNeutralItemsUpdated, e);
            }

            if (e is PlayerUpdated)
            {
                RaiseEvent(PlayerUpdated, e);
            }

            if (e is PlayerDetailsChanged)
            {
                RaiseEvent(PlayerDetailsChanged, e);
            }

            if (e is PlayerKillsChanged)
            {
                RaiseEvent(PlayerKillsChanged, e);
            }

            if (e is PlayerDeathsChanged)
            {
                RaiseEvent(PlayerDeathsChanged, e);
            }

            if (e is PlayerAssistsChanged)
            {
                RaiseEvent(PlayerAssistsChanged, e);
            }

            if (e is PlayerLastHitsChanged)
            {
                RaiseEvent(PlayerLastHitsChanged, e);
            }

            if (e is PlayerDeniesChanged)
            {
                RaiseEvent(PlayerDeniesChanged, e);
            }

            if (e is PlayerKillStreakChanged)
            {
                RaiseEvent(PlayerKillStreakChanged, e);
            }

            if (e is PlayerGoldChanged)
            {
                RaiseEvent(PlayerGoldChanged, e);
            }

            if (e is PlayerWardsPurchasedChanged)
            {
                RaiseEvent(PlayerWardsPurchasedChanged, e);
            }

            if (e is PlayerWardsPlacedChanged)
            {
                RaiseEvent(PlayerWardsPlacedChanged, e);
            }

            if (e is PlayerWardsDestroyedChanged)
            {
                RaiseEvent(PlayerWardsDestroyedChanged, e);
            }

            if (e is PlayerRunesActivatedChanged)
            {
                RaiseEvent(PlayerRunesActivatedChanged, e);
            }

            if (e is PlayerCampsStackedChanged)
            {
                RaiseEvent(PlayerCampsStackedChanged, e);
            }

            if (e is ProviderUpdated)
            {
                RaiseEvent(ProviderUpdated, e);
            }

            if (e is RoshanUpdated)
            {
                RaiseEvent(RoshanUpdated, e);
            }

            if (e is WearablesUpdated)
            {
                RaiseEvent(WearablesUpdated, e);
            }

            if (e is PlayerWearablesUpdated)
            {
                RaiseEvent(PlayerWearablesUpdated, e);
            }
        }

    }
}
