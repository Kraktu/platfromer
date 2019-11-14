using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
	PlayerAccel _player;
	Camera _camera;
	Vector3 _originalCameraPos;
	float _originalOrtSize;
	private void Start()
	{
		_camera = GetComponent<Camera>();
		_originalOrtSize = _camera.orthographicSize;
		StartCoroutine(FindingPlayer());
		StartCoroutine(CameraFollowingScreen());
	}
	IEnumerator FindingPlayer()
	{
		while (true)
		{
			if (_player == null)
			{
				_player = FindObjectOfType<PlayerAccel>();
			}
			yield return null;
		}
	}
	IEnumerator CameraFollowingScreen()
	{
		while (true)
		{
			if (_player != null)
			{
				if (_player.transform.position.y > transform.position.y + _camera.orthographicSize)
				{
					transform.Translate(0, _camera.orthographicSize * 2, 0);
				}
				if (_player.transform.position.y < transform.position.y - _camera.orthographicSize - 2)
				{
					transform.Translate(0, -_camera.orthographicSize * 2, 0);
				}
				if (_player.transform.position.x > transform.position.x + _camera.orthographicSize * _camera.aspect)
				{
					transform.Translate((_camera.orthographicSize * _camera.aspect * 2), 0, 0);
				}
				if (_player.transform.position.x < transform.position.x - _camera.orthographicSize * _camera.aspect)
				{
					transform.Translate((-_camera.orthographicSize * _camera.aspect * 2), 0, 0);
				}
			}
			yield return null;
		}
	}
	IEnumerator CameraFollowingPlayerWhileZoom()
	{
		while (true)
		{
			_camera.transform.position = _player.transform.position;
			yield return null;
		}
	}
	Coroutine _zoomCoroutine,_deZoomCoroutine;
	public void Zoom(float magnitude, float duration = 1f)
	{
		if (_zoomCoroutine == null)
		{
			_zoomCoroutine = StartCoroutine(ZoomCoroutine(magnitude, duration));
		}

	}
	public void DeZoom(float duration)
	{
		_deZoomCoroutine = StartCoroutine(DeZoomCoroutine(duration));
	}
	IEnumerator ZoomCoroutine(float magnitude, float duration)
	{
		float initialValue = _camera.orthographicSize;
		float finalValue = _originalOrtSize / magnitude;
		Vector3 initialVector = _camera.transform.position;
		Vector3 finalVector = _player.transform.position;
		float time=0;
		if (_deZoomCoroutine!=null)
		{
			StopCoroutine(_deZoomCoroutine);
			_deZoomCoroutine = null;
		}
		StopCoroutine(CameraFollowingScreen());
		while (time<duration)
		{
			time += Time.deltaTime;
			_camera.orthographicSize = Mathf.Lerp(initialValue, finalValue, time/duration);
			_camera.transform.position = Vector3.Lerp(initialVector, finalVector, time / duration);
			yield return null;
		}
		_camera.orthographicSize = finalValue;
		StartCoroutine(CameraFollowingPlayerWhileZoom());

		_originalCameraPos = _camera.transform.position;
		


		yield return null;
		_zoomCoroutine = null;

	}
	IEnumerator DeZoomCoroutine(float duration = 1f)
	{
		float initialValue = _camera.orthographicSize;
		float finalValue = _originalOrtSize;
		Vector3 initialVector = _camera.transform.position;
		Vector3 finalVector = _originalCameraPos;

		float time = 0;
		if (_zoomCoroutine!=null)
		{
			StopCoroutine(_zoomCoroutine);
			_zoomCoroutine = null;
		}
		_camera.transform.position = _originalCameraPos;
		StopCoroutine(CameraFollowingPlayerWhileZoom());
		while (time < duration)
		{
			time += Time.deltaTime;
			_camera.orthographicSize = Mathf.Lerp(initialValue, finalValue, time / duration);
			_camera.transform.position = Vector3.Lerp(initialVector, finalVector, time / duration);
			yield return null;
		}
		_camera.orthographicSize = finalValue;
		StartCoroutine(CameraFollowingScreen());

		yield return null;
		_deZoomCoroutine = null;
	}
}

