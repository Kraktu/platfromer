using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwitch : MonoBehaviour
{
	public Boss _boss;
	public float _delayBeforeDestruction, _delayBeforeReconstruction, _magnitude;
	public GameObject[] _objectToRemove;
	BoxCollider2D _boxCollider;

	private void Start()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_boxCollider.enabled = false;
			SoundManager.Instance.PlaySoundEffect("BossSwitchActivated");
			StartCoroutine(HitBoss());
		}
	}
	IEnumerator HitBoss()
	{
		StartCoroutine(Shake(_delayBeforeDestruction));
		yield return new WaitForSeconds(_delayBeforeDestruction);
		for (int i = 0; i < _objectToRemove.Length; i++)
		{
			_objectToRemove[i].SetActive(false);
		}

		yield return new WaitForSeconds(_delayBeforeReconstruction);
		for (int i = 0; i < _objectToRemove.Length; i++)
		{
			_objectToRemove[i].SetActive(true);
		}
		transform.position = _objectToRemove[(int)Random.Range(0, _objectToRemove.Length - 1)].transform.position+Vector3.up*1.5f;
		_boss.OnLooseLife();
		_boxCollider.enabled = true;


		yield return null;
	}

	IEnumerator Shake(float delay)
	{

		float elapsed = 0.0f;

		Vector3 originalCamPos = Camera.main.transform.position;

		while (elapsed < delay)
		{

			elapsed += Time.deltaTime;

			float percentComplete = elapsed / delay;
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= _magnitude * damper;
			y *= _magnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z)+originalCamPos;

			yield return null;
		}

		Camera.main.transform.position = originalCamPos;

	}
}
