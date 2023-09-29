using UnityEngine;
using DG.Tweening;

namespace Game
{
	public class Mover : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private Transform _targetPoint;

		private void Start()
		{
			float distance = (_targetPoint.position - transform.position).magnitude;
			float time = distance / _speed;
			transform.DOMove(_targetPoint.position, time).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo);
		}
	}
}