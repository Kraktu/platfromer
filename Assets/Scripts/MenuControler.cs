using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
	public TextMesh _easterEgg;
	public List<MenuItem> _menuItems;
	List<MenuItem> _initialMenuItems;
	public Color _activeColor, _inactiveColor;
	MenuItem _activeMenuItem;
	int _activeIndex,_activeResolution;
	Vector2[] Resolution = new Vector2[3];
	bool _isFullScreen = false;
	void Start()
    {
		Resolution[0] = new Vector2(1920, 1080);
		Resolution[1] = new Vector2(1280, 720);
		Resolution[2] = new Vector3(720, 405);
		_initialMenuItems = _menuItems;
		_activeResolution = 1;
		InitMenu();   
    }

    // Update is called once per frame
    void Update()
    {
		MenuNavigation();
	}
	void InitMenu()
	{
		_activeIndex = 0;
		_activeMenuItem = _menuItems[_activeIndex];
		foreach (MenuItem menuItem in _menuItems)
		{
			TextMesh textMesh = menuItem.GetComponent<TextMesh>();
			textMesh.text = menuItem._label;
			menuItem.SetInactive(_inactiveColor);
			menuItem.InitAllSubItems();
			if (menuItem.action== MenuAction.Resolution)
			{
				menuItem.GetComponentInChildren<TextMesh>().text = "Resolution : " + Resolution[_activeResolution].x + " X " + Resolution[_activeResolution].y;
			}
		}
		_activeMenuItem.SetActive(_activeColor);
	}
	void MenuNavigation()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
		{
			NavigateUpDown(goDown: true);
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z))
		{
			NavigateUpDown(goDown: false);
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			MenuSelected();
		}
		if (_activeMenuItem.action== MenuAction.Resolution&& (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q)))
		{

			ActionResolution(left: true);
		}
		if (_activeMenuItem.action == MenuAction.Resolution && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
		{
			ActionResolution(left: false);
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			foreach (MenuItem menuItem in _menuItems)
			{

				TextMesh textMesh = menuItem.GetComponent<TextMesh>();
				textMesh.fontSize = 0;
			}
			_menuItems = _initialMenuItems;
			foreach (MenuItem menuItem in _menuItems)
			{

				TextMesh textMesh = menuItem.GetComponent<TextMesh>();
				textMesh.fontSize = 200;

			}
			InitMenu();
		}
		#region EasterEgg
		if (Input.GetKey(KeyCode.P))
		{
			if (Input.GetKey(KeyCode.O))
			{
				if (Input.GetKey(KeyCode.U))
				{
					if (Input.GetKey(KeyCode.E))
					{
						if (Input.GetKey(KeyCode.T))
						{
							_easterEgg.gameObject.SetActive(true);
						}
					}
				}
			}
		}
		if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.O) || Input.GetKeyUp(KeyCode.U) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.T))
		{
			_easterEgg.gameObject.SetActive(false);
		}
		#endregion
	}
	void MenuSelected()
	{
		switch (_activeMenuItem.action)
		{
			case MenuAction.GoDeeper:
				ActionGoDeeper();
				break;
			case MenuAction.Credits:
				ActionCredits();
				break;
			case MenuAction.Quit:
				ActionQuit();
				break;
			case MenuAction.NewGame:
				ActionNewGame();
				break;
			case MenuAction.Continue:
				ActionContinue();
				break;
			case MenuAction.FullScreen:
				ActionFullScreen();
				break;
			case MenuAction.Resolution:
				ApplyResolution();
				break;
			default:
				break;
		}
	}

	#region Actions
	void ActionGoDeeper()
	{
		
		_activeMenuItem.ShowAllSubItems();
		foreach (MenuItem menuItem in _menuItems)
		{
			
			TextMesh textMesh = menuItem.GetComponent<TextMesh>();
			textMesh.fontSize = 0;
		
		}
		_menuItems = _activeMenuItem._subMenuItems;
		foreach (MenuItem menuItem in _menuItems)
		{

			TextMesh textMesh = menuItem.GetComponent<TextMesh>();
			textMesh.fontSize = 200;

		}
		InitMenu();
	}
	void ActionCredits()
	{
		SceneManager.LoadScene("credits");
	}
	void ActionQuit()
	{
		Application.Quit();
	}
	void ActionNewGame()
	{
		SceneManager.LoadScene("level1");
	}
	void ActionContinue()
	{

	}
	void ActionResolution(bool left)
	{
		if (left==true)
		{
			_activeResolution--;
			if (_activeResolution<0)
			{
				_activeResolution = Resolution.Length - 1;
			}
		}
		if (left==false)
		{
			_activeResolution++;
			if (_activeResolution > Resolution.Length-1)
			{
				_activeResolution = 0;
			}
		}
		_activeMenuItem.GetComponentInChildren<TextMesh>().text = "Resolution : " + Resolution[_activeResolution].x + " X " + Resolution[_activeResolution].y;
	}
	#endregion

	void ApplyResolution()
	{
		Screen.SetResolution((int)Resolution[_activeResolution].x, (int)Resolution[_activeResolution].y, _isFullScreen) ;
	}
	void ActionFullScreen()
	{
		_isFullScreen = !_isFullScreen;
		Screen.SetResolution((int)Resolution[_activeResolution].x, (int)Resolution[_activeResolution].y, _isFullScreen);
	}
	private void NavigateUpDown(bool goDown)
	{
		if (goDown)
		{
			_activeIndex++;
		}
		if (!goDown)
		{
			_activeIndex--;
		}
		if (_activeIndex<0)
		{
			_activeIndex= _menuItems.Count - 1;
		}
		if (_activeIndex>= _menuItems.Count)
		{
			_activeIndex = 0;
		}
		_activeMenuItem.SetInactive(_inactiveColor);
		_activeMenuItem = _menuItems[_activeIndex];
		_activeMenuItem.SetActive(_activeColor);
	}
}
