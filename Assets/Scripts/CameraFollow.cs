using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	PlayerAccel _player;
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
				transform.position = new Vector3(
					_player.transform.position.x,
					_player.transform.position.y,
					transform.position.z);
			}
			yield return null;
		}
	}
}
