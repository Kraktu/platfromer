using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public Camera _bossCamera;
	public int _lifePoint=1;
	public GameObject _enemyPrefab,_endLevel;
	public string _sceneToLoadWhenBossDead;
	[HideInInspector]
	public SpriteRenderer _sprite;
	[HideInInspector]
	public Coroutine _bossCameraCoroutine;

	private void Start()
	{
		_sprite = GetComponent<SpriteRenderer>();
		transform.localScale = new Vector3(_lifePoint, _lifePoint, 1);
		_sprite.color = Color.white;
	}
	public void OnLooseLife()
	{
		_lifePoint--;
		transform.localScale = new Vector3(_lifePoint, _lifePoint, 1);
		_sprite.color = new Color(1, 1/(_lifePoint)*0.7f, 1/ (_lifePoint) * 0.7f, 1);
		GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
		if (_lifePoint == 0)
		{
			KillTheBoss();
		}
	}
	void KillTheBoss()
	{
		GameObject _end = Instantiate(_endLevel, transform.position+Vector3.up*3, Quaternion.identity);
		_end.GetComponent<EndLevel>()._sceneToLoad = _sceneToLoadWhenBossDead;
		Destroy(gameObject);
	}
	public void StartBossCameraCoroutine()
	{
		_bossCameraCoroutine = StartCoroutine(BossCameraCoroutine());
	}
	 IEnumerator BossCameraCoroutine()
	{
		_bossCamera.depth = 2;
		yield return new WaitForSeconds(2f);
		_lifePoint++;
		transform.localScale = new Vector3(_lifePoint, _lifePoint, 1);
		_sprite.color = new Color(1, 1 /_lifePoint, 1 / _lifePoint, 1);
		yield return new WaitForSeconds(2f);
		_bossCamera.depth = 0;
	}
}
