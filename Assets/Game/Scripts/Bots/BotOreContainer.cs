namespace Game
{
    using UnityEngine;
    using UnityEngine.Events;

    public class BotOreContainer : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private Ore _target;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent<Ore>(out Ore ore) && ore == _target)
                TakeOre(ore);
        }

        public event UnityAction<Ore> OreGathered;

        public bool HasTarget => _target != null;

        public Ore HandOverOre()
        {
            Ore ore = _target;
            ore.transform.SetParent(null);
            _target = null;

            return ore;
        }

        public void SetTraget(Ore ore) =>
            _target = ore;

        private void TakeOre(Ore ore)
        {
            ore.transform.SetParent(_container);
            ore.transform.localPosition = Vector3.zero;
            OreGathered?.Invoke(ore);
        }
    }
}
