using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public GameObject nextRoom;

	private bool isActive;
	private PlayerControl hero;

	public AudioClip sound;

	// Use this for initialization
	void Start () {
		hero=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("OpenDoor")>0 && isActive && hero.moveEnabled && hero.doorEnabled){
			this.open();
			hero.doorEnabled=false;
		}
		else if(Input.GetAxis("OpenDoor")==0) {
			hero.doorEnabled=true;
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
		AudioSource.PlayClipAtPoint(sound,transform.position);
		WorldManager manager=GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
		manager.currentWorld.currentRoom.SetActive(false);
		manager.currentWorld.currentRoom=nextRoom;
		nextRoom.SetActive(true);
		isActive=false;
	}
}
