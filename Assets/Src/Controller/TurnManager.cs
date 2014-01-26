using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {

	private List<Character> turnOrder;
	private BattleManager mBattleManager;

	public GameObject battleUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(List<Character> order,BattleManager manager){
		turnOrder=order;
		mBattleManager=manager;
	}

	public void begin(){
		//mBattleManager.endBattle();
	}
}
