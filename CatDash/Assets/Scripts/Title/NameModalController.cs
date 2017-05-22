using System;
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

	[SerializeField, HeaderAttribute ("セッション切れ")]
	private GameObject limitOver;

	[SerializeField, HeaderAttribute ("セッション切れ親obj")]
	private GameObject parent;


	//イベント発行
	public event EventHandler nameInputHandler;

	private NameInputModel nameInputModel = new NameInputModel();

	//private Encoding encJIS = Encoding.GetEncoding("Shift_JIS");

	void Start(){

		this.cautionObj.SetActive (false);

		this.gameObject.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 0.7f);
	}


	/// <summary>
	/// 入力カーソルが外れたら実行
	/// </summary>
	public void NameInput(){

		nameInputModel.name = nameTxt.text;

	}


	/// <summary>
	/// 決定ボタン
	/// </summary>
	public void DecisionButton(){
	
		if (string.IsNullOrEmpty (nameInputModel.name)) {
			cautionObj.SetActive (true);
			return;
		}

		//半角判定
		/*int num = encJIS.GetByteCount(this.userName);
		bool isZenkaku = ( num == this.userName.Length * 2 );

		if (!isZenkaku) {
			cautionObj.SetActive (true);
			return;
		}*/


		//注意文言非表示
		cautionObj.SetActive (false);

		//イベント発行
		nameInputHandler(this,EventArgs.Empty);


	}


	public void OnRecieveTransition(object sender, EventArgs e){

		transition.NextScene ( "scene_Game" );

	}



	public void SetActive(bool isActive){
	
		this.gameObject.SetActive ( isActive );
	
	}



	public string GetName(){
		return nameInputModel.name;
	}
}
