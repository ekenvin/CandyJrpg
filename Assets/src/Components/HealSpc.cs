using UnityEngine;
using System.Collections;

public class HealSpc : SpecialAttack {
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
		for(int i=0;i<manager.turnManager.allyOrder.Count;i++){
			Character curAlly=manager.turnManager.allyOrder[i];
			curAlly.HP+=manager.turnManager.partySprit;
		}
		manager.turnManager.partySprit=0;
		effect.SetActive(true);
		Invoke("hide",1.4f);
		return "Healed party for "+manager.turnManager.partySprit;
	}
	public void hide(){
		effect.SetActive(false);
	}
}
