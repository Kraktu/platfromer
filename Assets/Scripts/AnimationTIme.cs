using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTIme : MonoBehaviour
{
	Animator _anim;
	Dictionary<string,float> _animationClipsTime = new Dictionary<string,float>();
	private void Start()
	{
		_anim = GetComponent<Animator>();
		CalculateAnimationTime();
	}
	void CalculateAnimationTime()
	{
		AnimationClip[] clips = _anim.runtimeAnimatorController.animationClips;
		foreach (AnimationClip clip in clips)
		{
			_animationClipsTime.Add(clip.name, clip.length);
		}
		foreach (KeyValuePair<string,float> clipTime in _animationClipsTime)
		{
			//Debug.Log("key: " + clipTime.Key + "Value: " + clipTime.Value);
		}
	}
	public float GetTime(string clipName)
	{
		return _animationClipsTime[clipName];
	}
}
