﻿using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioSource worldMusic;
	public AudioSource battleMusic;
	public AudioSource bossMusic;
	public AudioSource winMusic;
	public AudioSource mainMusic;

	public AudioSource currentMusic;
	public AudioSource nextMusic;
	public bool fade;

	// Use this for initialization
	void Start () {
		init ();

		fade=false;
	}

	public void init(){
		worldMusic.volume=0;
		battleMusic.volume=0;
		bossMusic.volume=0;
		winMusic.volume=0;
		mainMusic.volume=1;
		currentMusic=mainMusic;
	}
	
	// Update is called once per frame
	void Update () {
		if(fade){

			currentMusic.volume-=0.01f;
			nextMusic.volume+=0.01f;
			Debug.Log("fading to "+nextMusic.gameObject.name);
			Debug.Log(currentMusic.volume +", "+ nextMusic.volume);
			if(currentMusic.volume<=0 && nextMusic.volume>=1){
				Debug.Log("music transitioned");
				currentMusic=nextMusic;
				fade=false;
			}
		}
	}

	public void fadeToMusic(AudioSource newMusic){

		nextMusic=newMusic;
		nextMusic.Play();
		fade=true;
	}
}
