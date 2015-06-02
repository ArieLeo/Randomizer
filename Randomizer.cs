// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
// 
// This file is part of the Randomizer extension for Unity. Licensed under the
// MIT license. See LICENSE file in the project root folder.

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RandomizerEx {

    // todo move remarks to the github docs
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
        private IntervalType intervalType;

        [SerializeField]
        private float maxInterval;

        [SerializeField]
        private float minInterval;

        [SerializeField]
        private UnityEvent triggerCallback;

        [SerializeField]
        private float initStateOnProbability;

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
        public IntervalType IntervalType {
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
        ///     Reference to coroutine responsible for toggling class state.
        /// </summary>
        public Task ToggleStateCoroutine { get; set; }

        /// <summary>
        /// Callback execute on state changed to on.
        /// </summary>
        public UnityEvent TriggerCallback {
            get { return triggerCallback; }
            set { triggerCallback = value; }
        }

        /// <summary>
        /// Probability that the initial state will be on.
        /// </summary>
        public float InitStateOnProbability {
            get { return initStateOnProbability; }
            set { initStateOnProbability = value; }
        }

        #endregion PROPERTIES

        #region UNITY MESSAGES

        private void Awake() {
            HandleInitStateOnProbability();
        }

        private void Start() {
            HandleIntervalType();
        }
        #endregion UNITY MESSAGES

        #region METHODS
        private void HandleIntervalType() {
            switch (IntervalType) {
                case IntervalType.Fixed:
                    ToggleStateCoroutine = new Task(FixTimeTrigger());

                    break;
                case IntervalType.Random:
                    ToggleStateCoroutine = new Task(RandomTimeTrigger());

                    break;
            }
        }

        private void HandleInitStateOnProbability() {
            if (Random.value < InitStateOnProbability) {
                TriggerCallback.Invoke();
            }
        }


        private IEnumerator FixTimeTrigger() {
            yield return new WaitForSeconds(InitDelay);

            while (true) {
                yield return new WaitForSeconds(Interval);

                TriggerCallback.Invoke();
            }
        }

        private IEnumerator RandomTimeTrigger() {
            yield return new WaitForSeconds(InitDelay);

            while (true) {
                // Calculate random time to wait.
                var randomInterval = Random.Range(MinInterval, MaxInterval);

                yield return new WaitForSeconds(randomInterval);

                TriggerCallback.Invoke();
            }
        }

        #endregion METHODS
    }

}