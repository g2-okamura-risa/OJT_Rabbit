using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using DG.Tweening;

public class TitleController : MonoBehaviour {

	[SerializeField, HeaderAttribute ("名前入力コントローラー")]
	private NameModalController nameModalController;

	[SerializeField, HeaderAttribute ("遷移コントローラー")]
	private TransitionController transition;

	private LoginModel loginModel = new LoginModel();


	void Start () {

		//名前入力モーダル表示
		nameModalController.SetActive ( false );
		nameModalController.nameInputHandler += this.OnRecieveNameInput;

		//イベント登録
		loginModel.newUserDataHandler += this.OnRecieveEvent;
		loginModel.transitionHandler += this.OnRecieveTransition;

		DOTween.Init ( false, true, LogBehaviour.ErrorsOnly );

	}
	

	public void GameStart(){

		StartCoroutine ( loginModel.Loading (transition) );

	}


	public void ResetUUID(){
		
		PlayerPrefs.DeleteAll();
	
	}


	public void OnRecieveEvent(object sender, EventArgs e){

		nameModalController.SetActive ( true );

	}


	public void OnRecieveTransition(object sender, EventArgs e){
	
		transition.NextScene ( "scene_Game" );
	
	}


	public void OnRecieveNameInput(object sender, EventArgs e){

		loginModel.SetUserName(nameModalController.GetName());
		StartCoroutine ( loginModel.Loading (transition) );
	}



}
