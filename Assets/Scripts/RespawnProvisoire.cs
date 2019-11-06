using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnProvisoire : MonoBehaviour
{
	public Game _game;
	public GameObject _playerPrefab;
	

	void Update()
    {
		if (transform.position.y<-12)
		{
			Instantiate(_playerPrefab, new Vector3(-18, -4, -3), Quaternion.identity);
			_game._maxLifePoint--;
			_game._lifeText.text = "x " + _game._maxLifePoint;
			_game.GameOverScreen();
			Destroy(this.gameObject);
		}
    }
}
