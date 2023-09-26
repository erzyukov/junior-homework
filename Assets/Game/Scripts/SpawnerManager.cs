using System.Collections;
using UnityEngine;

namespace Game
{
    public class SpawnerManager : MonoBehaviour
    {
		[SerializeField] private int _enemyCount;
		[SerializeField] private float _spawnSecondsDeley;
		[SerializeField] private EnemySpawner[] _enemySpawners;

		private void Start()
		{
			StartCoroutine(SpawnEnemy());
		}

		private IEnumerator SpawnEnemy()
		{
			WaitForSeconds waitForSeconds = new WaitForSeconds(_spawnSecondsDeley);

			for (int i = 0; i < _enemyCount; i++)
			{
				int spawnerIndex = Random.Range(0, _enemySpawners.Length);
				_enemySpawners[spawnerIndex].Spawn();

				yield return waitForSeconds;
			}
		}
	}
}
