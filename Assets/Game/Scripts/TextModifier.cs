using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
    public class TextModifier : MonoBehaviour
    {
		[SerializeField] private string _replaceText;
		[SerializeField] private string _additionalText;
		[SerializeField] private string _effectText;
		[SerializeField] private Text _textElement;
		[SerializeField] private float _duration;

		private void Start()
        {
			float delay = 1;

			Sequence sequence = DOTween.Sequence();
			sequence.SetDelay(delay);
			sequence.Append(_textElement.DOText(_replaceText, _duration));
			sequence.Append(_textElement.DOText(_additionalText, _duration).SetRelative());
			sequence.Append(_textElement.DOText(_effectText, _duration, true, ScrambleMode.All));
			sequence.SetLoops(-1, LoopType.Restart);
		}
    }
}
