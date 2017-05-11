using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text;
using LitJson;


public class NameModalController : MonoBehaviour {

	[SerializeField, HeaderAttribute ("名前入力txt")]
	private Text nameTxt;
	[SerializeField, HeaderAttribute ("注意文言")]
	private GameObject cautionObj;
	[SerializeField, HeaderAttribute("遷移コントローラー")]
	private TransitionController transition;


	private string userName;
	private Encoding encJIS = Encoding.GetEncoding("Shift_JIS");

	void Start(){

		this.cautionObj.SetActive (false);

		DOTween.Init (false, true, LogBehaviour.ErrorsOnly);
	

		this.gameObject.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 1.0f);
	}


	/// <summary>
	/// 入力カーソルが外れたら実行
	/// </summary>
	public void NameInput(){

		this.userName = nameTxt.text;

	}



	public void DecisionButton(){
	
		if (string.IsNullOrEmpty (this.userName)) {
			cautionObj.SetActive (true);
			return;
		}

		int num = encJIS.GetByteCount(this.userName);
		bool isZenkaku = ( num == this.userName.Length * 2 );

		if (!isZenkaku) {
			cautionObj.SetActive (true);
			return;
		}


		//名前保存
		PlayerPrefs.SetString ( Config.PREFS_KEY_NAME, this.userName );
		PlayerPrefs.Save ();



		//通信開始OK 遷移

		//注意文言非表示
		cautionObj.SetActive (false);

		//API通信開始
		WWWForm w = new WWWForm ();
		w.AddField ("uuid", Config.USER_UUID);
		w.AddField ("user_name", this.userName);


		API api = new API ();
		StartCoroutine(api.Connect(Config.URL_LOGIN, w, this.transition, GetToken));
	}


	private void GetToken(JsonData json){

		Config.AUTH_TOKEN = (string) json["auth_token"];

		//ゲームへ遷移
		transition.NextScene("scene_Game");
	}
}
