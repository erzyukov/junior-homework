using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HeroHealth : MonoBehaviour
    {
		[SerializeField] private int _healthAmount;

		private Hero _hero;

		private void Start()
		{
			_hero = new Hero(_healthAmount);
			HealthRateChanged.Invoke(_hero.HealthRate);
		}

		public UnityEvent<float> HealthRateChanged;

		public void Hit(int damageSize)
		{
			_hero.TakeDamage(damageSize);
			HealthRateChanged.Invoke(_hero.HealthRate);
		}

		public void Heal(int amount)
		{
			_hero.Heal(amount);
			HealthRateChanged.Invoke(_hero.HealthRate);
		}
	}
}