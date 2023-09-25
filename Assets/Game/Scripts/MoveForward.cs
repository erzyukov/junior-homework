using UnityEngine;

namespace Game
{
	public class MoveForward : MonoBehaviour
	{
		[SerializeField] private float _speed;

		void Update()
		{
			Vector3 movingIncrement = transform.forward * _speed * Time.deltaTime;
			transform.Translate(movingIncrement, Space.World);
		}
	}
}