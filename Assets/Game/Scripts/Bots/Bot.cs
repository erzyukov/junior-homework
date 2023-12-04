namespace Game
{
	using UnityEngine;
	using UnityEngine.Events;

	[RequireComponent(typeof(BotMover))]
    public class Bot : MonoBehaviour
    {
		private State _state;
		private Transform _startPoint;
		private BotMover _mover;
		private BotOreContainer _oreContainer;

		private void Start()
		{
			_startPoint = new GameObject("StartMark").GetComponent<Transform>();
			_startPoint.position = transform.position;

			_mover = GetComponent<BotMover>();
			_oreContainer = GetComponent<BotOreContainer>();

			_oreContainer.OreGathered += OnOreGatheredHandler;

			Freed?.Invoke(this);
		}

		private void OnDestroy()
		{
			_oreContainer.OreGathered -= OnOreGatheredHandler;
		}

		public event UnityAction<Bot> Freed;

		public enum State
		{
			Free,
			Gather,
			Return
		}

		public bool IsFree => _state == State.Free;

		public void StartGather(Ore ore)
		{
			_state = State.Gather;
			_oreContainer.SetTraget(ore);
			_mover.MoveTo(ore.transform);
		}

		public bool HasOre => _state == State.Return && _oreContainer.HasTarget;

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

		private void OnOreGatheredHandler(Ore ore)
		{
			_state = State.Return;
			_mover.MoveTo(_startPoint);
		}
	}
}
