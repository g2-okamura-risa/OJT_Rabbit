using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	private Vector3 pos;

	// Use this for initialization
	void Start () {

		this.transform.localPosition = new Vector3 (0f, 0f, 0f);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (this.transform.localPosition.x <= 0.0f) {
			this.transform.localPosition = new Vector3(0.0f, 0.0f,0.0f);
		}

		
	}

	public void SetPos(float posX){

		this.gameObject.transform.localPosition = new Vector3 (posX+4.8f, 0f, 0f);
	
	}

}
