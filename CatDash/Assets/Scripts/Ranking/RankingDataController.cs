using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using DG.Tweening;

public class RankingDataController : MonoBehaviour {


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

	public void Init(RankingDataModel rankData){
		
		this.frameObj.SetActive (false);
		this.nameTxt.text 	= rankData.name;
		this.user_id 		= rankData.user_id;
		this.scoreTxt.text 	= rankData.score;
		this.rankTxt.text 	= rankData.ranking;

		if ( Config.USER_ID == this.user_id ) {
			SetFrame ();
		
		}
	}

	/// <summary>
	/// フレームを光らせる
	/// </summary>
	private void SetFrame(){
	
		this.frameObj.SetActive (true);

		for (int i = 0; i < 4; i++) {

			frameImg [i].DOColor (new Color (1f, 1f, 0f, 0f), 1f).SetLoops (-1, LoopType.Yoyo);
		
		}
	}

}
