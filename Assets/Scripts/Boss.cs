using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public Camera _bossCamera;
	public int _lifePoint = 1;
	public GameObject _enemyPrefab, _endLevel;
	public string _sceneToLoadWhenBossDead;
	[HideInInspector]
	public SpriteRenderer _sprite;
	[HideInInspector]
	public Coroutine _bossCameraCoroutine;
	public float _animationDuration;

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
		_sprite.color = new Color(1, 1 / (_lifePoint) * 0.7f, 1 / (_lifePoint) * 0.7f, 1);
		GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
		if (_lifePoint == 0)
		{
			KillTheBoss();
		}
	}
	void KillTheBoss()
	{
		GameObject _end = Instantiate(_endLevel, transform.position + Vector3.up * 3, Quaternion.identity);
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
		yield return new WaitForSeconds(1f);
		_lifePoint++;
		StartCoroutine(AnimateBoss());
		yield return new WaitForSeconds(2f);
		_bossCamera.depth = 0;
	}

	public AnimationCurve _scaleAnimation;
	public AnimationCurve _colorAnimation;
	IEnumerator AnimateBoss()
	{
		float t = 0;
		float tRatio;
		float tScaled;
		float tColored;

		Vector3 startScale = transform.localScale;
		Vector3 endScale = new Vector3(_lifePoint*0.7f, _lifePoint*0.7f, 1);
		Color startColor = _sprite.color;
		Color endColor = new Color(1, 1 / (_lifePoint) * 0.7f, 1 / (_lifePoint) * 0.7f, 1);
		SoundManager.Instance.PlaySoundEffect("ChickenBossGrow",1-_lifePoint*0.1f);
		SoundManager.Instance.ChangeMusicPitch(0.9f + _lifePoint * 0.1f);
		while (t < _animationDuration)
			{
				tRatio = t / _animationDuration; ;
				tScaled = _scaleAnimation.Evaluate(tRatio);
				

				tColored = _colorAnimation.Evaluate(tRatio);

				transform.localScale = Vector3.Lerp(startScale, endScale, tScaled);

				_sprite.color = Color.Lerp(startColor, endColor, tColored);

				t += Time.deltaTime;
				yield return null;
			}
			transform.localScale = endScale;
			_sprite.color = startColor;
	}
}
