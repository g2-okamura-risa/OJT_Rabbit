using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TitleController : MonoBehaviour {

	[SerializeField, HeaderAttribute ("モーダルobj")]
	private GameObject modalObj;
	[SerializeField, HeaderAttribute ("スタートボタン")]
	private Button startButton;




	private bool isNewUser = false;

	// Use this for initialization
	void Start () {
		modalObj.SetActive (false);
		//uuid設定

		//なかったらボタンおしてモーダル表示
		this.isNewUser = true;

		//あったらそのままスタート


	}
	

	public void ToGame(){

		if (this.isNewUser) {
			modalObj.SetActive (true);
			startButton.enabled = false;
		} else {

			//ゲームへ遷移


		}

	
	}
}
