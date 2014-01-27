﻿using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {
	private bool paused = false;
	
	// Update is called once per frame
	void Update () {

		if(paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void pause(){
		paused = !paused;
	}
}
