using UnityEngine;
using System.Collections;

public class DefEffect : BattleEffect {
	public GameObject particles;

	public AudioClip sound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void execute(GameObject _target){
		AudioSource.PlayClipAtPoint(sound,transform.position);
		particles.SetActive(true);
		Invoke ("hide",1.4f);
	}
	public void hide(){
		particles.GetComponent<Scaler>().reset();
		particles.SetActive(false);
	}
}
