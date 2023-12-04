namespace Game
{
	using System.Linq;
	using UnityEngine;
	using UnityEngine.Events;

	public class BaseBots : MonoBehaviour
    {
		private Bot[] _bots;

        private void Start()
        {
			_bots = GetComponentsInChildren<Bot>();

			for (int i = 0; i < _bots.Length; i++)
				_bots[i].Freed += OnBotFreedHandler;
		}

		private void OnDestroy()
		{
			for (int i = 0; i < _bots.Length; i++)
				_bots[i].Freed -= OnBotFreedHandler;
		}

		public event UnityAction<Bot> BotFreed;

		public bool HasFreeBots => _bots.Any(bot => bot.IsFree);

		public Bot GetFreeBot() => _bots.Where(bot => bot.IsFree).FirstOrDefault();

		private void OnBotFreedHandler(Bot bot) =>
			BotFreed.Invoke(bot);
	}
}
