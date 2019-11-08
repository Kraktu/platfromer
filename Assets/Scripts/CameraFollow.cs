using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	PlayerAccel _player;
	public Camera _camera;
	private void Start()
	{
		StartCoroutine(FindingPlayer());
		StartCoroutine(CameraFollowingPlayer());
	}
	IEnumerator FindingPlayer()
	{
		while (true)
		{
			if (_player==null)
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
			if (_player!=null)
			{
				transform.localPosition = new Vector3(_camera.transform.position.x - _player.transform.position.x * 0.1f, _camera.transform.position.y,0);
			}
			yield return null;
		}
	}
}
