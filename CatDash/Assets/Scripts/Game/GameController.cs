using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;


public class GameController : MonoBehaviour {

	[SerializeField, HeaderAttribute("距離コントローラー")]
	private Distance distanceController;

	[SerializeField, HeaderAttribute("タイマーコントローラー")]
	private Timer time;

	[SerializeField, HeaderAttribute("遷移コントローラー")]
	private TransitionController tansition;


	[SerializeField, HeaderAttribute("プレイヤーコントローラー")]
	private Player player; //移動用

	[SerializeField, HeaderAttribute("プレイヤーコントローラー")]
	private BackGroundScrollController bgController;


	[SerializeField, HeaderAttribute("足ボタン")]
	private Button[] footBtn = new Button[2];



	private bool isTurnOver = false; //転んだらtrue; 2秒間ボタン停止
	private int turnOver = 0;

	private Vector3 pos;
	private float btnStopCount = 0.0f;
	private float dis;
	private int leftCnt = 0;
	private int rightCnt = 0;
	public bool isGameOver = false;


	private const float STOP_TIME = 2.0f; 
	private const int CONTINUITY_NUM = 2; 
	private const float MOVING_DISTANCE = 0.5f;
	private const float MAX_MOVING_DISTANCE = 100.0f;


	// Use this for initialization
	void Start () {


	}


	// Update is called once per frame
	void Update () {


		if (this.isGameOver) {
			return;
		
		}


		if (isTurnOver) {
			//ボタンoff
			this.isFootBtn (false);

			//カウント開始
			btnStopCount += 1 * Time.deltaTime;

			if (btnStopCount >= STOP_TIME) {				

				turnOver++; 
				btnStopCount = 0.0f;
				isTurnOver = false;
				this.isFootBtn (true);

			}
				
		}



		if (this.dis >= MAX_MOVING_DISTANCE) {

			this.isGameOver = true;
			this.time.countStart = false;
			//ゴールアニメーション
			player.SetState(Player.PLAYER_STATE.JUMP);
			//API
			ScoreSend();

		}




	}



	#region 足ボタン

	public void LeftBtn(){


		this.leftCnt++;
		this.rightCnt = 0;

		if (this.leftCnt >= CONTINUITY_NUM) {
			this.leftCnt = 0;
			this.isTurnOver = true;
			player.SetState(Player.PLAYER_STATE.TURNOVER);
			return;
		}

		player.SetState(Player.PLAYER_STATE.LEFT_WALK);

		playerCalculate ();


	}


	public void RightBtn(){


		this.rightCnt++;
		this.leftCnt = 0;



		if (this.rightCnt >= CONTINUITY_NUM) {
			this.rightCnt = 0;
			this.isTurnOver = true;
			player.SetState(Player.PLAYER_STATE.TURNOVER);
			return;
		}

		player.SetState(Player.PLAYER_STATE.RIGHT_WALK);

		playerCalculate ();

	}


	private void playerCalculate(){

		time.countStart = true;



		this.dis += MOVING_DISTANCE;
		this.distanceController.SetDistance (this.dis);
		this.bgController.SetScroll ();



	}

	private void isFootBtn(bool isActive){

		this.footBtn [0].interactable = isActive;
		this.footBtn [1].interactable = isActive;

	}


	#endregion


	#region API


	private void ScoreSend(){
	
		WWWForm w = new WWWForm();
		w.AddField ("auth_token", Config.AUTH_TOKEN);
		w.AddField ("goal_time", this.time.time.ToString());
		w.AddField ("turnover_num", this.turnOver);

		API api = new API ();

		StartCoroutine(api.Connect(Config.URL_RESULT, w, tansition, ToResult));

	}

	private void ToResult(JsonData json){

		tansition.NextScene ("scene_Ranking");

	}


	#endregion
}
