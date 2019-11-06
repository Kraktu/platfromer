using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	public Text _lifeText;
	public int _maxLifePoint;
	int _settedLifePoint;
	public Image _gameOverScreen;
	// Start is called before the first frame update
	void Awake()
	{
		Debug.Log("Start game");
		QualitySettings.vSyncCount = 0;  // VSync must be disabled to allow targetFrameRate
		Application.targetFrameRate = 60;
	}
	private void Start()
	{
		_settedLifePoint = _maxLifePoint;
		_lifeText.text = "x " + _maxLifePoint;
		_gameOverScreen.gameObject.SetActive(false);
	}
	public void GameOverScreen()
	{
		if (_maxLifePoint==0)
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
