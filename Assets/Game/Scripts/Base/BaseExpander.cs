namespace Game
{
	using System;
	using UnityEngine;
	using UnityEngine.EventSystems;

    public class BaseExpander : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private int _basePrice;
		[SerializeField] private GameObject _flagPrefab;
		[SerializeField] private LayerMask _groundLayerMask;
		[SerializeField] private BaseWarehouse _baseWarehouse;
		[SerializeField] private Base _basePrefab;

		private Base _base;
		private BaseBots _baseBots;
		private BaseSpawner _baseSpawner;

		private Camera _camera;
		private Transform _flag;
		private State _state;

		private Ray _ray;
		private RaycastHit _hitData;

		private void Awake()
		{
			_base = GetComponent<Base>();
			_baseBots = GetComponent<BaseBots>();
			_baseWarehouse.OreDelivered += OnOreDeliveredHandler;
			_baseBots.BotFreed += OnBotFreedHandler;
		}

		private void Start()
		{
			_camera = Camera.main;
		}

		void Update()
		{
			if (_state != State.Initiated && _state != State.TargetPlaced)
				return;

			if (Input.GetMouseButtonDown(0))
			{
				_ray = _camera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(_ray, out _hitData, 1000, _groundLayerMask))
				{
					_flag.position = _hitData.point;

					if (_state == State.Initiated)
						StartBuildProcess();
				}
			}
		}

		private void OnDestroy()
		{
			_baseWarehouse.OreDelivered -= OnOreDeliveredHandler;
			_baseBots.BotFreed -= OnBotFreedHandler;
		}

		public enum State
		{
			None,
			Initiated,
			TargetPlaced,
			WaitForResources,
			Building,
		}

		public void InitBase(BaseSpawner baseSpawner)
		{
			_baseSpawner = baseSpawner;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (_state != State.None)
				return;

			_flag = Instantiate(_flagPrefab, eventData.pointerCurrentRaycast.worldPosition, Quaternion.identity).transform;
			_state = State.Initiated;
		}

		private void StartBuildProcess()
		{
			_base.SetState(Base.State.BuildBase);
			_state = State.TargetPlaced;

			TryBuild();
		}

		private void OnOreDeliveredHandler()
		{
			if (_state != State.TargetPlaced)
				return;

			TryBuild();
		}

		private void OnBotFreedHandler(Bot bot)
		{
			if (_state != State.TargetPlaced)
				return;

			if (_baseWarehouse.TrySpentOre(_basePrice))
				BuildBase(bot);
		}

		private void TryBuild()
		{
			if (_baseBots.HasFreeBots == false)
				return;

			Bot bot = _baseBots.GetFreeBot();

			if (_baseWarehouse.TrySpentOre(_basePrice))
				BuildBase(bot);
		}

		private void BuildBase(Bot bot)
		{
			_base.SetState(Base.State.BuildBots);
			_state = State.Building;
			bot.StartBuild(_flag, () =>
			{
				_baseBots.RemoveBot(bot);
				BaseFacade createdBase = _baseSpawner.SpawnBase(0, _flag.position);
				createdBase.AddBot(bot);
				bot.SetColor(createdBase.BaseColor);
				bot.SetFree();
				Destroy(_flag.gameObject);
				_state = State.None;
			});
		}
	}
}