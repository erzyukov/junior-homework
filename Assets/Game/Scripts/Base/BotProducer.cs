namespace Game
{
	using UnityEngine;

	[RequireComponent(typeof(BaseBots))]
    public class BotProducer : MonoBehaviour
    {
		[SerializeField] private int _botPrice;
		[SerializeField] private Transform _botContainer;
		[SerializeField] private Bot _botPrefab;
		[SerializeField] private BaseWarehouse _baseWarehouse;

		private BaseBots _baseBots;
		private bool _isProduceActive;

		private void Awake()
		{
			_baseBots = GetComponent<BaseBots>();
			_baseWarehouse.OreDelivered += OnOreDeliveredHandler;
			_isProduceActive = true;
		}

		private void OnDestroy()
		{
			_baseWarehouse.OreDelivered -= OnOreDeliveredHandler;
		}

		private void OnOreDeliveredHandler()
		{
			if (_isProduceActive == false)
				return;

			if (_baseWarehouse.TrySpentOre(_botPrice))
				ProduceBot();
		}

		private void ProduceBot()
		{
			Bot bot = Instantiate(_botPrefab, transform.position, Quaternion.identity, _botContainer);
			_baseBots.AddBot(bot);
		}
	}
}
