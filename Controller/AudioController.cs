using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : AudioElement {

	void Start()
	{
		int rnd = Random.Range(0, 3);
		
		app.Model.AudioModel.backGroundMusic[rnd].AudioSource.Play();
	}
	
	public void PlayStep()
	{
		app.Model.AudioModel.StepSound.AudioSource.Play();
	}

	public void PlayStepSequence()
	{
		app.Model.AudioModel.StepSequenceSound.AudioSource.Play();
	}
	
	public void PlayBallHit()
	{
		if (app.Model.AudioModel.HitBallSound.randomPitch)
		{
			float rndPitch = Random.Range(0.8f, 1.5f);
			app.Model.AudioModel.HitBallSound.AudioSource.pitch = rndPitch;
			app.Model.AudioModel.HitBallSound.AudioSource.Play();
		}
		else
		{
			app.Model.AudioModel.HitBallSound.AudioSource.Play();
		}
		
	}
	
	public void PlayBallHit(float volumeMultiplyer)
	{
		app.Model.AudioModel.HitBallSound.AudioSource.volume = app.Model.AudioModel.HitBallSound.volume * volumeMultiplyer;
		
		if (app.Model.AudioModel.HitBallSound.randomPitch)
		{
			float rndPitch = Random.Range(0.8f, 1.5f);
			app.Model.AudioModel.HitBallSound.AudioSource.pitch = rndPitch;
			app.Model.AudioModel.HitBallSound.AudioSource.Play();
		}
		else
		{
			app.Model.AudioModel.HitBallSound.AudioSource.Play();
		}
		
	}
	
	public void PlayBasketHit()
	{
		if (app.Model.AudioModel.HitBasketSound.randomPitch)
		{
			float rndPitch = Random.Range(0.8f, 1.5f);
			app.Model.AudioModel.HitBasketSound.AudioSource.pitch = rndPitch;
			app.Model.AudioModel.HitBasketSound.AudioSource.Play();
		}
		else
		{
			app.Model.AudioModel.HitBasketSound.AudioSource.Play();
		}
	}
	
	public void PlayGoal()
	{
		if (app.Model.AudioModel.GoalSound.randomPitch)
		{
			float rndPitch = Random.Range(0.8f, 1.5f);
			app.Model.AudioModel.GoalSound.AudioSource.pitch = rndPitch;
			app.Model.AudioModel.GoalSound.AudioSource.Play();
		}
		else
		{
			app.Model.AudioModel.GoalSound.AudioSource.Play();
		}
	}

	public void PlayConfusion()
	{
		if (app.Model.AudioModel.ConfusionSound.randomPitch)
		{
			float rndPitch = Random.Range(0.8f, 1.5f);
			app.Model.AudioModel.ConfusionSound.AudioSource.pitch = rndPitch;
			app.Model.AudioModel.ConfusionSound.AudioSource.Play();
		}
		else
		{
			app.Model.AudioModel.ConfusionSound.AudioSource.Play();
		}
	}
	
	public void PlayHitMan()
	{
		if (app.Model.AudioModel.HitManSound.randomPitch)
		{
			float rndPitch = Random.Range(0.9f, 1.2f);
			app.Model.AudioModel.HitManSound.AudioSource.pitch = rndPitch;
			app.Model.AudioModel.HitManSound.AudioSource.Play();
		}
		else
		{
			app.Model.AudioModel.HitManSound.AudioSource.Play();
		}
	}
	
	public void PlayGoalWow()
	{
		if (app.Model.AudioModel.GoalWowSound.randomPitch)
		{
			float rndPitch = Random.Range(0.9f, 1.2f);
			app.Model.AudioModel.GoalWowSound.AudioSource.pitch = rndPitch;
			app.Model.AudioModel.GoalWowSound.AudioSource.Play();
		}
		else
		{
			app.Model.AudioModel.GoalWowSound.AudioSource.Play();
		}
	}

	public void DecreaseCurrentPLayPitch()
	{
	}
	
	public void IncreaseCurrentPLayPitch()
	{
	}

}
