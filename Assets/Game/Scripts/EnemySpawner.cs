using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
		[SerializeField] private Enemy _prefab;
		[SerializeField] private Transform _target;

		public void Spawn()
		{
			Enemy enemy = Instantiate(_prefab, transform.position, Quaternion.identity);
			enemy.MoveTo(_target.position);
		}
	}
}