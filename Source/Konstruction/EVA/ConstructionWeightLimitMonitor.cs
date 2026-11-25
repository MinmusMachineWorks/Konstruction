using UnityEngine;

namespace Konstruction
{
    /// <summary>
    /// Owns the change that <see cref="ModuleKonstructionForeman"/> makes to the global
    /// <see cref="PhysicsGlobals.ConstructionWeightLimit"/>.
    /// </summary>
    /// <remarks>
    /// KSP reads that limit from Physics.cfg once, at startup, and never resets it, so the value is
    /// shared by every kerbal in every scene. This monitor records the stock value before the first
    /// raise and restores it as soon as the raise no longer applies: the active vessel is more than
    /// <see cref="BoostRange"/> from the vessel that raised it, that vessel is unloaded or destroyed,
    /// or the flight scene ends.
    /// </remarks>
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class ConstructionWeightLimitMonitor : MonoBehaviour
    {
        /// <summary>Same radius the Foreman uses to count nearby crew and helpers.</summary>
        public const double BoostRange = 2000d;

        private const float CheckIntervalSeconds = 1f;

        private static double? _stockLimit;
        private static bool _boosted;
        private static Vessel _source;

        private float _nextCheck;

        /// <summary>
        /// The limit KSP had before any Foreman raised it (the current value if none has yet).
        /// </summary>
        public static double StockLimit => _stockLimit ?? PhysicsGlobals.ConstructionWeightLimit;

        /// <summary>
        /// Set the construction weight limit (in newtons) while the active vessel stays near <paramref name="source"/>.
        /// </summary>
        public static void Apply(Vessel source, double limit)
        {
            if (_stockLimit == null)
            {
                _stockLimit = PhysicsGlobals.ConstructionWeightLimit;
            }
            PhysicsGlobals.ConstructionWeightLimit = limit;
            _source = source;
            _boosted = true;
        }

        /// <summary>Put the stock limit back, if a Foreman changed it.</summary>
        public static void Restore()
        {
            if (!_boosted)
            {
                return;
            }
            PhysicsGlobals.ConstructionWeightLimit = StockLimit;
            _source = null;
            _boosted = false;
            Debug.Log($"[KONSTRUCTION] EVA construction weight limit restored to {StockLimit:N1} N");
        }

        private void Update()
        {
            if (!_boosted || Time.unscaledTime < _nextCheck)
            {
                return;
            }
            _nextCheck = Time.unscaledTime + CheckIntervalSeconds;

            var active = FlightGlobals.ActiveVessel;
            // A destroyed Vessel compares equal to null (UnityEngine.Object).
            if (_source == null || !_source.loaded || active == null
                || Vector3d.Distance(active.GetWorldPos3D(), _source.GetWorldPos3D()) > BoostRange)
            {
                Restore();
            }
        }

        private void OnDestroy()
        {
            // Leaving the flight scene, including quickload and revert.
            Restore();
        }
    }
}
