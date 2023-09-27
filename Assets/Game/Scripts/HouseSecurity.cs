using System.Collections;
using UnityEngine;

namespace Game
{
    public class HouseSecurity : MonoBehaviour
    {
		[SerializeField] private AudioSource _alarm;
		[SerializeField] private float _changeVolumeSpeed;

		private Coroutine _alarmVolumeChanger;

		private void Start()
		{
			_alarm.volume = 0;
		}

		private void OnTriggerEnter(Collider other) =>
			RunAlarm();

		private void OnTriggerExit(Collider other) =>
			StopAlarm();

		private void RunAlarm()
		{
			ResetVolumeChanger();
			_alarmVolumeChanger = StartCoroutine(ChangeVolume(_changeVolumeSpeed));
		}

		private void StopAlarm()
		{
			ResetVolumeChanger();
			_alarmVolumeChanger = StartCoroutine(ChangeVolume(-_changeVolumeSpeed));
		}

		private IEnumerator ChangeVolume(float speed)
		{
			if (_alarm.volume == 0)
				_alarm.Play();

			float maxVolumeValue = 1f;

			while (_alarm.volume != maxVolumeValue || _alarm.volume != 0)
			{
				_alarm.volume = Mathf.MoveTowards(_alarm.volume, maxVolumeValue, speed * Time.deltaTime);

				yield return null;
			}

			if (_alarm.volume == 0)
				_alarm.Stop();
		}

		private void ResetVolumeChanger()
		{
			if (_alarmVolumeChanger != null)
				StopCoroutine(_alarmVolumeChanger);
		}
	}
}