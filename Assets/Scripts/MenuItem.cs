using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuAction
{
	GoDeeper,
	Credits,
	Quit,
	NewGame,
	Continue,
	Resolution,
	FullScreen
}

public class MenuItem : MonoBehaviour
{
	public string _label;
	public MenuAction action;
	public List<MenuItem> _subMenuItems;
	TextMesh _textMesh;
	private void Awake()
	{
		_textMesh = GetComponent<TextMesh>();
	}
	public void SetActive(Color color)
	{
		_textMesh.color = color;
	}
	public void SetInactive(Color color)
	{
		_textMesh.color = color;
	}
	public void InitAllSubItems()
	{
		foreach (MenuItem menuItem in _subMenuItems)
		{
			TextMesh textMesh = menuItem.GetComponent<TextMesh>();
			textMesh.text = menuItem._label;
			menuItem.Hide();
			menuItem.InitAllSubItems();
		}
	}
	public void ShowAllSubItems()
	{
		foreach (MenuItem menuItem in _subMenuItems)
		{
			menuItem.Show();
		}
	}
	public void Hide()
	{
		gameObject.SetActive(false);
	}
	public void Show()
	{
		gameObject.SetActive(true);

	}
}

