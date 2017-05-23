using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class TransitionController : MonoBehaviour {

	[SerializeField, HeaderAttribute("フェード用Obj")]
	private Image curtainImg;

	[SerializeField, HeaderAttribute("%表示用テキスト")]
	private Text loadingTxt;

	[SerializeField, HeaderAttribute("%バー")]
	private Slider slider;

	[SerializeField]
	private GameObject loadingObj;

	private string sceneName;


	void Start()
	{
		curtainImg.color = new Color (0f, 0f, 0f, 1f);
		curtainImg.DOColor (new Color (0f, 0f, 0f, 0f), 0.8f);
	}


	public void NextScene(string sceneName){


		this.sceneName = sceneName;
		this.Process1 ();


	}
		
	private void Process1(){
		
		curtainImg.DOColor (new Color (0f, 0f, 0f, 1f),0.8f).OnComplete (() => {Process2();});
	}

	private void Process2(){

		SceneManager.LoadScene(sceneName);
	}



	#region test

	IEnumerator LoadingScene(string sceneName){
	
		AsyncOperation async = Application.LoadLevelAsync(sceneName);
		async.allowSceneActivation = false;    // シーン遷移をしない

		while (async.progress < 0.9f) {
			Debug.Log(async.progress);
			loadingTxt.text = (async.progress * 100).ToString("F0") + "%";
			slider.value = async.progress;
			yield return new WaitForEndOfFrame();
		}

		Debug.Log("Scene Loaded");

		loadingTxt.text = "100%";
		slider.value = 1;

		yield return new WaitForSeconds(1);

		async.allowSceneActivation = true;    // シーン遷移許可
	
	}

	#endregion
}
