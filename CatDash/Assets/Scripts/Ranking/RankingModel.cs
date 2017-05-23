using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class RankingModel : MonoBehaviour {


	public List<RankingData> rankingList = new List<RankingData> ();
	public event EventHandler rankingHandler;


	/// <summary>
	///  ランキングデータ取得通信
	/// </summary>
	public IEnumerator GetRankingData(TransitionController transition){
		
		API api = new API ();
		WWWForm w = new WWWForm ();

		w.AddField ( "auth_token", Config.AUTH_TOKEN );

		return api.Connect ( Config.URL_RANKING, w, transition, SetRankingList );

	}


	private void SetRankingList(JsonData json){
	
		for ( int i = 0; i < json.Count - 1; i++ ) {
		
			RankingData rankDataModel = new RankingData ();
			rankDataModel.name 		= (string)	json [i]["user_name"];
			int score				= (int)		json [i]["score"];
			rankDataModel.user_id 	= (int)		json [i]["user_id"];


			rankDataModel.score 	= string.Format ( "{0:#,0}Pt", score );
			rankDataModel.ranking	= (i + 1).ToString () + "位";

			rankingList.Add ( rankDataModel );
			
		}

		//イベント発行
		rankingHandler ( this, EventArgs.Empty );

	}


}
