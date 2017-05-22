using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using LitJson;

public class API : MonoBehaviour{

	private Action< LitJson.JsonData > callback;
	private TransitionController transition;
	/// <summary>エラーモーダルの親obj </summary>
	public GameObject parent;
	/// <summary>エラーモーダルプレハブ </summary>
	public GameObject limitOverObj;


	public IEnumerator Connect ( string path, WWWForm data, TransitionController transition, Action< JsonData > callback=null ) {
		//TODO:トークンチェック追加
		using ( WWW www = new WWW ( path, data ) ) {

			while ( !www.isDone ) {
				Debug.Log ( "NowLoading" );
				yield return www;
			}

			if ( !string.IsNullOrEmpty ( www.error ) ) {
				Debug.LogError ( "www Error:" + www.error );
				yield break;
			}

			Debug.Log ( www.text );
			Debug.Log ( path );

			this.callback = callback;
			this.transition = transition;
			this.GetData ( www );
		}

	}


	private void GetData( WWW w ){

		LitJson.JsonData data = LitJson.JsonMapper.ToObject( w.text );

		//エラーが帰ってきたらモーダル表示
		if ( ( int )data ["error_state"] == 1 ) {
			//有効期限切れ
			GameObject sessionObj = Instantiate ( this.limitOverObj, this.parent.transform ) as GameObject;
			sessionObj.SetActive (true);
			sessionObj.GetComponent<LimitModalController> ().transition = this.transition;
			sessionObj.transform.DOScale ( new Vector3 ( 1.0f, 1.0f, 1.0f ), 0.7f );

			return;
		}
		if ( this.callback != null )
			this.callback ( data );

	}

}


