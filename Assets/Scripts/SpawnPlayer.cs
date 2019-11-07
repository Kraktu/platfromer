using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
	public PlayerAccel _playerToSpawn;

	private void Start()
	{
		Spawn();
	}
	public void Spawn()
	{
		Instantiate(_playerToSpawn, transform.position, Quaternion.identity);
	}
}
