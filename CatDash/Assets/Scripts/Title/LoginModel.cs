using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class LoginModel{

	private string uuid;
	private string name;


	//イベント発行
	public event EventHandler newUserDataHandler;
	public event EventHandler transitionHandler;

	public IEnumerator Loading(TransitionController transition){

		uuid = PlayerPrefs.GetString ( Config.PREFS_KEY_UUID );

		if ( uuid == "" ) {

			newUserDataHandler ( this, EventArgs.Empty );
			this.CreateUUID ();
			yield break;
		} 




		//uuid送信しtokenもらう
		WWWForm w = new WWWForm();
		w.AddField ( "uuid", uuid );

		if ( name != null ) {
		
			w.AddField ( "user_name", name );
		
		}

		API api = new API ();
	
		yield return api.Connect ( Config.URL_LOGIN, w, transition, GetUserData );
	
	}


	private void GetUserData( JsonData json ){

		Config.AUTH_TOKEN 	= (string)	json["auth_token"];
		Config.USER_ID 		= (int)		json ["user_id"];
		transitionHandler ( this, EventArgs.Empty );
	}
		


	private void CreateUUID(){

		uuid = System.Guid.NewGuid ().ToString ();
		PlayerPrefs.SetString (Config.PREFS_KEY_UUID, uuid);
		PlayerPrefs.Save ();

	}


	public void SetUserName(string name){
		this.name = name;
	}








}




