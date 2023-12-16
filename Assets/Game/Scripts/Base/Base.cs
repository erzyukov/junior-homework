namespace Game
{
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(BaseBots))]
    public class Base : MonoBehaviour
    {
        private OreSpawner _oreSpawner;
        private BaseBots _baseBots;
        private State _state;

        public event UnityAction<State> StateChanged;

        public enum State
        {
            BuildBots,
            BuildBase,
        }

        private void Awake()
        {
            SetState(State.BuildBots);
            _baseBots = GetComponent<BaseBots>();
        }

        private void OnEnable()
        {
            _baseBots.BotFreed += OnBotFreed;
        }

        private void OnDisable()
        {
            _baseBots.BotFreed -= OnBotFreed;
            _oreSpawner.Spawned -= OnOreSpawned;
        }

        public void InitBase(OreSpawner oreSpawner)
        {
            _oreSpawner = oreSpawner;
            _oreSpawner.Spawned += OnOreSpawned;
        }

        public void SetState(State state)
        {
            _state = state;
            StateChanged?.Invoke(_state);
        }

        private void OnBotFreed(Bot bot)
        {
            if (_state == State.BuildBots && _oreSpawner.TryGetOre(out Ore ore))
                bot.StartGather(ore);
        }

        private void OnOreSpawned()
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