using UnityEngine;
using System.Collections;

namespace OneDayGame {

	public class Randomizer : GameComponent {

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

		public override void Awake() {
			base.Awake();
		}

		public override void Start () {
			base.Start();

			// Handle 'Fixed' interval type.
			if (_intervalType == IntervalTypes.Fixed) {
				Invoke("Trigger", _initDelay);
			}
		}

		public override void Update () {
			base.Update();

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

		public override void FixedUpdate() {
			base.FixedUpdate();
		}

		public override void LateUpdate() {
			base.LateUpdate();
		}

		/// Method that triggers the '_result' in time intervals.
		private void Trigger() {
			// Change component state right after initial delay.
			_result = !_result;

			//  Change controller state in fixed intervals.
			StartCoroutine(Timer.Start(
						_interval,
						true,
						() => { _result = !_result; }
						));
		}
	}
}
