// Copyright (c) 2015 Bartłomiej Wołk (bartlomiejwolk@gmail.com)
// 
// This file is part of the Randomizer extension for Unity. Licensed under the
// MIT license. See LICENSE file in the project root folder.

using System.Collections;
using UnityEngine;

namespace RandomizerEx {

    /// <summary>
    /// </summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>
    ///             Interval type inspector option will have
    ///             effect only when the Start method is called.
    ///         </item>
    ///     </list>
    /// </remarks>
    public class Randomizer : MonoBehaviour {
        #region INSPECTOR FIELDS

        [SerializeField]
        private float initDelay;

        [SerializeField]
        private float interval;

        [SerializeField]
        private IntervalTypes intervalType;

        [SerializeField]
        private float maxInterval;

        [SerializeField]
        private float minInterval;

        #endregion INSPECTOR FIELDS

        #region PROPERTIES

        public float InitDelay {
            get { return initDelay; }
            set { initDelay = value; }
        }

        /// <summary>
        ///     Time between consequent state changes. Used with fixed interval
        ///     type.
        /// </summary>
        public float Interval {
            get { return interval; }
            set { interval = value; }
        }

        /// <summary>
        ///     Decides when class state will be toggled. For eg. it can be toggled
        ///     in fixed time steps or randomly.
        /// </summary>
        public IntervalTypes IntervalType {
            get { return intervalType; }
            set { intervalType = value; }
        }

        public float MaxInterval {
            get { return maxInterval; }
            set { maxInterval = value; }
        }

        public float MinInterval {
            get { return minInterval; }
            set { minInterval = value; }
        }

        /// <summary>
        ///     The class output value. Use it to trigger actions in your game.
        /// </summary>
        public bool State { get; private set; }

        /// <summary>
        ///     Reference to coroutine responsible for toggling class state.
        /// </summary>
        public Task ToggleStateCoroutine { get; set; }

        #endregion PROPERTIES

        #region UNITY MESSAGES

        private void Start() {
            if (IntervalType == IntervalTypes.Fixed) {
                ToggleStateCoroutine = new Task(FixTimeTrigger());
            }

            if (IntervalType == IntervalTypes.Random) {
                ToggleStateCoroutine = new Task(RandomTimeTrigger());
            }
        }

        #endregion UNITY MESSAGES

        #region METHODS

        private IEnumerator FixTimeTrigger() {
            yield return new WaitForSeconds(InitDelay);

            while (true) {
                State = !State;
                yield return new WaitForSeconds(Interval);
            }
        }

        private IEnumerator RandomTimeTrigger() {
            yield return new WaitForSeconds(InitDelay);

            while (true) {
                // Toggle state.
                State = !State;

                // Calculate random time to wait.
                var randomInterval = Random.Range(MinInterval, MaxInterval);

                yield return new WaitForSeconds(randomInterval);
            }
        }

        #endregion METHODS
    }

}