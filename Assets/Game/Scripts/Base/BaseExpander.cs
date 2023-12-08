namespace Game
{
	using System;
	using UnityEngine;
	using UnityEngine.EventSystems;

    public class BaseExpander : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private GameObject _flagPrefab;
		[SerializeField] private LayerMask _groundLayerMask;

		private Base _base;

		private Camera _camera;
		private Transform _flag;
		private Vector3 _buildTargetPosition;
		private State _state;

		private Ray _ray;
		private RaycastHit _hitData;

		private void Awake()
		{
			_base = GetComponent<Base>();
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
					_buildTargetPosition = _hitData.point;
					_flag.position = _buildTargetPosition;

					if (_state == State.Initiated)
						StartBuildProcess();
				}
			}
		}

		private void StartBuildProcess()
		{
			_base.SetState(Base.State.BuildBase);
			_state = State.TargetPlaced;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (_state != State.None)
				return;

			_flag = Instantiate(_flagPrefab, eventData.pointerCurrentRaycast.worldPosition, Quaternion.identity).transform;
			_state = State.Initiated;
		}

		public enum State
		{
			None,
			Initiated,
			TargetPlaced,
			WaitFreeBot,
			Building,
		}
    }
}
