using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        public void LookAt(Vector3 position) =>
			transform.LookAt(position);
    }
}
