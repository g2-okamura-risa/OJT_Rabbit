using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;


public class RankingController : MonoBehaviour {

	[SerializeField, HeaderAttribute ("rankingData親obj")]
	private GameObject rankContents;

	[SerializeField, HeaderAttribute("rankingData親Rect")]
	private RectTransform rankContentRect;

	[SerializeField, HeaderAttribute("rankDataファクトリー")]
	private RankingDataFactory rankFactory;

	[SerializeField, HeaderAttribute("遷移コントローラ")]
	private TransitionController transition;

	[SerializeField, HeaderAttribute("セッション切れモーダル")]
	private GameObject limitOverObj;

	[SerializeField, HeaderAttribute("自動スクロール")]
	private AutoScrollComponent autoScroll;

	[SerializeField, HeaderAttribute("ランキングデータの間隔サイズ")]
	private float rankDataBuffY = 5.0f; 

	[SerializeField, HeaderAttribute("フェード画像")]
	private Image fadeImage;

	private RankingModel rankingModel = new RankingModel();
	private List<RankingDataModel> rankList = new List<RankingDataModel> ();
	private List<GameObject> rankGameObjList = new List<GameObject>();//アニメーション用




	void Awake(){
		InitFadeImage ();
		rankingModel.rankingHandler += this.CreateRankingData;
		StartCoroutine ( rankingModel.GetRankingData ( transition ) );
	
	}



	/// <summary>
	/// タイトル画面に遷移
	/// </summary>
	public void ToTitle(){
		
		transition.NextScene ( "scene_Title" );
	
	}
		

	/// <summary>
	/// ランキングデータ生成
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	private void CreateRankingData(object sender, EventArgs e){

		rankList = rankingModel.rankingList;
		GameObject obj 				= null;

		for ( int i = 0; i < rankList.Count; i++ ){

			//生成
			obj = rankFactory.CreateObj(rankList [i]);
			obj.transform.parent = this.rankContents.transform;

			rankGameObjList.Add ( obj );

		}

		AnimationSet ();


		SetFadeOut ();
	}

	private void InitFadeImage(){
		
		fadeImage.color = new Color (0f, 0f, 0f, 1f);
	
	}

	private void SetFadeOut(){
		
		fadeImage.DOColor (new Color (0f, 0f, 0f, 0f), 0.8f);
	}



	private void AnimationSet(){

		int index = 0;
		int myIndex = 0;
		RectTransform rankRect = null;
		for ( int i = 0; i < rankGameObjList.Count; i++ ){

	
			rankRect = rankGameObjList[i].GetComponent<RectTransform> ();

			//オブジェクト生成位置を等間隔
			rankGameObjList[i].transform.localPosition = new Vector3 ( 0.0f, -( rankRect.sizeDelta.y * index + rankDataBuffY ), 0.0f );

			index++;

			if ( Config.USER_ID != rankList[i].user_id )
				continue;

			index--;
			myIndex = i;

	
			float posX = Mathf.Abs( rankContentRect.sizeDelta.x );
			if ( posX <= 0f ) {
				posX = 800f;	//念のため
			}
			rankGameObjList[i].transform.localPosition += new Vector3 ( posX,  0.0f, 0.0f );		//右端に待機

		}

		//コンテンツサイズを修正
		rankContentRect.sizeDelta = new Vector2 ( 0.0f, rankRect.sizeDelta.y * rankList.Count + rankDataBuffY * ( rankList.Count - 1 ) );

		autoScroll.rankList = rankGameObjList;

		autoScroll.SetCenter ( myIndex, rankList.Count, rankRect.sizeDelta.y + rankDataBuffY );

	
	}




}

