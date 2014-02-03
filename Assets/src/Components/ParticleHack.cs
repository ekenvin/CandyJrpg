using UnityEngine;
using System.Collections;

public class ParticleHack : MonoBehaviour {
	public string layer="Foreground";
	public int order=0;
	// Use this for initialization
	void Start () {
		if(particleSystem!=null){
			particleSystem.renderer.sortingLayerName=layer;
			particleSystem.renderer.sortingOrder=order;
		}
		else{
			renderer.sortingLayerName=layer;
			renderer.sortingOrder=order;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
