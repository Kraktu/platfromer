using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour
{
	public GameObject[] _platformsToRemove;
	public GameObject[] _platformsToActivate;
	public bool _isSwitch;
	[HideInInspector]
	public bool _hasBeenTriggered = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag== "Player")
		{
			if (!_hasBeenTriggered)
			{
				SwitchState(_platformsToRemove, _platformsToActivate);
			}
			else if (_hasBeenTriggered)
			{
				SwitchState(_platformsToActivate, _platformsToRemove);
			}

			_hasBeenTriggered = !_hasBeenTriggered;

			if (!_isSwitch)
			{
				GetComponent<BoxCollider2D>().enabled = false;
			}
			SoundManager.Instance.PlaySoundEffect("SwitchActivated");
		}
	}

	void SwitchState(GameObject[] gameObjectsToDestroy,GameObject[] gameObjectsToActivate)
	{
		for (int i = 0; i < gameObjectsToActivate.Length; i++)
		{
			gameObjectsToActivate[i].SetActive(true);
		}
		for (int i = 0; i < gameObjectsToDestroy.Length; i++)
		{
			gameObjectsToDestroy[i].SetActive(false);
		}
	}
}
