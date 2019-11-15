using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControler : MonoBehaviour
{
	public List<MenuItem> _menuItems;
	public Color _activeColor, _inactiveColor;
	MenuItem _activeMenuItem;
	int _activeIndex;
    void Start()
    {
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
