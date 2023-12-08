namespace Game
{
	using System;
	using System.Collections;
	using UnityEngine;

    public class BotMover : MonoBehaviour
    {
		[SerializeField] private float _speed;

		private Transform _target;
		private Coroutine _mover;

		public void MoveTo(Transform target, Action TargetReached = null)
		{
			if (_mover != null)
				StopCoroutine(_mover);
			
			_target = target;
			_mover = StartCoroutine(StartMove(TargetReached));
		}

		private IEnumerator StartMove(Action TargetReached)
		{
			while (transform.position != _target.position)
			{
				transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

				yield return null;
			}

			TargetReached?.Invoke();
		}
	}
}