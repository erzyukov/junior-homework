namespace Game
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[RequireComponent(typeof(BoxCollider))]
    public class OreSpawner : MonoBehaviour
    {
		[SerializeField] private Ore _orePrefab;
		[SerializeField] private float _spawnSecondsDelay;
		[SerializeField] private int _maxOreOnField;

		private List<Ore> _ores;
		private Bounds _bounds;

		private void Start()
		{
			_ores = new List<Ore>();
			_bounds = GetComponent<BoxCollider>().bounds;
			StartCoroutine(SpawnOre());
		}

		public List<Ore> Ores => _ores;

		private IEnumerator SpawnOre()
		{
			WaitForSeconds waitForSeconds = new WaitForSeconds(_spawnSecondsDelay);

			while (Application.isPlaying)
			{
				yield return new WaitUntil(() => _ores.Count < _maxOreOnField);
				yield return waitForSeconds;

				Vector3 position = GetRandomPosition();
				Ore ore = Instantiate(_orePrefab, position, Quaternion.identity, transform);
				_ores.Add(ore);
			}
		}

		private Vector3 GetRandomPosition()
		{
			var randomPositionInside = new Vector3(
				UnityEngine.Random.Range(_bounds.min.x, _bounds.max.x),
				0,
				UnityEngine.Random.Range(_bounds.min.z, _bounds.max.z)
			);

			return _bounds.ClosestPoint(randomPositionInside);
		}

		public void Remove(Ore ore) =>
			_ores.Remove(ore);
	}
}
