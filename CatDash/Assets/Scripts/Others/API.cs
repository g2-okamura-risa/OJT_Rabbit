using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using LitJson;

public class API: MonoBehaviour{

	private Action< LitJson.JsonData > callback;
	private TransitionController transition;
	/// <summary>エラーモーダルの親obj </summary>
	public GameObject parent;
	/// <summary>エラーモーダルプレハブ </summary>
	public GameObject limitOverObj;


	public IEnumerator Connect ( string path, WWWForm data, TransitionController transition, Action< JsonData > callback=null ) {
		//TODO:トークンチェック追加
		using(WWW www = new WWW( path, data )){

			Debug.Log(path);

			while (!www.isDone) {
				Debug.Log ( "NowLoading" );
				yield return www;
			}

			if(!string.IsNullOrEmpty(www.error)){
				Debug.LogError ( "www Error:" + www.error );
				yield break;
			}

			Debug.Log ( www.text );
			Debug.Log ( path );

			this.callback 	= callback;
			this.transition = transition;
			this.GetData ( www );

		}
	}



	/// <summary>
	/// データ受け取り用
	/// </summary>
	/// <returns>The get.</returns>
	/// <param name="path">Path.</param>
	/// <param name="callback">Callback.</param>
	public IEnumerator ConnectGet ( string path, TransitionController transition, Action< JsonData > callback=null ) {

		Debug.Log (path);

		using(WWW www = new WWW(path)){

			while (!www.isDone) {
				Debug.Log ( "NowLoading" );
				yield return www;
			}

			if(!string.IsNullOrEmpty(www.error)){
				Debug.LogError ( "www Error:" + www.error );
				yield break;
			}

			this.callback 	= callback;
			this.transition = transition;
			Debug.Log ( www.text );
			this.GetData ( www );

		}
	}



	/// <summary>
	/// データ送信用
	/// </summary>
	/// <returns>The send.</returns>
	/// <param name="path">Path.</param>
	/// <param name="data">Data.</param>
	/// <param name="callback">Callback.</param>
	public IEnumerator ConnectSend ( string path, WWWForm data, Action callback = null ) {

		//TODO:トークンチェック追加

		using( WWW www = new WWW( path, data ) ){

			while ( !www.isDone ) {
				Debug.Log ( "NowLoading" );
				yield return www;
			}

			if( !string.IsNullOrEmpty( www.error ) ){
				Debug.LogError( "www Error:" + www.error );
				yield break;
			}

			callback();

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


