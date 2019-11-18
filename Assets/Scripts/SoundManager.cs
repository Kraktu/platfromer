using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[Header("AudioSources")]
	public AudioSource _aSourceSFX, _aSourceMusic;

	//public float _sfxVolume;
	static public SoundManager Instance { get; private set; }
	public List<AudioClipStruct> _soundEffects = new List<AudioClipStruct>();
	[Header("Listof sound effect clips")]
	Dictionary<string, AudioClip> _soundEffectsDict = new Dictionary<string, AudioClip>();
	
	[System.Serializable]
	public struct AudioClipStruct
	{
		public string name;
		public AudioClip clip;
	}
	private void Update()
	{
		//_aSourceSFX.outputAudioMixerGroup.audioMixer.SetFloat("SFXVolume", _sfxVolume); 
	}
	// Start is called before the first frame update
	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
	private void Start()
	{
		GenerateSoundEffectDict();
	}
	void GenerateSoundEffectDict()
	{
		foreach (AudioClipStruct audioClip in _soundEffects)
		{
			_soundEffectsDict.Add(audioClip.name, audioClip.clip);
		}
	}
	public void PlaySoundEffect(string clipName , float pitch=1)
	{
		_aSourceSFX.pitch = pitch;
		_aSourceSFX.PlayOneShot(_soundEffectsDict[clipName]);
	}
	public void ChangeMusicPitch(float pitch)
	{
		_aSourceMusic.pitch = pitch;
	}
}
