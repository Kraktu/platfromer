using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
	PlayerAccel _player;
	Camera _camera;
	private void Start()
	{
		_camera = GetComponent<Camera>();
		StartCoroutine(FindingPlayer());
		StartCoroutine(CameraFollowingPlayer());
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
	IEnumerator CameraFollowingPlayer()
	{
		while (true)
		{
			if (_player != null)
			{
				if (_player.transform.position.y>transform.position.y+_camera.orthographicSize)
				{
					transform.Translate(0, _camera.orthographicSize*2, 0);
				}
				if (_player.transform.position.y < transform.position.y - _camera.orthographicSize-3)
				{
					transform.Translate(0, -_camera.orthographicSize * 2, 0);
				}
				if (_player.transform.position.x>transform.position.x+_camera.orthographicSize*_camera.aspect)
				{
					transform.Translate(_camera.orthographicSize * _camera.aspect*2, 0, 0);
				}
				if (_player.transform.position.x < transform.position.x - _camera.orthographicSize * _camera.aspect)
				{
					transform.Translate(-_camera.orthographicSize * _camera.aspect*2, 0, 0);
				}
			}
			yield return null;
		}
	}
}

