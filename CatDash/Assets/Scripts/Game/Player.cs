using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[SerializeField, HeaderAttribute("猫obj")]
	private GameObject playerObj; //移動用

	[SerializeField, HeaderAttribute("足ボタン")]
	private Button[] footBtn = new Button[2];
 	
	[SerializeField, HeaderAttribute("距離コントローラー")]
	private Distance distance;

	[SerializeField, HeaderAttribute("タイマーコントローラー")]
	private Timer time;




	private bool isTurnOver = false; //転んだらtrue; 2秒間ボタン停止
	private int turnOver = 0;

	private Vector3 pos;
	private float btnStopCount = 0.0f;
	private float dis;
	private int leftCnt = 0;
	private int rightCnt = 0;


	// Use this for initialization
	void Start () {
		isTurnOver = true;
	}

	
	// Update is called once per frame
	void Update () {

		if (isTurnOver) {
			//ボタンoff
			this.isFootBtn (false);

			//カウント開始
			btnStopCount += 1 * Time.deltaTime;

			if (btnStopCount >= 2.0f) {
			
				turnOver++; //転んだ回数カウント
				Debug.Log( turnOver );
				btnStopCount = 0.0f;
				isTurnOver = false;
				this.isFootBtn (true);

			}


		}



		
	}



	#region 足ボタン

	public void LeftBtn(){
		this.leftCnt++;
		this.rightCnt = 0;
		if (this.leftCnt >= 2) {
			this.leftCnt = 0;
			this.isTurnOver = true;
			return;
		}
		playerCalculate ();
	}


	public void RightBtn(){
		this.rightCnt++;
		this.leftCnt = 0;
		if (this.rightCnt >= 2) {
			this.rightCnt = 0;
			this.isTurnOver = true;
			return;
		}
		playerCalculate ();

	}


	private void playerCalculate(){
	
		time.countStart = true;

		this.dis += 0.5f;
		distance.SetDistance (this.dis);

	}

	private void isFootBtn(bool isActive){
	
		this.footBtn [0].interactable = isActive;
		this.footBtn [1].interactable = isActive;
	
	}


	#endregion






}
