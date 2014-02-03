using UnityEngine;
using System.Collections;

public class SpecialAttack : MonoBehaviour {

	public int requiredSpt;
	public string mName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual string execute(Character target){
		return "Default Special";
	}
}
