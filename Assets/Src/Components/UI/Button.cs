using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public delegate void OnClick();
	public OnClick onClick;

	void OnMouseDown(){
		if(onClick!=null){
			onClick();
		}
	}
}
