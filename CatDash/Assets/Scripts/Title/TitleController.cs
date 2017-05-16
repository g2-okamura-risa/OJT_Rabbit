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



	private bool isNewUser = false;


	// Use this for initialization
	void Start () {

		DOTween.Init (false, true, LogBehaviour.ErrorsOnly);


		modalObj.SetActive (false);
		//uuid設定
		Config.USER_UUID = PlayerPrefs.GetString (Config.PREFS_KEY_UUID);

		//uuidを持っていない
		if (Config.USER_UUID == "") {
			// 新規作成
			Config.USER_UUID = System.Guid.NewGuid ().ToString ();
			PlayerPrefs.SetString (Config.PREFS_KEY_UUID, Config.USER_UUID);
			PlayerPrefs.Save ();

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
			StartCoroutine(api.Connect (Config.URL_LOGIN, w, transition,GetToken));
			transition.NextScene("scene_Game");
		}

	}


	public void ResetUUID(){
		
		PlayerPrefs.DeleteAll();
	
	}

	#endregion
	private void GetToken(JsonData json){

		Config.AUTH_TOKEN = (string) json["auth_token"];

		//ゲームへ遷移
		transition.NextScene("scene_Game");
	}


}
