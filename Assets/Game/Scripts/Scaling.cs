using UnityEngine;

namespace Game
{
	public class Scaling : MonoBehaviour
	{
		[SerializeField] private float _increaseSpeed;

		void Update()
		{
			Vector3 scalingIncrement = Vector3.one * _increaseSpeed * Time.deltaTime;
			transform.localScale += scalingIncrement;
		}
	}
}