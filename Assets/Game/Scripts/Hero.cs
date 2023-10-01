using UnityEngine;

namespace Game
{
    public class Hero
    {
		private int _maxHealth;
		private int _health;

		public Hero(int health)
		{
			_health = health;
			_maxHealth = health;
		}

		public float HealthRate => (float)_health / _maxHealth;

		public void Heal(int amount) =>
			_health = Mathf.Min(_health + amount, _maxHealth);

		public void TakeDamage(int amount) =>
			_health = Mathf.Max(_health - amount, 0);
    }
}