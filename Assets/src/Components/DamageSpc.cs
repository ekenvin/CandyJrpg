using UnityEngine;
using System.Collections;

public class DamageSpc : SpecialAttack {

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
			int dmg=manager.turnManager.partySprit*2;
			curEnemy.HP-=dmg;
			info+="Damaged "+curEnemy.mName+ " for "+dmg;
		}
		manager.turnManager.partySprit=0;
		return info;
	}
}
