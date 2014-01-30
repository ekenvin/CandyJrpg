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

	public delegate void OnTargetSelected(Character target);
	public OnTargetSelected attackTarget;
	public OnTargetSelected specialTarget;
	public OnTargetSelected onSelected;

	public Character currentlyGoing;
	public int currentIdx=0;
	public bool inUI=false;
	public bool targetingEnabled=false;

	public int partySprit=0;
	public SpriteRenderer allySprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	public void init(List<Character> order,BattleManager manager){
		turnOrder=order;
		mBattleManager=manager;

	}

	public void begin(){
		for(int i=0;i<turnOrder.Count;i++){
			if(turnOrder[i] is Ally)
				partySprit+=turnOrder[i].spt;
		}

		battleUIInstance=(GameObject)Instantiate(battleUI);
		battleUIInstance.transform.parent=mBattleManager.background.transform;
		battleUIInstance.transform.position=GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		battleUIInstance.transform.Translate(0,0,1);

		allySprite=battleUIInstance.transform.Find("PlayerMount").GetComponent<SpriteRenderer>();

		atkButton=battleUIInstance.transform.Find("AttackBtn").GetComponent<Button>();
		atkButton.onClick=attack;
		defButton=battleUIInstance.transform.Find("DefendBtn").GetComponent<Button>();
		defButton.onClick=defend;
		runButton=battleUIInstance.transform.Find("RunBtn").GetComponent<Button>();
		runButton.onClick=run;
		specButton=battleUIInstance.transform.Find("SpecialBtn").GetComponent<Button>();
		specButton.onClick=special;
		focButton=battleUIInstance.transform.Find("FocusBtn").GetComponent<Button>();
		focButton.onClick=focus;

		battleUIInstance.SetActive(false);

		currentIdx=-1;
		nextTurn();
	}

	private void run(GameObject sender){
		mBattleManager.endBattle();
	}

	private void nextTurn(){
		if(checkIsEnd())
			return;
		currentIdx++;
		if(currentIdx>=turnOrder.Count)
			currentIdx=0;
		currentlyGoing=turnOrder[currentIdx];
		Debug.Log("Currently Going "+currentlyGoing.gameObject.name);
		if(currentlyGoing is Ally){
			battleUIInstance.SetActive(true);
			allySprite.sprite=(currentlyGoing as Ally).mSprite;
		}
		else{
			(currentlyGoing as Enemy).determineAction(mBattleManager.allies);
			nextTurn();
		}
	}

	private bool checkIsEnd(){
		bool allyAlive=false;
		bool enemyAlive=false;
		for(int i=0;i<turnOrder.Count;i++){
			if(turnOrder[i] is Ally && turnOrder[i].HP>0){
				allyAlive=true;
			}
			else if(turnOrder[i] is Enemy && turnOrder[i].HP >0){
				Debug.Log("Enemy Alive..."+turnOrder[i].HP);
				enemyAlive=true;
			}
		}
		if(!enemyAlive){
			Debug.Log("Battle Win!");
			mBattleManager.endBattle();
			return true;
		}
		if(!allyAlive){
			Debug.Log("Battle Lose!");
			mBattleManager.endBattle();
			return true;
		}
		return false;
	}

	private void enableTargeting(){
		Debug.Log("Targeting enabled");
		targetingEnabled=true;
		battleUIInstance.SetActive(false);
	}

	public void selectTarget(GameObject sender){
		Debug.Log("Target selected");
		if(targetingEnabled){

			onSelected(sender.GetComponent<Enemy>());
			targetingEnabled=false;
			nextTurn();
		}
	}

	private void attack(GameObject sender){
		attackTarget=currentlyGoing.attack;
		onSelected=attackTarget;
		enableTargeting();
	}

	private void special(GameObject sender){
		specialTarget=currentlyGoing.special;
		onSelected=specialTarget;
		enableTargeting();
	}


	private void defend(GameObject sender){
		currentlyGoing.defend();
		nextTurn();
	}
	
	private void focus(GameObject sender){
		partySprit+=currentlyGoing.spt;
		nextTurn();
	}

}
