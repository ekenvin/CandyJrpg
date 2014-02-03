using UnityEngine;
using System.Collections;

public class AttackEffect : BattleEffect {

	private string initialLayer;
	private GameObject target;
	public GameObject effect;
	public AudioClip sound;

	private float startTime;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	}

	public void execute(GameObject _target){
		AudioSource.PlayClipAtPoint(sound,transform.position);
		target=_target;
		SpriteRenderer spt=target.gameObject.GetComponent<SpriteRenderer>();
		initialLayer=spt.sortingLayerName;
		effect.transform.position=target.transform.position;
		effect.transform.Translate(0,3.5f,0);
		effect.SetActive(true);
		Debug.Log("Invoke repeating start");
		startTime=Time.time;
		InvokeRepeating("fade",0,0.1f);
		Invoke ("hide",1.1f);
	}
	public void fade(){
		SpriteRenderer spt=target.gameObject.GetComponent<SpriteRenderer>();
		if(spt.sortingLayerName==this.initialLayer){
			spt.sortingLayerName="World";
		}
		else{
			spt.sortingLayerName=this.initialLayer;
		}
		Debug.Log (spt.sortingLayerName);
	}
	public void hide(){
		effect.GetComponent<Scaler>().reset();
		SpriteRenderer spt=target.gameObject.GetComponent<SpriteRenderer>();
		spt.sortingLayerName=this.initialLayer;
		effect.SetActive(false);
		CancelInvoke("fade");
		Debug.Log ("Effect Hidden");
	}
}
