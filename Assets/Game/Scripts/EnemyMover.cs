using System.Collections;
using UnityEngine;

namespace Game
{
    public class EnemyMover : MonoBehaviour
    {
		[SerializeField] private float _speed;

		private Transform _target;

		public void Init(Transform target)
		{
			_target = target;
			StartCoroutine(StartMove());
		}

		private IEnumerator StartMove()
		{
			while (transform.position != _target.position)
			{
				transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

				yield return null;
			}
		}
	}
}