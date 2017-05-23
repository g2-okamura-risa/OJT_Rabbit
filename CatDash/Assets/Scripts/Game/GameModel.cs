using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class GameModel{



	private float 	moveStep		= 0.0f;		//移動距離 per oneTap
	private float 	startPosX		= 870.0f;	//開始地点
	private float 	endPosX			= -866f;	//終了地点

	public float 	bgPosX	{get; private set;}	//背景座標posX
	public float 	time	{get; private set;}
	public float 	distance{get; private set;}

	private int 	turnOver 		= 0;		//転んだ回数

	private float 	btnStopCount 	= 0.0f;		
	private int		leftTapCnt 		= 0;
	private int 	rightTapCnt 	= 0;


	private const float STOP_TIME 			= 2.0f; //転んだときのデバフ
	private const int 	CONTINUITY_NUM 		= 2; 	//連続タップ上限回数

	// 遷移イベント
	public event EventHandler transitionHandler;
	public event EventHandler recoveryHandler;

	public bool isGameStart{ get; private set; }

	private const float MOVING_DISTANCE 	= 0.5f;
	private const float MAX_MOVING_DISTANCE = 100.0f;

	public enum ANIMATION_STATE
	{
		NORMAL,
		LEFT_WALK,
		RIGHT_WALK,
		TURNOVER,
		JUMP
	};

	public enum INPUT_TYPE
	{
		LEFT,
		RIGHT,
	};


	private ANIMATION_STATE animState;



	public GameModel(){
		
		turnOver 		= 0;
		time 			= 0.0f;
		this.distance 	= 0.0f;
		bgPosX 			= startPosX;
		animState		= ANIMATION_STATE.NORMAL;
		int totalTap 	= (int)(MAX_MOVING_DISTANCE / MOVING_DISTANCE);
		float distance 	= endPosX - startPosX;
		moveStep 		= distance / totalTap;
		
	}


	public string GetCurrentTime( float timeDelta ){
		time += 1 * timeDelta;
		return time.ToString ( "F2" );
	}



	/// <summary>
	/// 入力タイプ受付
	/// </summary>
	/// <returns>アニメーションstate.</returns>
	public ANIMATION_STATE GetState(INPUT_TYPE type){

		switch ( type ) {

			case INPUT_TYPE.LEFT:
				isGameStart = true;

				this.leftTapCnt++;
				this.rightTapCnt = 0;

				if ( this.leftTapCnt >= CONTINUITY_NUM ) {
					turnOver++; 
					this.leftTapCnt = 0;
					this.animState = ANIMATION_STATE.TURNOVER;
					return this.animState;
				}


				this.animState = ANIMATION_STATE.LEFT_WALK;

				AddScrollBg ();
				AddDistance ();

				return this.animState;


			case INPUT_TYPE.RIGHT:
				isGameStart = true;

				this.rightTapCnt++;
				this.leftTapCnt = 0;

				if ( this.rightTapCnt >= CONTINUITY_NUM ) {
					turnOver++; 
					this.rightTapCnt = 0;
					this.animState = ANIMATION_STATE.TURNOVER;
					return this.animState;

				}


				this.animState = ANIMATION_STATE.RIGHT_WALK;

				AddScrollBg ();
				AddDistance ();
				return this.animState;

		}

		return this.animState;

	}





	#region calculation
	/// <summary>
	/// スクロール移動計算結果
	/// </summary>
	private void AddScrollBg(){
		bgPosX += moveStep;
	}

	/// <summary>
	/// 移動距離加算
	/// </summary>
	private void AddDistance(){
	
		this.distance += MOVING_DISTANCE;

		//ゲーム終了判定
		if ( this.distance >= MAX_MOVING_DISTANCE ) {

			animState = ANIMATION_STATE.JUMP;
			isGameStart = false;
		}
	}

	/// <summary>
	/// 転んだ回数加算
	/// </summary>
	public void AddTurnOverNum(){
		turnOver++;
	}


	#endregion


	/// <summary>
	///スコア送信API
	/// </summary>
	public IEnumerator ScoreSend(TransitionController tansition){

		WWWForm w = new WWWForm();
		w.AddField ("auth_token", Config.AUTH_TOKEN);
		w.AddField ("goal_time", time.ToString());
		w.AddField ("turnover_num", turnOver);

		API api = new API ();
		yield return api.Connect(Config.URL_RESULT, w, tansition, ToResult);

	}

	private void ToResult(JsonData json){
		//遷移イベント発行
		transitionHandler ( this, EventArgs.Empty );
	}

}
	