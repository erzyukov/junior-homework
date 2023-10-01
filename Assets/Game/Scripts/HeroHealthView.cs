using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	[RequireComponent(typeof(Slider))]
	public class HeroHealthView : MonoBehaviour
    {
		[SerializeField] private float _maxDisplayHealthChangeDelta;

		private Coroutine _healthRateAnimator;
		private Slider _slider;

		private void Start()
		{
			_slider = GetComponent<Slider>();
		}

		public void SetHealthRate(float healthRate)
		{
			if (_healthRateAnimator != null)
				StopCoroutine(_healthRateAnimator);

			_healthRateAnimator = StartCoroutine(AnimateHealth(healthRate));
		}

		private IEnumerator AnimateHealth(float healthRate)
		{
			while (_slider.value != healthRate)
			{
				_slider.value = Mathf.MoveTowards(_slider.value, healthRate, _maxDisplayHealthChangeDelta);

				yield return null;
			}
		}
	}
}
