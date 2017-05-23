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
	private GameModel.ANIMATION_STATE animState;

	// Use this for initialization
	void Start () {

		gameModel = new GameModel ();
		gameModel.transitionHandler += this.OnRecieveTransition;

		//全画面タップOFF
		screenBtnObj.SetActive (false);

	}


	// Update is called once per frame
	void Update () {

		if ( !gameModel.isGameStart )
			return;


		timerTxt.text = gameModel.GetCurrentTime ( Time.deltaTime );


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

		animState = gameModel.GetState ( GameModel.INPUT_TYPE.LEFT );
		this.SetAction ( animState );
	
	}


	public void RightBtn(){

		animState = gameModel.GetState ( GameModel.INPUT_TYPE.RIGHT );
		this.SetAction ( animState );

	}




	public void ScreenTap(){
		
		StartCoroutine ( gameModel.ScoreSend ( transition ) );
	
	}
	#endregion


	/// <summary>
	/// 画面遷移イベント
	/// </summary>
	public void OnRecieveTransition(object sender, EventArgs e){

		transition.NextScene ( "scene_Ranking" );

	}
		

	/// <summary>
	/// ステートセット
	/// </summary>
	private void SetAction(GameModel.ANIMATION_STATE action){

		switch ( action ) {

			case GameModel.ANIMATION_STATE.LEFT_WALK:

				MoveBackGround ();
				UpdateScore ();
				animator.SetTrigger ( "LEFT_WALK" );
				break;

			case GameModel.ANIMATION_STATE.RIGHT_WALK:

				MoveBackGround ();
				UpdateScore ();
				animator.SetTrigger ( "RIGHT_WALK" );
				break;

			case GameModel.ANIMATION_STATE.JUMP:

				UpdateScore ();
				animator.SetBool ( "JUMP", true );
				screenBtnObj.SetActive ( true );
				SetActiveFootBtn ( false );
				break;


			case GameModel.ANIMATION_STATE.TURNOVER:
				
				animator.SetTrigger ( "TURN_OVER" );
				StartCoroutine ( WaitRecoveryBtn() );
				break;
		
		}
	}


	/// <summary>
	/// タップ時処理まとめ
	/// </summary>
	private void MoveBackGround(){

		bgRectTransform.localPosition = new Vector3 (  gameModel.bgPosX, bgRectTransform.localPosition.y, 0f );

	}

	private void UpdateScore(){
		
		distanceTxt.text = gameModel.distance.ToString ( "F1" );

	}


	/// <summary>
	/// ボタンアクティブセット
	/// </summary>
	private void SetActiveFootBtn(bool isActive){

		this.footBtn [0].interactable = isActive;
		this.footBtn [1].interactable = isActive;

	}


	private IEnumerator WaitRecoveryBtn(){
		
		SetActiveFootBtn ( false );
		yield return new WaitForSeconds(2.0f);
		SetActiveFootBtn ( true );
	
	}




}
