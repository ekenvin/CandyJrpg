using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour {
	public Vector3 scaleDir;
	public Vector3 maxScale;
	public Vector3 initialScale;
	// Use this for initialization
	void Awake () {
		initialScale=gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(scaleDir*Time.deltaTime);
		gameObject.transform.localScale+=scaleDir*Time.deltaTime;
		if(gameObject.transform.localScale.x>this.maxScale.x && maxScale.x!=0)
			gameObject.transform.localScale=new Vector3(maxScale.x,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
		else if(gameObject.transform.localScale.y>maxScale.y && maxScale.y!=0)
			gameObject.transform.localScale=new Vector3(gameObject.transform.localScale.x,maxScale.y,gameObject.transform.localScale.z);
		else if(gameObject.transform.localScale.z>maxScale.z && maxScale.z!=0)
			gameObject.transform.localScale=new Vector3(gameObject.transform.localScale.x,gameObject.transform.localScale.y,maxScale.z);
	}

	public void reset(){
		gameObject.transform.localScale=initialScale;
	}
}
