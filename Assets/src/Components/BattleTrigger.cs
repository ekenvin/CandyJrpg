using UnityEngine;
using System.Collections;

public class BattleTrigger : MonoBehaviour {
	private bool wasTriggered=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D c){
		if(!wasTriggered){
			wasTriggered=true;
			GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>().generateBattle();
			Destroy(this.gameObject);
		}

	}
}
