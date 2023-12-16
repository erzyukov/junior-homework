namespace Game
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(BotMover), typeof(BotOreContainer))]
    public class Bot : MonoBehaviour
    {
        [SerializeField] MeshRenderer _meshRenderer;

        private const string StartMark = "StartMark";

        private State _state;
        private Transform _startPoint;
        private BotMover _mover;
        private BotOreContainer _oreContainer;

        public event UnityAction<Bot> Freed;

        public enum State
        {
            Free,
            Gather,
            Return,
            Build,
        }

        public bool IsFree => _state == State.Free;

        public bool HasOre => _state == State.Return && _oreContainer.HasTarget;

        private void Start()
        {
            _startPoint = new GameObject(StartMark).transform;
            _startPoint.position = transform.position;
            _startPoint.SetParent(transform.parent);

            _mover = GetComponent<BotMover>();
            _oreContainer = GetComponent<BotOreContainer>();

            Freed?.Invoke(this);
        }

        private void OnEnable()
        {
            _oreContainer.OreGathered += OnOreGathered;
        }

        private void OnDisable()
        {
            _oreContainer.OreGathered -= OnOreGathered;
        }

        public void StartGather(Ore ore)
        {
            if (_state != State.Free)
                return;

            _state = State.Gather;
            _oreContainer.SetTraget(ore);
            _mover.MoveTo(ore.transform);
        }

        public void StartBuild(Transform target, Action targetReachedCallback)
        {
            if (_state != State.Free)
                return;

            _state = State.Build;
            _startPoint.position = target.position;
            _mover.MoveTo(target, targetReachedCallback);
        }

        public Ore HandOverOre()
        {
            Ore ore = _oreContainer.HandOverOre();
            ore.transform.SetParent(null);

            return ore;
        }

        public void SetFree()
        {
            _state = State.Free;
            Freed?.Invoke(this);
        }

        public void SetColor(Color color)
        {
            _meshRenderer.material.color = color;
        }

        private void OnOreGathered(Ore ore)
        {
            _state = State.Return;
            _mover.MoveTo(_startPoint);
        }
    }
}
