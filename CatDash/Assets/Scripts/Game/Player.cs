using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


	//アニメーション処理メイン
	[SerializeField, HeaderAttribute("アニメーションコントローラー")]
	private Animator animator;

	[SerializeField,HeaderAttribute("カメラコントローラー")]
	private CameraController cameraController;


	public void Set(string tab){

	
	
	}

	public void Move(){
	
		this.transform.localPosition += new Vector3(1f,0.0f,0.0f);
		cameraController.SetPos (this.transform.localPosition.x);
	}


	public void Walk(){

	
	
	}

}
