namespace Game
{
	using UnityEngine;
	using TMPro;
	using UnityEngine.Events;

	public class BaseWarehouse : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _oreCountText;

		private int _oreCount;

		private void OnTriggerEnter(Collider other)
		{
			if (other.transform.parent.TryGetComponent<Bot>(out Bot bot) && bot.HasOre)
			{
				Ore ore = bot.HandOverOre();
				Destroy(ore.gameObject);

				_oreCount++;
				UpdateUi();

				OreDelivered.Invoke();

				bot.SetFree();
			}
		}

		public event UnityAction OreDelivered;

		public int OreCount => _oreCount;

		public bool TrySpentOre(int amount)
		{
			if (_oreCount < amount)
				return false;

			_oreCount -= amount;
			UpdateUi();
			return true;
		}

		public void UpdateUi()
		{
			_oreCountText.text = _oreCount.ToString();
		}
	}
}