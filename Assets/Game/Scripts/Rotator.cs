using UnityEngine;

namespace Game
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField] private float _angularSpeed;

		private void Update()
		{
			Vector3 rotationEulersIncrement = Vector3.up * _angularSpeed * Time.deltaTime;
			transform.Rotate(rotationEulersIncrement);
		}
	}
}