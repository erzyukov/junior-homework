using UnityEngine;

namespace Game
{
	public class Mover : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private Transform _targetPoint;

		private void Update()
		{
			Vector3 movingIncrement = transform.forward * _speed * Time.deltaTime;
			transform.Translate(movingIncrement, Space.World);
		}
	}
}