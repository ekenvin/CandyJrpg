using UnityEngine;
using System.Collections;

public class OpeningScreen : MonoBehaviour {
	public GameObject hero;


	// Use this for initialization
	void Start () {
		Pauser p=GameObject.FindGameObjectWithTag("WorldManager").GetComponentInChildren<Pauser>();
		p.pause();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		this.gameObject.SetActive(false);
		AudioManager audioManager=GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
		audioManager.fadeToMusic(audioManager.worldMusic);
		hero.SetActive(true);
		Pauser p=GameObject.FindGameObjectWithTag("WorldManager").GetComponentInChildren<Pauser>();
		p.pause();
	}
}
