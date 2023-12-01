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
			Bot bot = other.GetComponentInParent<Bot>();

			if (bot != null && bot.HasOre)
			{
				Ore ore = bot.HandOverOre();
				OreConsumed?.Invoke(ore);
				Destroy(ore.gameObject);

				_oreCount++;
				_oreCountText.text = _oreCount.ToString();
			}
		}

		public event UnityAction<Ore> OreConsumed;
	}
}
