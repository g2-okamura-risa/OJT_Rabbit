using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text;

public class NameModalController : MonoBehaviour {

	[SerializeField, HeaderAttribute ("名前入力txt")]
	private Text nameTxt;
	[SerializeField, HeaderAttribute ("注意文言")]
	private GameObject cautionObj;



	private string userName;
	private Encoding encJIS = Encoding.GetEncoding("Shift_JIS");

	void Start(){

		this.cautionObj.SetActive (false);

		DOTween.Init (false, true, LogBehaviour.ErrorsOnly);
	

		this.gameObject.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 1.0f);
	}


	/// <summary>
	/// 入力カーソルが外れたら実行
	/// </summary>
	public void NameInput(){

		this.userName = nameTxt.text;

	}



	public void DecisionButton(){
	
		if (string.IsNullOrEmpty (this.userName)) {
			cautionObj.SetActive (true);
			return;
		}

		int num = encJIS.GetByteCount(this.userName);
		bool isZenkaku = ( num == userName.Length * 2);

		if (!isZenkaku) {
			cautionObj.SetActive (true);
			return;
		}


		//通信開始OK 遷移
		cautionObj.SetActive (false);
	}
}
