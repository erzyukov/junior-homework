namespace Game
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.Events;

    public class BaseWarehouse : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _oreCountText;
        [SerializeField] private BaseBots _baseBots;

        private int _oreCount;

        public event UnityAction OreDelivered;

        public int OreCount => _oreCount;

        private void OnTriggerStay(Collider other)
        {
            if (other.transform.parent.TryGetComponent<Bot>(out Bot bot) && _baseBots.HasBot(bot) && bot.HasOre)
            {
                Ore ore = bot.HandOverOre();
                Destroy(ore.gameObject);

                _oreCount++;
                UpdateUi();

                OreDelivered.Invoke();

                bot.SetFree();
            }
        }

        public bool TrySpentOre(int amount)
        {
            if (_oreCount < amount)
                return false;

            _oreCount -= amount;
            UpdateUi();

            return true;
        }

        public void UpdateUi() =>
            _oreCountText.text = _oreCount.ToString();
    }
}