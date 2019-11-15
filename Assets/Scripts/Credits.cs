
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
	int _rotation=50;
	public string[] _sentence;
	int _choosedSentence;

	private void Start()
	{
		_choosedSentence = 0;
	}
	void Update()
    {
		transform.Rotate(0, 0, _rotation * Time.deltaTime);
		
		if (Input.GetKeyDown(KeyCode.U))
		{
			SceneManager.LoadScene("MainMenu");
		}
		if (Input.GetKey(KeyCode.Mouse0))
		{
			_rotation = 100;
		}
		if(Input.GetKeyUp(KeyCode.Mouse0))
		{
			_rotation = 50;
		}
		if(Input.GetKeyDown(KeyCode.Mouse1))
		{
			_choosedSentence=(int)Random.Range(0,_sentence.Length);

			GetComponent<TextMesh>().text = _sentence[_choosedSentence];
		}
    }
}
