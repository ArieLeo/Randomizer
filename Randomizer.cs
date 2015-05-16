using UnityEngine;
using System.Collections;

namespace Randomizer {

    public class Randomizer : MonoBehaviour {

        #region FIELDS

        /// Helper variable.
        ///
        /// In-game time to next trigger.
        /// Used in 'Random' option.
        private float timeToTrigger;

        #endregion

        #region INSPECTOR FIELDS

        [SerializeField]
        private float initDelay;

        [SerializeField]
        private float interval;

        [SerializeField]
        private IntervalTypes intervalType;

        [SerializeField]
        private float minInterval;

        [SerializeField]
        private float maxInterval;
        #endregion

        #region PROPERTIES

        /// <summary>
        /// The class output value. Use it to trigger actions in your game.
        /// </summary>
        // todo rename to State
        public bool Result { get; private set; }

        /// <summary>
        /// Initial delay.
        /// </summary>
        public float InitDelay {
            get { return initDelay; }
            set { initDelay = value; }
        }

        /// <summary>
        /// Time between consequent triggers.
        /// </summary>
        public float Interval {
            get { return interval; }
            set { interval = value; }
        }

        /// <summary>
        /// Interval type.
        /// </summary>
        public IntervalTypes IntervalType {
            get { return intervalType; }
            set { intervalType = value; }
        }

        /// <summary>
        /// Minimum interval.
        /// </summary>
        public float MinInterval {
            get { return minInterval; }
            set { minInterval = value; }
        }

        /// <summary>
        /// Maximum interval.
        /// </summary>
        public float MaxInterval {
            get { return maxInterval; }
            set { maxInterval = value; }
        }

        #endregion

        #region UNITY MESSAGES

        private void Start () {
            // Handle 'Fixed' interval type.
            if (IntervalType == IntervalTypes.Fixed) {
                Invoke("Trigger", InitDelay);
            }
        }

        private void Update () {
            // Handle 'Random' interval option.
            if (IntervalType == IntervalTypes.Random) {
                // Wait random interval before trigger.
                if (Time.time > timeToTrigger) {
                    float interval;

                    // Calculate new random interval.
                    interval = Random.Range(MinInterval, MaxInterval);
                    // Update time to next trigger.
                    timeToTrigger = Time.time + interval;

                    // Trigger.
                    Result = !Result;
                }
            }
        }

        #endregion

        #region METHODS

        /// Method that triggers the '_result' in time intervals.
        // todo rename
        private void Trigger() {
            // Change component state right after initial delay.
            Result = !Result;

            //  Change controller state in fixed intervals.
            //StartCoroutine(Timer.Start(
            //            _interval,
            //            true,
            //            () => { _result = !_result; }
            //            ));

            var triggerResultCoroutine = new Task(TriggerResult());
        }

        // todo rename to ToggleState.
        private IEnumerator TriggerResult() {
            while (true) {
                Result = !Result;
                yield return new WaitForSeconds(Interval);
            }
        }

        #endregion
    }

}
