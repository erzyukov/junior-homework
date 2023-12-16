namespace Game
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    [RequireComponent(typeof(BaseBots), typeof(Base))]
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

        public enum State
        {
            None,
            Initiated,
            TargetPlaced,
            WaitForResources,
            Building,
        }

        private void Awake()
        {
            _base = GetComponent<Base>();
            _baseBots = GetComponent<BaseBots>();
        }

        private void OnEnable()
        {
            _baseBots.BotFreed += OnBotFreed;
            _baseWarehouse.OreDelivered += OnOreDelivered;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
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

        private void OnDisable()
        {
            _baseWarehouse.OreDelivered -= OnOreDelivered;
            _baseBots.BotFreed -= OnBotFreed;
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

        private void OnOreDelivered()
        {
            if (_state != State.TargetPlaced)
                return;

            TryBuild();
        }

        private void OnBotFreed(Bot bot)
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

            _base.SetState(Base.State.BuildBots);
        }
    }
}