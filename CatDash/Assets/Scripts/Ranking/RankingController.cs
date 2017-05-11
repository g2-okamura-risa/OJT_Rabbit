using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class RankingController : MonoBehaviour {

	[SerializeField, HeaderAttribute ("rankingData親obj")]
	private GameObject rankContents;

	[SerializeField, HeaderAttribute ("rankingDataプレハブ")]
	private GameObject rankPrefab;

	[SerializeField, HeaderAttribute("遷移コントローラ")]
	TransitionController transition;



	void Awake(){

		//TODO: ランキングデータ取得通信
		API api = new API ();

		WWWForm w = new WWWForm ();
		w.AddField ("auth_token", Config.AUTH_TOKEN);
		StartCoroutine (api.Connect (Config.URL_RANKING, w, transition, GetRanking));


	}

	// Use this for initialization
	void Start () {
		
	}


	#region ボタン

	/// <summary>
	/// タイトル画面に遷移
	/// </summary>
	public void ToTitle(){
		transition.NextScene ("scene_Title");
	}

	#endregion


	private void GetRanking(JsonData json){

		for (int i = 0; i < json.Count; i++) {
			GameObject obj = Instantiate (rankPrefab, this.rankContents.transform) as GameObject;
			RankingData rankData = obj.GetComponent<RankingData> ();
			rankData.Init (json, i);
		}
	
	}
}
