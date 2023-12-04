namespace Game
{
	using UnityEngine;
	using TMPro;

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
				_oreCountText.text = _oreCount.ToString();
				
				bot.SetFree();
			}
		}
	}
}