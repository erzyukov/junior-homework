namespace Game
{
    using UnityEngine;

    [RequireComponent(typeof(BaseBots), typeof(Base))]
    public class BaseBotProducer : MonoBehaviour
    {
        [SerializeField] private int _botPrice;
        [SerializeField] private int _maxCount;
        [SerializeField] private Transform _botContainer;
        [SerializeField] private Bot _botPrefab;
        [SerializeField] private BaseWarehouse _baseWarehouse;

        private Base _base;
        private BaseBots _baseBots;
        private bool _isProduceActive;
        private Color _baseColor;

        private void Awake()
        {
            _base = GetComponent<Base>();
            _baseBots = GetComponent<BaseBots>();
        }

        private void OnEnable()
        {
            _base.StateChanged += OnBaseStateChanged;
            _baseWarehouse.OreDelivered += OnOreDelivered;
        }

        private void Start()
        {
            _isProduceActive = true;
        }

        private void OnDisable()
        {
            _baseWarehouse.OreDelivered -= OnOreDelivered;
            _base.StateChanged -= OnBaseStateChanged;
        }

        public void InitBase(int botCount, Color baseColor)
        {
            _baseColor = baseColor;

            for (int i = 0; i < botCount; i++)
                ProduceBot();
        }

        private void OnBaseStateChanged(Base.State baseState)
        {
            _isProduceActive = baseState == Base.State.BuildBots;
        }

        private void OnOreDelivered()
        {
            if (_isProduceActive == false || _baseBots.Count >= _maxCount)
                return;

            if (_baseWarehouse.TrySpentOre(_botPrice))
                ProduceBot();
        }

        private void ProduceBot()
        {
            Bot bot = Instantiate(_botPrefab, transform.position, Quaternion.identity, _botContainer);
            bot.SetColor(_baseColor);
            _baseBots.AddBot(bot);
        }
    }
}
