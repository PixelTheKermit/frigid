using System.Linq;
using Content.Shared.Damage;
using Content.Shared.FixedPoint;
using Content.Shared.MobState.EntitySystems;
using Robust.Shared.GameStates;

namespace Content.Shared.MobState.Components
{
    /// <summary>
    ///     When attached to an <see cref="DamageableComponent"/>,
    ///     this component will handle critical and death behaviors for mobs.
    ///     Additionally, it handles sending effects to clients
    ///     (such as blur effect for unconsciousness) and managing the health HUD.
    /// </summary>
    [RegisterComponent]
    [NetworkedComponent]
    public sealed class MobStateComponent : Component
    {
        /// <summary>
        ///     States that this <see cref="MobStateComponent"/> mapped to
        ///     the amount of damage at which they are triggered.
        ///     A threshold is reached when the total damage of an entity is equal
        ///     to or higher than the int key, but lower than the next threshold.
        ///     Ordered from lowest to highest.
        /// </summary>
        [ViewVariables]
        [DataField("thresholds")]
        public readonly SortedDictionary<int, DamageState> _lowestToHighestStates = new();

        // TODO Remove Nullability?
        [ViewVariables]
        public DamageState? CurrentState { get; set; }

        [ViewVariables]
        public FixedPoint2? CurrentThreshold { get; set; }

        public IEnumerable<KeyValuePair<int, DamageState>> _highestToLowestStates => _lowestToHighestStates.Reverse();

        [Obsolete("Use MobStateSystem")]
        public bool IsAlive()
        {
            return IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<SharedMobStateSystem>()
                .IsAlive(Owner, this);
        }

        [Obsolete("Use MobStateSystem")]
        public bool IsSoftCrit()
        {
            return IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<SharedMobStateSystem>()
                .IsSoftCrit(Owner, this);
        }

        [Obsolete("Use MobStateSystem")]
        public bool IsCritical()
        {
            return IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<SharedMobStateSystem>()
                .IsCritical(Owner, this);
        }

        [Obsolete("Use MobStateSystem")]
        public bool IsDead()
        {
            return IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<SharedMobStateSystem>()
                .IsDead(Owner, this);
        }

        [Obsolete("Use MobStateSystem")]
        public bool IsIncapacitated()
        {
            return IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<SharedMobStateSystem>()
                .IsIncapacitated(Owner, this);
        }
    }
}
