using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
	public string _sceneToLoad;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerAccel player = collision.GetComponent<PlayerAccel>();
		if (player!=null)
		{
			LoadNextScene(player);
		}
	}
	Coroutine loadNextSceneCoroutine;
	void LoadNextScene(PlayerAccel player)
	{
		if (loadNextSceneCoroutine==null)
		{
			loadNextSceneCoroutine = StartCoroutine(LoadNextSceneCoroutine(player));
		}
	}
	IEnumerator LoadNextSceneCoroutine(PlayerAccel player)
	{
		player.Freeze();
		GetComponent<Animator>().Play("EndPressed");
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(_sceneToLoad);

	}
}
