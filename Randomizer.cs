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
        /// Initial delay.
        [SerializeField]
        private float initDelay;

        /// Time between consequent triggers.
        [SerializeField]
        private float interval;

        /// Interval type.
        [SerializeField]
        private IntervalTypes intervalType;

        /// Minimum interval.
        [SerializeField]
        private float minInterval;

        /// Maximum interval.
        [SerializeField]
        private float maxInterval;
        #endregion

        #region PROPERTIES

        /// <summary>
        /// The class output value. Use it to trigger actions in your game.
        /// </summary>
        public bool Result { get; private set; }

        /// Initial delay.
        public float InitDelay {
            get { return initDelay; }
            set { initDelay = value; }
        }

        /// Time between consequent triggers.
        public float Interval {
            get { return interval; }
            set { interval = value; }
        }

        /// Interval type.
        public IntervalTypes IntervalType {
            get { return intervalType; }
            set { intervalType = value; }
        }

        /// Minimum interval.
        public float MinInterval {
            get { return minInterval; }
            set { minInterval = value; }
        }

        /// Maximum interval.
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
            /// Handle 'Random' interval option.
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

        private IEnumerator TriggerResult() {
            while (true) {
                Result = !Result;
                yield return new WaitForSeconds(Interval);
            }
        }

        #endregion
    }

}
