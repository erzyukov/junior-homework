namespace Game
{
    using UnityEngine;

    public class BaseSpawner : MonoBehaviour
    {
        [SerializeField] private OreSpawner _oreSpawner;
        [SerializeField] private BaseFacade _basePrefab;
        [SerializeField] private Transform _startBasePoint;
        [SerializeField] private int _startBotCount;

        private void Start()
        {
            SpawnBase(_startBotCount, _startBasePoint.position);
        }

        public BaseFacade SpawnBase(int startBotCount, Vector3 position)
        {
            BaseFacade createdBase = Instantiate(_basePrefab, position, Quaternion.identity);
            createdBase.InitBase(_oreSpawner, startBotCount, this);

            return createdBase;
        }
    }
}