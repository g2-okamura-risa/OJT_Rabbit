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

	public int user_id;
	/// <summary>
	/// 初期化
	/// </summary>
	public void Init(JsonData rankData, int num){

		this.nameTxt.text 	= (string)	rankData [num]["user_name"];
		int score			= (int)		rankData [num]["score"];
		this.user_id 		= (int)		rankData [num]["user_id"];
		this.scoreTxt.text 	= string.Format ("{0:#,0}Pt", score);
		this.rankTxt.text 	= (num + 1).ToString () + "位";
	}
		
}
