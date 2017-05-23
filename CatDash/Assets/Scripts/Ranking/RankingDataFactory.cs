using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//生成機能のみ付け加えない
public class RankingDataFactory : MonoBehaviour {


	[SerializeField, HeaderAttribute("プレハブ")]
	private GameObject prefab;


	public GameObject CreateObj(RankingDataModel dateModel){
	
		GameObject obj = Instantiate( prefab );
		obj.GetComponent<RankingDataController> ().Init ( dateModel );

		return obj;
	
	}


}
