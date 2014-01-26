using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	public TurnManager turnManager;
	public PartyManager partyManager;

	private List<Enemy> enemies;
	private List<Ally> allies;


	private Battle currentBattle;
	private World currentWorld;
	private GameObject background;
	// Use this for initialization
	void Start () {
		//generateBattle();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<Enemy> generateEnemies(){
		return currentWorld.possibleEnemies;
	}

	public void generateBattle(){
		this.currentWorld=GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().currentWorld;
		this.enemies=this.generateEnemies();
		this.allies=partyManager.party;
		currentBattle=new Battle(this.enemies, this.allies);
		battleToScene();
	}

	private void battleToScene(){
		this.background=(GameObject)Instantiate(this.currentWorld.possibleBackgrounds[0]);
		for(int i=0;i<this.enemies.Count;i++){
			GameObject enemy=(GameObject)Instantiate(this.enemies[i].gameObject);
			enemy.transform.Translate(new Vector3(i,0,0));
		}
	}
}
