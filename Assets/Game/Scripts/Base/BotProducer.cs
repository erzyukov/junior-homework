namespace Game
{
	using System;
	using UnityEngine;

	[RequireComponent(typeof(BaseBots))]
    public class BotProducer : MonoBehaviour
    {
		[SerializeField] private int _botPrice;
		[SerializeField] private int _maxCount;
		[SerializeField] private Transform _botContainer;
		[SerializeField] private Bot _botPrefab;
		[SerializeField] private BaseWarehouse _baseWarehouse;

		private Base _base;
		private BaseBots _baseBots;
		private bool _isProduceActive;
		private Color _baseColor;

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

		public void InitBase(int botCount, Color baseColor)
		{
			_baseColor = baseColor;
			for (int i = 0; i < botCount; i++)
				ProduceBot();
		}

		private void OnBaseStateChangedHandler(Base.State baseState)
		{
			_isProduceActive = baseState == Base.State.BuildBots;
		}

		private void OnOreDeliveredHandler()
		{
			if (_isProduceActive == false || _baseBots.Count >= _maxCount)
				return;

			if (_baseWarehouse.TrySpentOre(_botPrice))
				ProduceBot();
		}

		private void ProduceBot()
		{
			Bot bot = Instantiate(_botPrefab, transform.position, Quaternion.identity, _botContainer);
			bot.SetColor(_baseColor);
			_baseBots.AddBot(bot);
		}
	}
}
