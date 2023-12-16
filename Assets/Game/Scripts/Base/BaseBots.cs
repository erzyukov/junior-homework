namespace Game
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;

    public class BaseBots : MonoBehaviour
    {
        private List<Bot> _bots = new List<Bot>();

        public event UnityAction<Bot> BotFreed;

        public int Count => _bots.Count;

        public bool HasFreeBots =>
            _bots.Any(bot => bot.IsFree);

        private void OnDisable()
        {
            for (int i = 0; i < _bots.Count; i++)
                _bots[i].Freed -= OnBotFreed;
        }

        public Bot GetFreeBot() =>
            _bots.Where(bot => bot.IsFree).FirstOrDefault();

        public bool HasBot(Bot bot) =>
            _bots.Contains(bot);

        public void AddBot(Bot bot)
        {
            bot.Freed += OnBotFreed;
            _bots.Add(bot);
        }

        public void RemoveBot(Bot bot)
        {
            bot.Freed -= OnBotFreed;
            _bots.Remove(bot);
        }

        private void OnBotFreed(Bot bot) =>
            BotFreed.Invoke(bot);
    }
}
