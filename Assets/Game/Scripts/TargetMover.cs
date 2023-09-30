using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game
{
    public class TargetMover : MonoBehaviour
    {
		[SerializeField] private float _speed;

		private int _targetIndex;
		private Vector3[] _path;

        void Start()
        {
			_path = new Vector3[2];

			int index = 0;
			_path[index] = transform.position;

			index++;
			float deviationMultiplier = 4;
			Vector2 deviation = Random.insideUnitCircle.normalized * deviationMultiplier;
			_path[index] = transform.position + new Vector3(deviation.x, 0, deviation.y);

			_targetIndex = index;
		}
		void Update()
        {
			if (transform.position == _path[_targetIndex])
			{
				_targetIndex++;
				
				if (_targetIndex >= _path.Length)
					_targetIndex = 0;
			}
			
			transform.position = Vector3.MoveTowards(transform.position, _path[_targetIndex], _speed * Time.deltaTime);
		}
    }
}
