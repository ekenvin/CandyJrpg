using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public int HP;
	public int atk;
	public int def;
	public int spd;
	public int acc;
	public int spt;

	public bool isDefending;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	public void attack(Character target){
		int hit=Random.Range(0,100);
		if(hit<acc){
			int dmg=(atk-target.def)/(isDefending?2:1);
			target.HP-=dmg;
			if(target.HP<0)
				target.HP=0;
		}
		else{
			Debug.Log("MISS!");
		}
	}

	public void defend(){
		isDefending=true;
	}

	public void special(Character target){

	}

	public void focus(){

	}
}
