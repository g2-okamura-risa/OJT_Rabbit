using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Player : MonoBehaviour {

	[SerializeField, HeaderAttribute("アニメーションコントローラー")]
	private Animator animator;

	[SerializeField, HeaderAttribute("ゲームコントローラー")]
	private GameController gameController;

	[SerializeField, HeaderAttribute("距離コントローラー")]
	private Distance distanceController;


	[SerializeField, HeaderAttribute("背景コントローラー")]
	private BackGroundScrollController bgController;


	private const float STOP_TIME 			= 2.0f; 
	private const int 	CONTINUITY_NUM 		= 2; 
	private const float MOVING_DISTANCE 	= 0.5f;
	private const float MAX_MOVING_DISTANCE = 100.0f;


	private bool 	isTurnOver 		= false; //転んだらtrue; 2秒間ボタン停止
	public 	int 	turnOver 		= 0;

	private float 	btnStopCount 	= 0.0f;
	private float 	dis 			= 0.0f;
	private int 	leftCnt 		= 0;
	private int 	rightCnt 		= 0;


	public enum PLAYER_STATE
	{
		NORMAL,
		LEFT_WALK,
		RIGHT_WALK,
		TURNOVER,
		JUMP
	};

	private PLAYER_STATE state = PLAYER_STATE.NORMAL;



	void Update(){
	
		//転んだとき
		if (isTurnOver) {

			//ボタンoff
			gameController.isFootBtn (false);

			//カウント開始
			btnStopCount += 1 * Time.deltaTime;

			if ( btnStopCount >= STOP_TIME ){				

				turnOver++; 
				Debug.Log ( turnOver+"回転んだ" );
				btnStopCount = 0.0f;
				isTurnOver = false;
				gameController.isFootBtn ( true );

			}

		} 
		//ゴール判定
		if ( this.dis >= MAX_MOVING_DISTANCE ) {

			gameController.isGameOver	= true;
			//ゴールアニメーション
			this.SetState( PLAYER_STATE.JUMP );
			gameController.isFootBtn ( false );
			//全画面タップON
			return;

		}
	
	}

	public void SetState(PLAYER_STATE state){
	
		this.state = state;

		switch ( this.state ) {
		
			case PLAYER_STATE.NORMAL:
				
				break;

			case PLAYER_STATE.LEFT_WALK:

				MoveLeft ();
				break;

			case PLAYER_STATE.RIGHT_WALK:

				MoveRight ();
				break;

			case PLAYER_STATE.TURNOVER:
				
				animator.SetTrigger ( "TURN_OVER" );
				break;

			case PLAYER_STATE.JUMP:
				
				animator.SetBool ( "JUMP", true );
				break;

		}
	
	}



	private void MoveLeft(){
		
		this.leftCnt++;
		this.rightCnt = 0;

		if ( this.leftCnt >= CONTINUITY_NUM ) {
			
			this.leftCnt = 0;
			this.isTurnOver = true;
			this.SetState ( PLAYER_STATE.TURNOVER );

			return;

		}

		animator.SetTrigger ( "LEFT_WALK" );

		this.dis += MOVING_DISTANCE;
		this.distanceController.SetDistance (this.dis);
		this.bgController.SetScroll ();
	
	}

	private void MoveRight(){
		
		this.rightCnt++;
		this.leftCnt = 0;

		if (this.rightCnt >= CONTINUITY_NUM) {
			this.rightCnt = 0;
			this.isTurnOver = true;
			this.SetState ( PLAYER_STATE.TURNOVER );
			return;
		}

		animator.SetTrigger ( "RIGHT_WALK" );

		this.dis += MOVING_DISTANCE;
		this.distanceController.SetDistance (this.dis);
		this.bgController.SetScroll ();
	
	}




}
