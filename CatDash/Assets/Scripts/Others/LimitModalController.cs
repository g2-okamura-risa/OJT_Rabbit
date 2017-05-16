using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitModalController : MonoBehaviour {

	[SerializeField, HeaderAttribute("テキスト")]
	private Text txtMessage;

	[SerializeField, HeaderAttribute("ボタン") ]
	private GameObject btnObj;

	public TransitionController transition;

	public void Start(){

		if (Application.loadedLevelName == "scene_Title") {
			btnObj.SetActive (false);
			txtMessage.text = "アプリを再起動してください";
		} else {
			txtMessage.text = "タイトルに戻ります";
			btnObj.SetActive (true);
		}

	}

	public void ToTitle(){
		transition.NextScene ("scene_Title");
	}
}
