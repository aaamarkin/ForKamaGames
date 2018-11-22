using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
	public AudioClip audioClip;

	[HideInInspector]
	public float volume;
//	[Range(0.8f, 1.5f)]
//	public float pitch;

	public bool randomPitch;

	[HideInInspector] public AudioSource AudioSource;

}

public class AudioModel : AppElement
{

	public Sound[] backGroundMusic;
	
	[Space(10)]
	
	public Sound StepSound;
	public Sound StepSequenceSound;
	
	public Sound HitBallSound;
	public Sound HitBasketSound;
	public Sound GoalSound;
	public Sound ConfusionSound;
	public Sound HitManSound;
	public Sound GoalWowSound;
	
	public override void LastInAwake()
	{
		foreach (var sound in backGroundMusic)
		{
			CreateAudioSource(sound, true, 0, 0.01f, 1f);
		}
		
		CreateAudioSource(StepSound, false);
		CreateAudioSource(StepSequenceSound, false);
		CreateAudioSource(HitBallSound, false, 0.01f, 0.3f, 1);
		CreateAudioSource(HitBasketSound, false);
		CreateAudioSource(GoalSound, false, 2f);
		CreateAudioSource(ConfusionSound, false, 0, 0.8f, 1);
		CreateAudioSource(HitManSound, false, 0f, 0.01f, 1);
		CreateAudioSource(GoalWowSound, false);
	}

	private void CreateAudioSource(Sound sound, bool loop, float time = 0, float volume = 0.3f, float pitch = 1)
	{
		sound.AudioSource = gameObject.AddComponent<AudioSource>();
		sound.AudioSource.clip = sound.audioClip;
		sound.AudioSource.volume = volume;
		sound.AudioSource.pitch = pitch;
		sound.AudioSource.loop = loop;
		sound.AudioSource.time = time;

		sound.volume = volume;
	}
}
