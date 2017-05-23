using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;


public class GameController : MonoBehaviour {

	[SerializeField, HeaderAttribute("遷移コントローラー")]
	private TransitionController transition;

	[SerializeField, HeaderAttribute("アニメーションコントローラー")]
	private Animator animator;

	[SerializeField, HeaderAttribute("足ボタン")]
	private Button[] footBtn = new Button[2];

	[SerializeField, HeaderAttribute("画面全タップボタン")]
	private GameObject screenBtnObj;

	[SerializeField, HeaderAttribute("セッション切れモーダルプレハブ")]
	private GameObject limitOverObj;

	[SerializeField, HeaderAttribute("タイマーテキスト")]
	private Text timerTxt;

	[SerializeField, HeaderAttribute("距離テキスト")]
	private Text distanceTxt;

	[SerializeField, HeaderAttribute("背景画像の親obj")]
	private RectTransform bgRectTransform;



	private GameModel gameModel;


	// Use this for initialization
	void Start () {

		gameModel = new GameModel ();
		gameModel.transitionHandler += this.OnRecieveTransition;
		gameModel.turnOverHandler 	+= this.OnRecieveTurnOver;
		gameModel.animationHandler += this.OnRecieveAnimationState;


		//全画面タップOFF
		screenBtnObj.SetActive (false);

	}


	// Update is called once per frame
	void Update () {


		if ( gameModel.isGameStart ) {
			timerTxt.text = gameModel.GetTime ();
			gameModel.Update ();

		}

		//デバッグ用
		if ( this.footBtn [0].interactable ) {
			
			if ( Input.GetKeyDown ( KeyCode.A ) ) {

				this.LeftBtn ();		

			} else if ( Input.GetKeyDown ( KeyCode.D ) ) {

				this.RightBtn ();		

			}
		}
			
	}



	#region ボタン

	public void LeftBtn(){

		MovePlayer ();
		gameModel.MoveLeft ();

	}


	public void RightBtn(){

		MovePlayer ();
		gameModel.MoveRight ();
	}

	/// <summary>
	/// タップ時処理まとめ
	/// </summary>
	private void MovePlayer(){
	
		gameModel.isGameStart = true;

		float posX = gameModel.AddScrollBg ();

		bgRectTransform.localPosition = new Vector3 ( posX, bgRectTransform.localPosition.y, 0f );

		distanceTxt.text = gameModel.AddDistance ().ToString("F1");
	}

	/// <summary>
	/// ボタンアクティブセット
	/// </summary>
	private void isFootBtn(bool isActive){

		this.footBtn [0].interactable = isActive;
		this.footBtn [1].interactable = isActive;

	}



	public void ScreenTap(){
		
		StartCoroutine ( gameModel.ScoreSend ( transition ) );
	
	}



	#endregion





	#region イベント受け取り

	public void OnRecieveTransition(object sender, EventArgs e){

		transition.NextScene ( "scene_Ranking" );

	}


	public void OnRecieveTurnOver(object sender, GameEventArgs g){
	
		isFootBtn ( !g.isTurnOver );


		if ( g.isTurnOver ) {
			//転んだ
			animator.SetTrigger ( "TURN_OVER" );
		}
			
	}



	public void OnRecieveAnimationState(object sender, GameEventArgs g){

		switch ( g.state ) {


			case GameModel.ANIMATION_STATE.LEFT_WALK:
				animator.SetTrigger ( "LEFT_WALK" );
				break;

			case GameModel.ANIMATION_STATE.RIGHT_WALK:
				animator.SetTrigger ( "RIGHT_WALK" );
				break;

			case GameModel.ANIMATION_STATE.JUMP:

				animator.SetBool ( "JUMP", true );
				screenBtnObj.SetActive ( true );
				isFootBtn ( false );
				break;

		}


	}



	#endregion




}
