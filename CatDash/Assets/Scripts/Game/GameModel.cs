using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class GameModel{


	private float 	time			= 0.0f;
	private float 	distance		= 0.0f;
	private float 	moveStep		= 0.0f;		//移動距離 per oneTap
	private float 	startPosX		= 870.0f;	//開始地点
	private float 	endPosX			= -866f;	//終了地点
	private float 	bgPosX			= 0.0f;		//背景座標posX

	private int 	turnOver 		= 0;		//転んだ回数

	private float 	btnStopCount 	= 0.0f;		
	private int		leftTapCnt 		= 0;
	private int 	rightTapCnt 	= 0;



	private const float STOP_TIME 			= 2.0f; //転んだときのデバフ
	private const int 	CONTINUITY_NUM 		= 2; 	//連続タップ上限回数



	// 遷移イベント
	public event EventHandler transitionHandler;

	// 転んだイベント
	private GameEventArgs gameEventArgs = new GameEventArgs();
	public delegate void GameEventHandler( object sender, GameEventArgs g );
	public event GameEventHandler turnOverHandler;
	public event GameEventHandler animationHandler;


	public bool isGameStart;

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

	public ANIMATION_STATE state{ get; set; }



	public GameModel(){
		
		turnOver 		= 0;
		time 			= 0.0f;
		this.distance 	= 0.0f;
		bgPosX 			= startPosX;
		state = ANIMATION_STATE.NORMAL;
		int totalTap 	= (int)(MAX_MOVING_DISTANCE / MOVING_DISTANCE);
		float distance 	= endPosX - startPosX;
		moveStep 		= distance / totalTap;
		
	}


	public void Update(){

		//転んだとき
		if (gameEventArgs.isTurnOver) {

			//カウント開始
			btnStopCount += 1 * Time.deltaTime;

			if ( btnStopCount >= STOP_TIME ){				

				turnOver++; 
				Debug.Log ( turnOver+"回転んだ" );
				btnStopCount = 0.0f;
				gameEventArgs.isTurnOver = false;
				turnOverHandler ( this, gameEventArgs );

			}

		} 

	}



	#region calculation
	/// <summary>
	/// スクロール移動計算結果
	/// </summary>
	/// <returns>The scroll background.</returns>
	public float AddScrollBg(){

		bgPosX += moveStep;
		return bgPosX;
	
	}

	/// <summary>
	/// 移動距離加算
	/// </summary>
	/// <returns>The distance.</returns>
	public float AddDistance(){
	
		this.distance += MOVING_DISTANCE;

		//ゲーム終了判定
		if ( this.distance >= MAX_MOVING_DISTANCE ) {
			
			gameEventArgs.state = ANIMATION_STATE.JUMP;
			isGameStart = false;
			animationHandler ( this, gameEventArgs );

		}

		return this.distance;

	}

	/// <summary>
	/// 転んだ回数加算
	/// </summary>
	public void AddTurnOverNum(){

		turnOver++;
	
	}


	/// <summary>
	/// タイマー加算
	/// </summary>
	/// <returns>The time.</returns>
	public string GetTime (){
		time += 1 * Time.deltaTime;
		return time.ToString("F1");
	}

	#endregion


	/// <summary>
	///スコア送信API
	/// </summary>
	/// <returns>The send.</returns>
	/// <param name="tansition">Tansition.</param>
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




	/// <summary>
	/// 左タップ
	/// </summary>
	public void MoveLeft(){

		this.leftTapCnt++;
		this.rightTapCnt = 0;

		if ( this.leftTapCnt >= CONTINUITY_NUM ) {

			this.leftTapCnt = 0;
			gameEventArgs.isTurnOver = true;
			turnOverHandler ( this, gameEventArgs );
			return;

		}

		gameEventArgs.state = ANIMATION_STATE.LEFT_WALK;
		animationHandler ( this, gameEventArgs );

	}


	/// <summary>
	/// 右タップ
	/// </summary>
	public void MoveRight(){

		this.rightTapCnt++;
		this.leftTapCnt = 0;

		if ( this.rightTapCnt >= CONTINUITY_NUM ) {
			this.rightTapCnt = 0;
			gameEventArgs.isTurnOver = true;
			turnOverHandler ( this, gameEventArgs );
			return;
		}

		gameEventArgs.state = ANIMATION_STATE.RIGHT_WALK;
		animationHandler ( this, gameEventArgs );
	}


}


public class GameEventArgs:EventArgs{

	public bool isTurnOver{ get; set; }

	public GameModel.ANIMATION_STATE state{ get; set; }



	public GameEventArgs(){
	
		isTurnOver = false;
		state = GameModel.ANIMATION_STATE.NORMAL;

	}

}