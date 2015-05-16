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
        public bool State { get; private set; }

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

        /// <summary>
        /// Reference to coroutine responsible for toggling
        /// class state.
        /// </summary>
        public Task ToggleStateCoroutine { get; set; }

        #endregion

        #region UNITY MESSAGES

        private void Start () {
            if (IntervalType == IntervalTypes.Fixed) {
                ToggleStateCoroutine = new Task(FixTimeTrigger());
            }

            if (IntervalType == IntervalTypes.Random) {
                ToggleStateCoroutine = new Task(RandomTimeTrigger());
            }
        }
        #endregion

        #region METHODS
        private IEnumerator RandomTimeTrigger() {
            yield return new WaitForSeconds(InitDelay);

            var randomInterval = 0f;

            while (true) {
                if (Time.time > timeToTrigger) {
                    // Toggle state.
                    State = !State;

                    // Calculate random time to wait.
                    randomInterval = Random.Range(MinInterval, MaxInterval);
                }

                yield return new WaitForSeconds(randomInterval);
            }
        }


        private IEnumerator FixTimeTrigger() {
            yield return new WaitForSeconds(InitDelay);

            while (true) {
                State = !State;
                yield return new WaitForSeconds(Interval);
            }
        }

        #endregion
    }

}
