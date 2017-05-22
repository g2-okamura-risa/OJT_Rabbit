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

	[SerializeField, HeaderAttribute("画面全タップボタン")]
	private GameObject screenBtnObj;

	[SerializeField, HeaderAttribute("セッション切れモーダルプレハブ")]
	private GameObject limitOverObj;


	private bool isTurnOver = false; //転んだらtrue; 2秒間ボタン停止

	public 	bool isGameOver = false;


	// Use this for initialization
	void Start () {
		
		//全画面タップOFF
		screenBtnObj.SetActive (false);

	}


	// Update is called once per frame
	void Update () {

		if (this.isGameOver) {

			this.time.countStart = false;

			//ゴールアニメーション
			player.SetState( Player.PLAYER_STATE.JUMP );

			//全画面タップON
			screenBtnObj.SetActive ( true );

			this.isGameOver = false;

			return;
		}
			


		if ( !isTurnOver ) {
			
			if ( Input.GetKeyDown ( KeyCode.A ) ) {

				this.LeftBtn ();		

			} else if ( Input.GetKeyDown ( KeyCode.D ) ) {

				this.RightBtn ();		

			}
		}
			
	}



	#region ボタン

	public void LeftBtn(){

		time.countStart = true;
		player.SetState(Player.PLAYER_STATE.LEFT_WALK);

	}


	public void RightBtn(){

		time.countStart = true;

		player.SetState(Player.PLAYER_STATE.RIGHT_WALK);


	}

	public void isFootBtn(bool isActive){

		this.footBtn [0].interactable = isActive;
		this.footBtn [1].interactable = isActive;

		// ボタンoff = ころんだ
		this.isTurnOver = !isActive;
	}

	public void ScreenTap(){
	
		//API
		ScoreSend();
	
	}



	#endregion

	#region API


	private void ScoreSend(){
	
		WWWForm w = new WWWForm();
		w.AddField ("auth_token", Config.AUTH_TOKEN);
		w.AddField ("goal_time", this.time.time.ToString());
		w.AddField ("turnover_num", player.turnOver);

		API api = new API ();
		api.limitOverObj = this.limitOverObj;
		api.parent = this.gameObject;
		StartCoroutine(api.Connect(Config.URL_RESULT, w, tansition, ToResult));

	}

	private void ToResult(JsonData json){

		tansition.NextScene ("scene_Ranking");

	}


	#endregion



	

}
