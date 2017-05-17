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

	/// <summary>
	/// スクロール中心セット
	/// </summary>
	/// <param name="index">移動したいコンテンツのindex.</param>
	/// <param name="totalndex">全コンテンツ数</param>
	public void SetCenter(int index, int totalNum) {

		scrollBar.value = 1.0f;

		float height = scrollViewRectTransform.sizeDelta.y;
		float contentHeight = contentsRectTransform.sizeDelta.y;
		//スクロールしなくてもすべて表示されている
		if (contentHeight <= height)  
			return;

		float y = (index + 0.5f) / totalNum;  // 要素の中心座標
		float scrollY = y - 0.5f * height / contentHeight;
		float ny = scrollY / (1 - height / contentHeight);  // ScrollRect用に正規化した座標

		float target = Mathf.Clamp (1 - ny, 0f, 1f);

		DOTween.To (() => scrollBar.value, n => scrollBar.value = n, target, 1f);

	}

}
