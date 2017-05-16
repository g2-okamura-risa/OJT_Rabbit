using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 自動スクロールクラス 
/// </summary>

public class AutoScrollComponent : MonoBehaviour {

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private RectTransform scrollViewRectTransform;

	[SerializeField]
	private RectTransform contentsRectTransform;


	private int index;
	private int totalNum;
	private float target;
	private float count = 0f;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		if (target <= scrollRect.verticalNormalizedPosition) {
			return;
		}
		 
		scrollRect.verticalNormalizedPosition += 0.01f * Time.deltaTime;

	}

	/// <summary>
	/// スクロール中心セット
	/// </summary>
	/// <param name="index">移動したいコンテンツのindex.</param>
	/// <param name="totalndex">全コンテンツ数</param>
	public void SetCenter(int index, int totalNum) {

		//this.index = index;
		//this.totalNum = totalNum;


		float height = scrollViewRectTransform.sizeDelta.y;
		float contentHeight = contentsRectTransform.sizeDelta.y;
		// コンテンツよりスクロールエリアのほうが広いので、スクロールしなくてもすべて表示されている
		if (contentHeight <= height)  
			return;

		float y = (index + 0.5f) / totalNum;  // 要素の中心座標
		float scrollY = y - 0.5f * height / contentHeight;
		//float ny = scrollY / (1 - height / contentHeight);  // ScrollRect用に正規化した座標

		target = Mathf.Clamp (1 - scrollY, 0, 1);


	}

}
