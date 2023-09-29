using UnityEngine;
using DG.Tweening;

namespace Game
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField] private float _speed;

		private void Start()
		{
			float distance = 360;
			float time = distance / _speed;

			transform
				.DORotate(new Vector3(0, 360, 0), time, RotateMode.FastBeyond360)
				.SetLoops(-1, LoopType.Restart)
				.SetEase(Ease.Linear);
		}
	}
}