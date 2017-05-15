using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrollController : MonoBehaviour {

	/*[SerializeField]
	private Renderer bgTree;

	[SerializeField]
	private Renderer bgField;

	[SerializeField]
	private Renderer bgBottom;


	[SerializeField]
	private float speedTree;
	[SerializeField]
	private float speedField;
	[SerializeField]
	private float speedBottom;


	//private float scroll;
	//private Vector2 offset;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float scroll = Mathf.Repeat (Time.time * speedTree, 1);
		Vector2 offset = new Vector2 (scroll, 0);
		bgTree.sharedMaterial.SetTextureOffset ("_MainTex", offset);

		float scroll1 = Mathf.Repeat (Time.time * speedField, 1);
		Vector2 offset1 = new Vector2 (scroll1, 0);

		bgField.sharedMaterial.SetTextureOffset ("_MainTex", offset1);


		float scroll2 = Mathf.Repeat (Time.time * speedBottom, 1);
		Vector2 offset2 = new Vector2 (scroll2, 0);
		bgBottom.sharedMaterial.SetTextureOffset ("_MainTex", offset2);
	}*/




	public void SetScroll(){
	
	
		this.transform.position += new Vector3 (-0.135f, 0.0f, 0.0f);
	
	
	
	}
}
