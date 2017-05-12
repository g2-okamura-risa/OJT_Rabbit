using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {


	/// <summary> UUID</summary>
	public static string USER_UUID{ get; set; }
	/// <summary> トークン</summary>
	public static string AUTH_TOKEN{ get; set; }


	#region PlayerPrefs key

	public const string PREFS_KEY_NAME = "userName";
	public const string PREFS_KEY_UUID = "uuid";

	#endregion

	#region urlパス

	public const string URL_LOGIN 	= "http://210.140.85.92:12083/sample/testlogin/login"; 
	public const string URL_RESULT 	= "http://210.140.85.92:12083/sample/testfinish/finish"; 	//token,goal_time,turnover_num送信
	public const string URL_RANKING = "http://210.140.85.92:12083/ranking/testranking/ranking";

	#endregion

}
