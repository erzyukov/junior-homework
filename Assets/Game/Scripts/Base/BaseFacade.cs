namespace Game
{
	using UnityEngine;

    public class BaseFacade : MonoBehaviour
    {
		private Base _base;
		private BaseBotProducer _botProducer;
		private BaseExpander _baseExpander;
		private BaseBots _baseBots;
		private Color _baseColor;

		private void Awake()
		{
			_base = GetComponent<Base>();
			_botProducer = GetComponent<BaseBotProducer>();
			_baseExpander = GetComponent<BaseExpander>();
			_baseBots = GetComponent<BaseBots>();
			_baseColor = Random.ColorHSV();
		}

		public Color BaseColor => _baseColor;

		public void InitBase(OreSpawner oreSpawner, int startBotCount, BaseSpawner baseSpawner)
		{
			_base.InitBase(oreSpawner);
			_botProducer.InitBase(startBotCount, _baseColor);
			_baseExpander.InitBase(baseSpawner);
		}

		public void AddBot(Bot bot) =>
			_baseBots.AddBot(bot);
	}
}