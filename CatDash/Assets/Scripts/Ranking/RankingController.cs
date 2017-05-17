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
		//w.AddField ("auth_token", Config.AUTH_TOKEN);
		w.AddField ("auth_token", "ss");
		StartCoroutine (api.Connect (Config.URL_RANKING, w, transition, GetRanking));

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

		int index = 0; //位置ずらし用

		List<GameObject> rankList = new List<GameObject> ();
		RectTransform rect = null;


		for (int i = 0; i < json.Count-1; i++) {
		
			GameObject obj = Instantiate (rankPrefab, this.rankContents.transform) as GameObject;
			rect = obj.GetComponent<RectTransform> ();

			obj.transform.localPosition = new Vector3 (0.0f, -rect.sizeDelta.y * index - 5.0f, 0.0f);

			rankList.Add (obj);

			RankingData rankData = obj.GetComponent<RankingData> ();
			rankData.Init (json, i);
			index++;


			if (Config.USER_ID != rankData.user_id)
				continue;

			index--;
			indexRank = i;
			totalRank = json.Count - 1;
			rankData.SetFrame ();
			obj.transform.localPosition += new Vector3 (800.0f,  0.0f, 0.0f);		//右端に待機
		
		}

		//コンテンツサイズを修正
		RectTransform rank = this.rankContents.GetComponent<RectTransform> ();
		rank.sizeDelta = new Vector2 (0.0f, rect.sizeDelta.y * totalRank + 5 * (totalRank - 1));

		autoScroll.rankList = rankList;

		autoScroll.SetCenter(indexRank, totalRank);

	}




}
