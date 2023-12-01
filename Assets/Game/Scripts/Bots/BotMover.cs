namespace Game
{
	using System.Collections;
	using UnityEngine;

    public class BotMover : MonoBehaviour
    {
		[SerializeField] private float _speed;

		private Transform _target;
		private Coroutine _mover;

		public void MoveTo(Transform target)
		{
			if (_mover != null)
				StopCoroutine(_mover);
			
			_target = target;
			_mover = StartCoroutine(StartMove());
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