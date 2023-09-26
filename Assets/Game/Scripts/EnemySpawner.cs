using System.Collections;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
		[SerializeField] private int _enemyCount;
		[SerializeField] private float _spawnSecondsDeley;
		[SerializeField] private Enemy _prefab;
		[SerializeField] private Transform[] _spawnPoints;

		private void Start()
		{
			StartCoroutine(SpawnEnemy());
		}

		private IEnumerator SpawnEnemy()
		{
			WaitForSeconds waitForSeconds = new WaitForSeconds(_spawnSecondsDeley);

			for (int i = 0; i < _enemyCount; i++)
            {
				Vector3 position = GetRandomPosition();
				Enemy enemy = Instantiate(_prefab, position, Quaternion.identity);
				Vector2 randomVector = Random.insideUnitCircle;
				Vector3 lookAtPosition = enemy.transform.position + new Vector3(randomVector.x, 0, randomVector.y);
				enemy.LookAt(lookAtPosition);

				yield return waitForSeconds;
			}
        }

		private Vector3 GetRandomPosition()
		{
			int index = Random.Range(0, _spawnPoints.Length);

			return _spawnPoints[index].position;
		}
	}
}
