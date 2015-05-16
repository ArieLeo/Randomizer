using UnityEngine;
using System.Collections;

namespace Randomizer {

	public class Randomizer : MonoBehaviour {

		public enum IntervalTypes { Fixed, Random }

		/// Result that the component returns.
		///
		/// This property can be read by another component in order to execute
		/// action at a random intervals.
		private bool _result;
		public bool Result {
			get { return _result; }
		}

		/// Initial delay.
		[SerializeField]
		private float _initDelay;

		/// Time between consequent triggers.
		[SerializeField]
		private float _interval;

		/// Interval type.
		[SerializeField]
		private IntervalTypes _intervalType;

		/// Minimum interval.
		[SerializeField]
		private float _minInterval;

		/// Maximum interval.
		[SerializeField]
		private float _maxInterval;

		/// Helper variable.
		///
		/// In-game time to next trigger.
		/// Used in 'Random' option.
		private float _timeToTrigger;

		private void Start () {
			// Handle 'Fixed' interval type.
			if (_intervalType == IntervalTypes.Fixed) {
				Invoke("Trigger", _initDelay);
			}
		}

		private void Update () {
			/// Handle 'Random' interval option.
			if (_intervalType == IntervalTypes.Random) {
				// Wait random interval before trigger.
				if (Time.time > _timeToTrigger) {
					float interval;

					// Calculate new random interval.
					interval = Random.Range(_minInterval, _maxInterval);
					// Update time to next trigger.
					_timeToTrigger = Time.time + interval;

					// Trigger.
					_result = !_result;
				}
			}
		}

		/// Method that triggers the '_result' in time intervals.
		private void Trigger() {
			// Change component state right after initial delay.
			_result = !_result;

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
	            _result = !_result;
	            yield return new WaitForSeconds(_interval);
	        }
	    }
	}
}
