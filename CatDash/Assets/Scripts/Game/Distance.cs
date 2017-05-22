using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour {

	[SerializeField, HeaderAttribute("距離テキスト")]
	private Text distanceTxt;

	// Use this for initialization
	void Start () {

		distanceTxt.text = "0.0";

	}
		
	public void SetDistance( float distance ){

		this.distanceTxt.text = distance.ToString ( "F1" );

	}
}
