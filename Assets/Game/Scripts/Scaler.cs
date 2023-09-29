using UnityEngine;
using DG.Tweening;

namespace Game
{
	public class Scaler : MonoBehaviour
	{
		[SerializeField] private float _targetScale;
		[SerializeField] private float _speed;

		private void Start()
		{
			float time = (_targetScale - transform.localScale.x) / _speed;
			transform.DOScale(_targetScale, time).SetEase(Ease.OutElastic).SetLoops(-1, LoopType.Restart);
		}
	}
}