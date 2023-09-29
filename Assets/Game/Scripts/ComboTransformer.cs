using UnityEngine;
using DG.Tweening;

namespace Game
{
	public class ComboTransformer : MonoBehaviour
	{
		[SerializeField] private Vector3[] _path;
		[SerializeField] private float _duration;

		private void Start()
		{
			float defaultScale = 1;
			float scaleTarget = 2;
			float halfDuration = _duration / 2;

			Sequence sequence = DOTween.Sequence();
			sequence.Append(transform.DOPath(_path, _duration).SetEase(Ease.Linear));
			sequence.Join(transform.DORotate(new Vector3(0, 360, 360), _duration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
			sequence.Join(transform.DOScale(scaleTarget, halfDuration).SetEase(Ease.InOutQuad));
			sequence.Insert(halfDuration, transform.DOScale(defaultScale, halfDuration).SetEase(Ease.InOutQuad));
			sequence.SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
		}
	}
}