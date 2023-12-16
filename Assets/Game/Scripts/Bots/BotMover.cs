namespace Game
{
    using System;
    using System.Collections;
    using UnityEngine;

    public class BotMover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Coroutine _mover;

        public void MoveTo(Vector3 target, Action TargetReached = null)
        {
            if (_mover != null)
                StopCoroutine(_mover);

            _mover = StartCoroutine(StartMove(target, TargetReached));
        }

        private IEnumerator StartMove(Vector3 target, Action TargetReached)
        {
            while (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

                yield return null;
            }

            TargetReached?.Invoke();
        }
    }
}