using UnityEngine;

namespace Game
{
	public class Scaler : MonoBehaviour
	{
		[SerializeField] private float _increaseSpeed;

		private void Update()
		{
			Vector3 scalingIncrement = Vector3.one * _increaseSpeed * Time.deltaTime;
			transform.localScale += scalingIncrement;
		}
	}
}