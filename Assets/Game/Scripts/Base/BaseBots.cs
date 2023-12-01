namespace Game
{
	using System.Linq;
	using UnityEngine;

    public class BaseBots : MonoBehaviour
    {
		private Bot[] _bots;

        private void Start()
        {
			_bots = GetComponentsInChildren<Bot>();
		}

		public bool HasFreeBots => _bots.Any(bot => bot.IsFree);

		public Bot GetFreeBot() => _bots.Where(bot => bot.IsFree).FirstOrDefault();
    }
}
