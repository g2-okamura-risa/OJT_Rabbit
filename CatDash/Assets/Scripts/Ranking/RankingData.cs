using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using DG.Tweening;

public class RankingData : MonoBehaviour {


	[SerializeField, HeaderAttribute ("名前")]
	private Text nameTxt;
	[SerializeField, HeaderAttribute ("順位")]
	private Text rankTxt;
	[SerializeField, HeaderAttribute ("スコア(pt)")]
	private Text scoreTxt;
	[SerializeField, HeaderAttribute("フレーム画像")]
	private Image[] frameImg = new Image[4]; 
	[SerializeField, HeaderAttribute("フレーム画像親Obj")]
	private GameObject frameObj;

	public int user_id;

	public void Init(JsonData rankData, int num){
		
		this.frameObj.SetActive (false);
		this.nameTxt.text 	= (string)	rankData [num]["user_name"];
		int score			= (int)		rankData [num]["score"];
		this.user_id 		= (int)		rankData [num]["user_id"];
		this.scoreTxt.text 	= string.Format ("{0:#,0}Pt", score);
		this.rankTxt.text 	= (num + 1).ToString () + "位";

	}

	/// <summary>
	/// フレームを光らせる
	/// </summary>
	public void SetFrame(){
	
		this.frameObj.SetActive (true);

		for (int i = 0; i < 4; i++) {

			frameImg [i].DOColor (new Color (1f, 1f, 0f, 0f), 1f).SetLoops (-1, LoopType.Yoyo);
		
		}
	}

}
