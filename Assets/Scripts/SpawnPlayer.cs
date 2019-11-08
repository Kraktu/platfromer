using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
	Animator _spawnAnimator;
	public PlayerAccel _playerToSpawn;

	private void Start()
	{
		_spawnAnimator = GetComponentInParent<Animator>();
		Spawn();
	}
	public void Spawn()
	{
		Instantiate(_playerToSpawn, transform.position, Quaternion.identity);
		_spawnAnimator.Play("StartMoving");
	}
}
