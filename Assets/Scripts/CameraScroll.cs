using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
	public Camera _camera;
	public GameObject _background;
	bool _isUpTriggered = false;

	public void CameraScrollUp()
	{
		if (_isUpTriggered==false)
		{
			_camera.transform.Translate(new Vector3(0, 23, 0));
			_background.transform.Translate(new Vector3(0, 23, 0));
			_isUpTriggered = true;
		}
	}
	public void ResetCameraTriggerUp()
	{
		_isUpTriggered = false;
	}
}
