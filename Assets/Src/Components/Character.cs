using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public int HP;
	public int atk;
	public int def;
	public int spd;
	public int acc;
	public int spt;
	public string mName;

	public bool isDefending;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	public string attack(Character target){
		int hit=Random.Range(0,100);
		if(hit<acc){
			int dmg=(atk-target.def)/(isDefending?2:1);
			if(dmg<0)
				dmg=0;
			target.HP-=dmg;
			if(target.HP<0)
				target.HP=0;
			return mName+" Hits "+target.mName +" for "+dmg+"\n\t\t HP is now "+target.HP;
		}
		else{
			return mName+" misses "+target.mName;
			Debug.Log("MISS!");
		}
	}

	public void defend(){
		isDefending=true;
	}

	public string special(Character target){
		return GetComponent<SpecialAttack>().execute(target);
	}

	public void focus(){

	}
}
