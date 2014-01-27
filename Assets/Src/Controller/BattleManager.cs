using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	public TurnManager turnManager;
	public PartyManager partyManager;

	private List<Enemy> enemies;
	private List<Ally> allies;

	private List<GameObject> enemyGraphics;

	public float battleProbability;
	public float battleThreshold;
	public float encounterRate;
	public bool battleEnabled;

	private Battle currentBattle;
	private World currentWorld;
	public GameObject background;
	// Use this for initialization
	void Start () {
		battleEnabled=true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(battleEnabled){
			battleProbability+=encounterRate;
			float check=Random.Range(0,battleProbability);
			if(check>battleThreshold){
				generateBattle();
				battleEnabled=false;
				Pauser p=GameObject.FindGameObjectWithTag("WorldManager").GetComponentInChildren<Pauser>();
				p.pause();
			}
		}
	}

	public List<Enemy> generateEnemies(){
		return currentWorld.possibleEnemies;
	}

	public void generateBattle(){
		this.currentWorld=GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().currentWorld;
		this.enemies=this.generateEnemies();
		this.allies=partyManager.party;
		currentBattle=new Battle(this.enemies, this.allies);
		List<Character> allParticipants=new List<Character>();
		for(int i=0;i<this.enemies.Count;i++){
			allParticipants.Add(this.enemies[i]);
		}
		for(int i=0;i<this.allies.Count;i++){
			allParticipants.Add(this.allies[i]);
		}

		allParticipants.Sort(delegate(Character x, Character y) {
			if(x.spd>y.spd)
				return -1;
			else
				return 1;
		});

		turnManager.init(allParticipants,this);
		battleToScene();
	}

	private void battleToScene(){
		enemyGraphics=new List<GameObject>();
		this.background=(GameObject)Instantiate(this.currentWorld.possibleBackgrounds[0]);
		for(int i=0;i<this.enemies.Count;i++){
		  	enemyGraphics.Add((GameObject)Instantiate(this.enemies[i].gameObject));
			enemyGraphics[i].transform.parent=this.background.transform;
			enemyGraphics[i].transform.Translate(new Vector3(i,0,0));
		}
		turnManager.begin();
	}

	public void endBattle(){
		GameObject.Destroy(this.background.gameObject);
		for(int i=0;i<this.enemies.Count;i++){
			GameObject.Destroy(enemyGraphics[i].gameObject);
		}

		currentBattle=null;
		battleProbability=0;
		battleEnabled=true;
		Pauser p=GameObject.FindGameObjectWithTag("WorldManager").GetComponentInChildren<Pauser>();
		p.pause();
	}
}
