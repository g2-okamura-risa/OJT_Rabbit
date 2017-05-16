using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using DG.Tweening;

public class TitleController : MonoBehaviour {

	[SerializeField, HeaderAttribute ("モーダルobj")]
	private GameObject modalObj;
	[SerializeField, HeaderAttribute ("スタートボタン")]
	private Button startButton;
	[SerializeField, HeaderAttribute ("スタートボタン画像")]
	private GameObject startBtnObj;
	[SerializeField, HeaderAttribute ("遷移コントローラー")]
	private TransitionController transition;

	[SerializeField]
	private GameObject limitOver;

	private bool isNewUser = false;


	// Use this for initialization
	void Start () {

		DOTween.Init (false, true, LogBehaviour.ErrorsOnly);


		modalObj.SetActive (false);

		//uuid取得
		Config.USER_UUID = PlayerPrefs.GetString (Config.PREFS_KEY_UUID);
		//uuidを持っていない
		if (Config.USER_UUID == "") {
			//名前入力モーダル表示
			this.isNewUser = true;

			return;
		}


		this.isNewUser = false;
	}
	
	#region ボタン
	public void ToGame(){

		if (this.isNewUser) {
			modalObj.SetActive (true);
			startButton.interactable = false;
		} else {
			
			//uuid送信しtokenもらう
			WWWForm w = new WWWForm();
			w.AddField ("uuid", Config.USER_UUID);

			API api = new API ();
			api.limitOverObj = limitOver;
			api.parent = this.gameObject;
			StartCoroutine(api.Connect (Config.URL_LOGIN, w, transition,GetToken));

		}

	}


	public void ResetUUID(){
		
		PlayerPrefs.DeleteAll();
	
	}

	#endregion
	private void GetToken(JsonData json){

		Config.AUTH_TOKEN 	= (string)	json["auth_token"];
		Config.USER_ID 		= (int)		json ["user_id"];

		//ゲームへ遷移
		transition.NextScene("scene_Game");
	}


}
