using UnityEngine;
using System.Collections;

public class DefendSpc : SpecialAttack {

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
			curAlly.isDefending=true;
		}
		manager.turnManager.partySprit=0;
		effect.SetActive(true);
		Invoke("hide",1.4f);
		return "Pepper Mince is defending the party.";
	}
	public void hide(){
		Scaler[] scalers= effect.GetComponentsInChildren<Scaler>();
		for(int i=0;i<scalers.Length;i++){
			scalers[i].reset();
		}
		effect.SetActive(false);
	}
}
