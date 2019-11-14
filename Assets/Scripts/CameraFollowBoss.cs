
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBoss : MonoBehaviour
{
	public Boss _boss;
	private void Update()
	{
		transform.position = new Vector3(_boss.transform.position.x,_boss.transform.position.y+3,-10);
	}
}
