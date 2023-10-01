using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HeroHealthController : MonoBehaviour
    {
		[SerializeField] private int _healthAmount;
		[SerializeField] private float _maxDisplayHealthChangeDelta;

		private Hero _hero;
		private float _displayedHealthRate;
		private Coroutine _healthRateAnimator;

		private void Start()
		{
			_displayedHealthRate = 1;
			_hero = new Hero(_healthAmount);
			UpdateDisplayedHealth();
		}

		public UnityEvent<float> OnHealthRateChange;

		public void Hit(int damageSize)
		{
			_hero.TakeDamage(damageSize);
			UpdateDisplayedHealth();
		}

		public void Heal(int amount)
		{
			_hero.Heal(amount);
			UpdateDisplayedHealth();
		}

		private void UpdateDisplayedHealth()
		{
			float healthRate = (float)_hero.Health / _hero.MaxHealth;

			if (_healthRateAnimator != null)
				StopCoroutine(_healthRateAnimator);

			_healthRateAnimator = StartCoroutine(AnimateHealth(healthRate));
		}

		private IEnumerator AnimateHealth(float healthRate)
		{
			while (_displayedHealthRate != healthRate)
			{
				_displayedHealthRate = Mathf.MoveTowards(_displayedHealthRate, healthRate, _maxDisplayHealthChangeDelta);
				OnHealthRateChange.Invoke(_displayedHealthRate);

				yield return null;
			}
		}
	}
}