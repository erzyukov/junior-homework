using DG.Tweening;
using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(MeshRenderer))]
    public class ColorChanger : MonoBehaviour
    {
		[SerializeField] private Color _targetColor;
		[SerializeField] private float _duration;

		private MeshRenderer _renderer;

		private void Start()
        {
			_renderer = GetComponent<MeshRenderer>();
			_renderer.material
				.DOColor(_targetColor, _duration)
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutFlash);
		}
    }
}
