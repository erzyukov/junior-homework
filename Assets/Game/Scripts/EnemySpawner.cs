using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
		[SerializeField] private EnemyMover _prefab;
		[SerializeField] private Transform _target;

		public void Spawn()
		{
			EnemyMover enemy = Instantiate(_prefab, transform.position, Quaternion.identity);
			enemy.Init(_target);
		}
	}
}