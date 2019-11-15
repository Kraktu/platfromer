using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
	public List<MenuItem> _menuItems;
	List<MenuItem> _initialMenuItems;
	public Color _activeColor, _inactiveColor;
	MenuItem _activeMenuItem;
	int _activeIndex;
    void Start()
    {
		_initialMenuItems = _menuItems;
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
			case MenuAction.Resolution:
				ActionResolution();
				break;
			case MenuAction.FullScreen:
				ActionFullScreen();
				break;
			default:
				break;
		}


	}
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
	void ActionResolution()
	{

	}
	void ActionFullScreen()
	{

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
