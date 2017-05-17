using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 自動スクロールクラス 
/// </summary>

public class AutoScrollComponent : MonoBehaviour {

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField] 
	private Scrollbar scrollBar;

	[SerializeField]
	private RectTransform scrollViewRectTransform;

	[SerializeField]
	private RectTransform contentsRectTransform;

	public List<GameObject> rankList = new List<GameObject>();


	private int myIndex = 0;



	/// <summary>
	/// スクロール中心セット
	/// </summary>
	/// <param name="index">移動したいコンテンツのindex.</param>
	/// <param name="totalndex">全コンテンツ数</param>
	public void SetCenter(int index, int totalNum) {
		this.myIndex = index;
		scrollBar.value = 1.0f;

		float height = scrollViewRectTransform.sizeDelta.y;
		float contentHeight = contentsRectTransform.sizeDelta.y;




		//スクロールしなくてもすべて表示されている&自分のランキングと全ランキング数が同じ
		if (contentHeight <= height && index == totalNum) {
			MoveXmyRankData ();
			return;
		}

		//スクロールしなくてもすべて表示されている&自分のランキングと全ランキング数が違う
		if (contentHeight <= height && index != totalNum) {
			MoveYotherRankData ();
			return;
		}

			

		float y = (index + 0.5f) / totalNum;  // 要素の中心座標
		float scrollY = y - 0.5f * height / contentHeight;
		float ny = scrollY / (1 - height / contentHeight);  // ScrollRect用に正規化した座標

		float target = Mathf.Clamp (1 - ny, 0f, 1f); //ScrollRectは1:top 0:bottom

		DOTween.To (() => scrollBar.value, n => scrollBar.value = n, target, 1f).OnComplete (MoveYotherRankData);


	}



	private void MoveYotherRankData(){
	
		int i = this.myIndex+1;

		float y = this.rankList [i].transform.localPosition.y;
		this.rankList [i].transform.DOLocalMoveY (y - 75.0f, 2f).OnComplete(MoveXmyRankData);
		i++;

		for (; i < rankList.Count; i++) {
			y = this.rankList [i].transform.localPosition.y;
			this.rankList [i].transform.DOLocalMoveY (y - 75.0f, 2f);
		}

	
	}


	public void MoveXmyRankData(){

		this.rankList [this.myIndex].transform.DOLocalMoveX (0, 2f);
		this.rankList.Clear ();
	
	}
}
