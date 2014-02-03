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

	public GameObject battleInfoUI;
	

	public delegate string OnTargetSelected(Character target);
	public OnTargetSelected attackTarget;
	public OnTargetSelected specialTarget;
	public OnTargetSelected onSelected;

	public Character currentlyGoing;
	public int currentIdx=0;
	public bool inUI=false;
	public bool targetingEnabled=false;

	public int partySprit=0;
	public SpriteRenderer allySprite;


	public bool inBattle=false;
	public GUISkin skin;

	public List<Ally> allyOrder;
	public List<Enemy> enemyOrder;

	public Rect p1Name;
	public Rect p2Name;
	public Rect p3Name;
	public Rect p4Name;

	public Rect p1Health;
	public Rect p2Health;
	public Rect p3Health;
	public Rect p4Health;

	public Rect spiritRect;

	public Rect infoRect;
	public string info;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	void OnGUI(){
		if(!inBattle)
			return;
		Vector3 scale=new Vector3();
		scale.x = Screen.width/1024.0f; // calculate hor scale
		scale.y = Screen.height/768.0f; // calculate vert scale
		scale.z = 1;
		var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		GUI.skin=skin;

		GUI.Label(p1Name,""+allyOrder[0].mName);
		GUI.Label(p2Name,""+allyOrder[1].mName);
		GUI.Label(p3Name,""+allyOrder[2].mName);
		GUI.Label(p4Name,""+allyOrder[3].mName);

		GUI.Label(p1Health,"Health: "+allyOrder[0].HP);
		GUI.Label(p2Health,"Health: "+allyOrder[1].HP);
		GUI.Label(p3Health,"Health: "+allyOrder[2].HP);
		GUI.Label(p4Health,"Health: "+allyOrder[3].HP);

		GUI.Label(spiritRect,"Suger Rush: "+partySprit);

		GUI.Label(infoRect,info);
	}

	public void init(List<Character> order,BattleManager manager){
		turnOrder=order;
		mBattleManager=manager;

	}

	public void begin(){
		allyOrder=new List<Ally>();
		enemyOrder=new List<Enemy>();
		for(int i=0;i<turnOrder.Count;i++){
			if(turnOrder[i] is Ally){
				partySprit+=turnOrder[i].spt;
				allyOrder.Add(turnOrder[i] as Ally);
			}
			else{
				enemyOrder.Add(turnOrder[i] as Enemy);
			}
		}

		battleUIInstance=(GameObject)Instantiate(battleUI);
		battleUIInstance.transform.parent=mBattleManager.background.transform;
		battleUIInstance.transform.position=GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		battleUIInstance.transform.Translate(0,2,1);

		battleInfoUI.SetActive(true);

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

		inBattle=true;

		currentIdx=-1;
		nextTurn();
	}

	private void run(GameObject sender){
		battleInfoUI.SetActive(false);
		inBattle=false;
		mBattleManager.endBattle();
	}

	private void nextTurn(){
		info="";
		battleUIInstance.SetActive(false);
		if(checkIsEnd())
			return;
		currentIdx++;
		if(currentIdx>=turnOrder.Count){
			currentIdx=0;
			for(int i=0;i<allyOrder.Count;i++){
				partySprit+=(int)Mathf.Floor(allyOrder[i].spt/2);
			}
		}
		currentlyGoing=turnOrder[currentIdx];

		if(currentlyGoing.HP<=0){
			nextTurn();
			return;
		}
			

		if(currentlyGoing is Ally){
			battleUIInstance.SetActive(true);
			allySprite.sprite=(currentlyGoing as Ally).mSprite;
		}
		else{
			info=(currentlyGoing as Enemy).determineAction(mBattleManager.allies);
			Invoke("nextTurn",1.5f);// nextTurn();
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
			else if(turnOrder[i] is Enemy && turnOrder[i].HP <=0){
				turnOrder[i].gameObject.SetActive(false);
			}
		}
		if(!enemyAlive){
			Debug.Log("Battle Win!");
			battleInfoUI.SetActive(false);
			inBattle=false;
			mBattleManager.endBattle();
			return true;
		}
		if(!allyAlive){
			Debug.Log("Battle Lose!");
			battleInfoUI.SetActive(false);
			inBattle=false;
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

			info=onSelected(sender.GetComponent<Enemy>());
			if(onSelected==currentlyGoing.attack){
				GetComponent<AttackEffect>().execute(sender);
			}
			targetingEnabled=false;
			Invoke("nextTurn",1.5f);
			//nextTurn();
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
		GetComponent<DefEffect>().execute(sender);
		info=currentlyGoing.name+" is defending";
		Invoke("nextTurn",1.5f);
		//nextTurn();
	}
	
	private void focus(GameObject sender){
		partySprit+=currentlyGoing.spt;
		GetComponent<FocEffect>().execute(sender);
		info="Gained "+currentlyGoing.spt+" sweet spirit!";
		Invoke("nextTurn",1.5f);
		//nextTurn();
	}

}
