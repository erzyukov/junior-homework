using UnityEngine;

namespace Game
{
	public class Rotation : MonoBehaviour
	{
		[SerializeField] private float _angularSpeed;

		void Update()
		{
			Vector3 rotationEulersIncrement = Vector3.up * _angularSpeed * Time.deltaTime;
			transform.Rotate(rotationEulersIncrement);
		}
	}
}