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

        /// Result that the component returns.
        ///
        /// This property can be read by another component in order to execute
        /// action at a random intervals.
        private bool result;

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
        public bool Result {
            get { return result; }
        }

        #endregion

        #region UNITY MESSAGES

        private void Start () {
            // Handle 'Fixed' interval type.
            if (intervalType == IntervalTypes.Fixed) {
                Invoke("Trigger", initDelay);
            }
        }

        private void Update () {
            /// Handle 'Random' interval option.
            if (intervalType == IntervalTypes.Random) {
                // Wait random interval before trigger.
                if (Time.time > timeToTrigger) {
                    float interval;

                    // Calculate new random interval.
                    interval = Random.Range(minInterval, maxInterval);
                    // Update time to next trigger.
                    timeToTrigger = Time.time + interval;

                    // Trigger.
                    result = !result;
                }
            }
        }

        #endregion

        #region METHODS

        /// Method that triggers the '_result' in time intervals.
        private void Trigger() {
            // Change component state right after initial delay.
            result = !result;

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
                result = !result;
                yield return new WaitForSeconds(interval);
            }
        }

        #endregion
    }

}
