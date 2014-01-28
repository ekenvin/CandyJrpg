using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void determineAction(List<Ally> allyParty){
		Debug.Log("Enemy Attack!");
		int rnd=Random.Range(0,allyParty.Count);
		attack(allyParty[rnd]);
	}
}
