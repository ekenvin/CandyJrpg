using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character {

	public Vector2 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string determineAction(List<Ally> allyParty){
		Debug.Log("Enemy Attack!");
		int rnd=Random.Range(0,allyParty.Count);
		return attack(allyParty[rnd]);
	}
}
