using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckPoint : MonoBehaviour
{
	public GameObject[] _TilesToActivate;
	public GameObject _startPoint;
	public GameObject _background;
	public Camera _camera;
	public float _animCameraDuration;
	public float _endCameraSize;

	//private void Start()
	//{
	//	StartCoroutine(FindingPlayer());
	//}
	//IEnumerator FindingPlayer()
	//{
	//	while (true)
	//	{
	//		if (_player == null)
	//		{
	//			_player = FindObjectOfType<PlayerAccel>();
	//		}
	//		yield return null;
	//	}
	//}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerAccel player = collision.gameObject.GetComponent<PlayerAccel>();
		if (player != null)
		{
			_camera.depth=2;
			_background.SetActive(true);
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			player.Freeze();
			for (int i = 0; i < _TilesToActivate.Length; i++)
			{
				_TilesToActivate[i].SetActive(true);
			}
			_startPoint.transform.position = new Vector3(58.5f, 37.5f, 0);
			SoundManager.Instance.ChangeMusic("BossMusic");
			StartCoroutine(CameraAnimation(player));

		}
	}
	IEnumerator CameraAnimation(PlayerAccel player)
	{
		float time = 0;
		Vector3 startPosition = _camera.transform.position;
		Vector3 endPosition = new Vector3(-17.5f,45,-10);
		Vector3 originalSize = _background.transform.localScale;
		Vector3 endSize = new Vector3(0.5f, 0.5f, 0);
		float startingCameraSize = _camera.orthographicSize;
		float endingCameraSize=_endCameraSize;
		while (time< _animCameraDuration)
		{
			float ratio = time / _animCameraDuration;
			_camera.transform.position = Vector3.Lerp(startPosition, endPosition, ratio);
			_camera.orthographicSize = Mathf.Lerp(startingCameraSize, endingCameraSize, ratio);
			_background.transform.localScale = Vector3.Lerp(originalSize, endSize, ratio);

			time += Time.deltaTime;
			yield return null;
		}
		_camera.transform.position = endPosition;
		_camera.orthographicSize = endingCameraSize;
		_background.transform.localScale = endSize;
		time = 0;
		SoundManager.Instance.PlaySoundEffect("PowerUp01");
		yield return new WaitForSeconds(2);
		while (time<_animCameraDuration/2)
		{
			float ratio = time / (_animCameraDuration/2);
			_camera.transform.position = Vector3.Lerp(endPosition, startPosition, ratio);
			_camera.orthographicSize = Mathf.Lerp(endingCameraSize, startingCameraSize, ratio);
			_background.transform.localScale = Vector3.Lerp(endSize, originalSize, ratio);
			time += Time.deltaTime;
			yield return null;
		}
		_camera.transform.position = startPosition;
		_camera.orthographicSize = startingCameraSize;
		_background.transform.localScale = originalSize;
		player.UnFreeze();
		_camera.depth=0;
		_background.SetActive(false);
	}
}
