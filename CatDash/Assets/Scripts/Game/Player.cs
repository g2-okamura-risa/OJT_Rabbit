using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Player : MonoBehaviour {


	//アニメーション処理メイン
	[SerializeField, HeaderAttribute("アニメーションコントローラー")]
	private Animator animator;

	[SerializeField,HeaderAttribute("カメラコントローラー")]
	private CameraController cameraController;



	public enum PLAYER_STATE
	{
		NORMAL,
		LEFT_WALK,
		RIGHT_WALK,
		TURNOVER,
		JUMP
	};

	private PLAYER_STATE state;

	void Start(){


		this.state = PLAYER_STATE.NORMAL;

		DOTween.Init (false, true, LogBehaviour.ErrorsOnly);


	}



	public void SetState(PLAYER_STATE state){
	
		this.state = state;

		switch (this.state) {

		case PLAYER_STATE.NORMAL:
			

			break;

		case PLAYER_STATE.LEFT_WALK:
			
			animator.SetTrigger ("LEFT_WALK");
			break;

		case PLAYER_STATE.RIGHT_WALK:

			animator.SetTrigger ("RIGHT_WALK");
			break;

		case PLAYER_STATE.TURNOVER:
			animator.SetTrigger ("TURN_OVER");
			break;

		case PLAYER_STATE.JUMP:
			animator.SetBool ("JUMP", true);
			break;

		}


	
	}

}
