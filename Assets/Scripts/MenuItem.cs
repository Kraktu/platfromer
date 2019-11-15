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
	public List<MenuItem> subMenuItems;
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
}

