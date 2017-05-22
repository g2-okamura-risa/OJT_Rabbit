using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

	[SerializeField, HeaderAttribute("タイマーtxt")]
	private Text timerTxt;

	/// <summary>足ボタンがタップされたらtrue </summary>
	public bool countStart = false;

	public float time{ get; set;}


	// Use this for initialization
	void Start () {

		timerTxt.text = "0.00";
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!countStart)
			return;

		//カウント開始
		time += 1 * Time.deltaTime;
		timerTxt.text = time.ToString ("F2");

		
	}
}
