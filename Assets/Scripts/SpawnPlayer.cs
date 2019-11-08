using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
	Animator _spawnAnimator;
	public PlayerAccel _playerToSpawn;

	public Text _lifeText;
	public int _maxLifePoint;
	int _settedLifePoint;
	public Image _gameOverScreen;

	private void Start()
	{
		_spawnAnimator = GetComponentInParent<Animator>();
		_settedLifePoint = _maxLifePoint;
		_lifeText.text = "x " + _maxLifePoint;
		_gameOverScreen.gameObject.SetActive(false);
		Spawn();
	}
	public void Spawn()
	{
		Instantiate(_playerToSpawn, transform.position, Quaternion.identity);
		_spawnAnimator.Play("StartMoving");
	}
	public void ReSpawn()
	{
		Instantiate(_playerToSpawn, transform.position, Quaternion.identity);
		_spawnAnimator.Play("StartMoving");
		_maxLifePoint--;
		_lifeText.text = "x " + _maxLifePoint;
		GameOverScreen();
	}
	public void GameOverScreen()
	{
		if (_maxLifePoint == 0)
		{
			_gameOverScreen.gameObject.SetActive(true);
		}
	}
	public void Retry()
	{
		_maxLifePoint = _settedLifePoint;
		_lifeText.text = "x " + _maxLifePoint;
		_gameOverScreen.gameObject.SetActive(false);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
