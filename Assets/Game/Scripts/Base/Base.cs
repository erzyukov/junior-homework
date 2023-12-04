namespace Game
{
	using UnityEngine;

	[RequireComponent(typeof(BaseBots))]
    public class Base : MonoBehaviour
    {
		[SerializeField] private float _scanInterval;

		private OreSpawner _oreSpawner;

		private BaseBots _baseBots;

		private void Start()
        {
			_oreSpawner = FindObjectOfType<OreSpawner>();
			_baseBots = GetComponent<BaseBots>();

			_baseBots.BotFreed += OnBotFreedHandler;
			_oreSpawner.Spawned += OnOreSpawnedHandler;
		}

		private void OnDestroy()
		{
			_baseBots.BotFreed -= OnBotFreedHandler;
			_oreSpawner.Spawned -= OnOreSpawnedHandler;
		}

		private void OnBotFreedHandler(Bot bot)
		{
			if (_oreSpawner.TryGetOre(out Ore ore))
				bot.StartGather(ore);
		}

		private void OnOreSpawnedHandler()
		{
			if (_baseBots.HasFreeBots == false)
				return;

			if (_oreSpawner.TryGetOre(out Ore ore))
			{
				Bot bot = _baseBots.GetFreeBot();
				bot.StartGather(ore);
			}
		}
	}
}
