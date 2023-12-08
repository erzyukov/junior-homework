namespace Game
{
	using System;
	using UnityEngine;

	[RequireComponent(typeof(BaseBots))]
    public class BotProducer : MonoBehaviour
    {
		[SerializeField] private int _botPrice;
		[SerializeField] private Transform _botContainer;
		[SerializeField] private Bot _botPrefab;
		[SerializeField] private BaseWarehouse _baseWarehouse;

		private Base _base;
		private BaseBots _baseBots;
		private bool _isProduceActive;

		private void Awake()
		{
			_baseBots = GetComponent<BaseBots>();
			_base = GetComponent<Base>();
			_baseWarehouse.OreDelivered += OnOreDeliveredHandler;

			_base.StateChanged += OnBaseStateChangedHandler;
		}

		private void OnDestroy()
		{
			_baseWarehouse.OreDelivered -= OnOreDeliveredHandler;
			_base.StateChanged -= OnBaseStateChangedHandler;
		}

		private void OnBaseStateChangedHandler(Base.State baseState)
		{
			_isProduceActive = baseState == Base.State.BuildBots;
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
