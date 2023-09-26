using System.Collections;
using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
		[SerializeField] private float _speed;

		public void MoveTo(Vector3 position)
		{
			StartCoroutine(StartMove(position));
		}

		private IEnumerator StartMove(Vector3 targetPosition)
		{
			while (transform.position != targetPosition)
			{
				transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

				yield return null;
			}
		}
	}
}