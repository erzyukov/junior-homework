namespace Game
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	[RequireComponent(typeof(BaseBots))]
    public class Base : MonoBehaviour
    {
		[SerializeField] private float _scanInterval;

		private OreSpawner _oreSpawner;
		private List<Ore> _gatheringOres;

		private BaseBots _baseBots;
		private BaseWarehouse _baseWarehouse;

		private void Start()
        {
			_gatheringOres = new List<Ore>();

			_oreSpawner = FindObjectOfType<OreSpawner>();
			_baseBots = GetComponent<BaseBots>();
			_baseWarehouse = GetComponentInChildren<BaseWarehouse>();

			_baseWarehouse.OreConsumed += OnOreConsumedHandler;

			StartCoroutine(SearchOres());
		}

		private void OnDestroy()
		{
			_baseWarehouse.OreConsumed -= OnOreConsumedHandler;
		}

		private IEnumerator SearchOres()
		{
			WaitForSeconds waitForSeconds = new WaitForSeconds(_scanInterval);

			while (Application.isPlaying)
			{
				yield return new WaitUntil(() => _baseBots.HasFreeBots);

				Ore founded = _oreSpawner.Ores
					.Where(ore => _gatheringOres.Contains(ore) == false && ore.IsGathering == false)
					.FirstOrDefault();

				if (founded != null)
				{
					Bot bot = _baseBots.GetFreeBot();
					bot.StartGather(founded);
					founded.IsGathering = true;
				}

				yield return waitForSeconds;
			}
		}

		private void OnOreConsumedHandler(Ore ore)
		{
			_oreSpawner.Remove(ore);
		}
	}
}
