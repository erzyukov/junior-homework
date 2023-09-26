using UnityEngine;

namespace Game
{
	public class ForwardMover : MonoBehaviour
	{
		[SerializeField] private float _speed;

		private void Update()
		{
			Vector3 movingIncrement = transform.forward * _speed * Time.deltaTime;
			transform.Translate(movingIncrement, Space.World);
		}
	}
}