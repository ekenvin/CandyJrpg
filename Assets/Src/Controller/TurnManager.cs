using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {

	private List<Character> turnOrder;
	private BattleManager mBattleManager;

	public GameObject battleUI;
	public GameObject battleUIInstance;
	public Button atkButton;
	public Button defButton;
	public Button runButton;
	public Button specButton;
	public Button focButton;

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
		battleUIInstance=(GameObject)Instantiate(battleUI);
		battleUIInstance.transform.parent=mBattleManager.background.transform;

		atkButton=battleUIInstance.transform.Find("AttackBtn").GetComponent<Button>();
		defButton=battleUIInstance.transform.Find("DefendBtn").GetComponent<Button>();
		runButton=battleUIInstance.transform.Find("RunBtn").GetComponent<Button>();
		runButton.onClick=run;
		specButton=battleUIInstance.transform.Find("SpecialBtn").GetComponent<Button>();
		focButton=battleUIInstance.transform.Find("FocusBtn").GetComponent<Button>();
	}

	private void run(){
		mBattleManager.endBattle();
	}
}
