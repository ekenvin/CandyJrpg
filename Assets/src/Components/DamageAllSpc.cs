using UnityEngine;
using System.Collections;

public class DamageAllSpc : SpecialAttack {
	public GameObject effect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override string execute(Character target){
		BattleManager manager=GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
		Character character=GetComponent<Character>();
		string info="";
		for(int i=0;i<manager.turnManager.enemyOrder.Count;i++){
			Character curEnemy=manager.turnManager.enemyOrder[i];
			int dmg=manager.turnManager.partySprit-(curEnemy.def/2);
			if(dmg<=10)
				dmg=10;
			curEnemy.HP-=dmg;
			info+="Damaged "+curEnemy.mName+ " for "+dmg+"\n";
		}
		effect.SetActive(true);
		Invoke("hide",1.4f);
		manager.turnManager.partySprit=0;
		return info;
	}
	public void hide(){
		effect.GetComponent<Scaler>().reset();
		effect.SetActive(false);
	}
}
