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
	private TransitionController transition;

	[SerializeField, HeaderAttribute("セッション切れモーダル")]
	private GameObject limitOverObj;

	[SerializeField, HeaderAttribute("自動スクロール")]
	private AutoScrollComponent autoScroll;

	private int indexRank = 0;
	private int totalRank = 0;

	void Awake(){

		//TODO: ランキングデータ取得通信
		API api = new API ();
		api.limitOverObj = this.limitOverObj;
		api.parent = this.gameObject;
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

		for (int i = 0; i < json.Count-1; i++) {
			GameObject obj = Instantiate (rankPrefab, this.rankContents.transform) as GameObject;
			RankingData rankData = obj.GetComponent<RankingData> ();
			rankData.Init (json, i);
			//rankList.Add (rankData);

			if (Config.USER_ID != rankData.user_id)
				continue;

			indexRank = i;
			totalRank = json.Count - 1;

		}


		StartCoroutine (AutoSet());

	}


	private IEnumerator AutoSet(){
		yield return new WaitForSeconds(1.0f);
		autoScroll.SetCenter(indexRank, totalRank);
	
	}

}
