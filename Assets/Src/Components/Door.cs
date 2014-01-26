using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public GameObject nextRoom;

	private bool isActive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("OpenDoor")>0 && isActive){
			this.open();
		}
	}

	void OnTriggerEnter2D(Collider2D c){

		if(c.gameObject==GameObject.FindGameObjectWithTag("Player")){
			isActive=true;
		}
	}
	void OnTriggerExit2D(Collider2D c){
		
		if(c.gameObject==GameObject.FindGameObjectWithTag("Player")){
			isActive=false;
		}
	}

	private void open(){
		WorldManager manager=GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
		manager.currentWorld.currentRoom.SetActive(false);
		manager.currentWorld.currentRoom=nextRoom;
		nextRoom.SetActive(true);
		isActive=false;
	}
}
