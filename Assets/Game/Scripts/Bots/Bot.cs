namespace Game
{
	using UnityEngine;

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
		}

		private void OnDestroy()
		{
			_oreContainer.OreGathered -= OnOreGatheredHandler;
		}

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
			_state = State.Free;
			return _oreContainer.HandOverOre();
		}

		private void OnOreGatheredHandler(Ore ore)
		{
			_state = State.Return;
			_mover.MoveTo(_startPoint);
		}
	}
}
