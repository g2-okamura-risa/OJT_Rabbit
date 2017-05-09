using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class RankingData : MonoBehaviour {


	[SerializeField, HeaderAttribute ("名前")]
	private Text nameTxt;
	[SerializeField, HeaderAttribute ("順位")]
	private Text rankTxt;
	[SerializeField, HeaderAttribute ("スコア(pt)")]
	private Text scoreTxt;

	/// <summary>
	/// 初期化
	/// </summary>
	public void Init(JsonData rankData, int num){

		var json = rankData ["ranking_data"];

		this.nameTxt.text 	= (string)	json [num]["user_name"];
		int score			= (int)		json [num]["score"];
		this.scoreTxt.text 	= string.Format ("{0:#,0}Pt", score);
		this.rankTxt.text 	= (num + 1).ToString ();

	}
}
