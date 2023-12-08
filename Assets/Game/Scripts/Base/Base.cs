namespace Game
{
	using UnityEngine;
	using UnityEngine.Events;

	[RequireComponent(typeof(BaseBots))]
    public class Base : MonoBehaviour
    {
		[SerializeField] private float _scanInterval;
		[SerializeField] private OreSpawner _oreSpawner;

		private BaseBots _baseBots;
		private State _state;

		private void Start()
        {
			SetState(State.BuildBots);
			_baseBots = GetComponent<BaseBots>();

			_baseBots.BotFreed += OnBotFreedHandler;
			_oreSpawner.Spawned += OnOreSpawnedHandler;
		}

		private void OnDestroy()
		{
			_baseBots.BotFreed -= OnBotFreedHandler;
			_oreSpawner.Spawned -= OnOreSpawnedHandler;
		}

		public event UnityAction<State> StateChanged;

		public enum State
		{
			BuildBots,
			BuildBase,
		}

		public void SetState(State state)
		{
			_state = state;
			StateChanged.Invoke(_state);
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