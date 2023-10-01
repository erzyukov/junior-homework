using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HeroHealthController : MonoBehaviour
    {
		[SerializeField] private int _healthAmount;

		private Hero _hero;

		private void Start()
		{
			_hero = new Hero(_healthAmount);
			OnHealthRateChange.Invoke(_hero.HealthRate);
		}

		public UnityEvent<float> OnHealthRateChange;

		public void Hit(int damageSize)
		{
			_hero.TakeDamage(damageSize);
			OnHealthRateChange.Invoke(_hero.HealthRate);
		}

		public void Heal(int amount)
		{
			_hero.Heal(amount);
			OnHealthRateChange.Invoke(_hero.HealthRate);
		}
	}
}