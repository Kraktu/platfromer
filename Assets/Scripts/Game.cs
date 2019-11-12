using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty { easy,normal,hard}
public class Game : MonoBehaviour
{
	public Difficulty _difficulty;
	static public Game Instance { get; private set; }
	// Start is called before the first frame update
	void Awake()
	{
		if (Instance!=null&& Instance!=this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
		Debug.Log("Start game");
		QualitySettings.vSyncCount = 0;  // VSync must be disabled to allow targetFrameRate
		Application.targetFrameRate = 60;
	}
}
