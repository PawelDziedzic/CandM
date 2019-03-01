using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DControlScript : GenericCollisionCastScript {

	protected bool isFacingLeft;
	public float speed;

	void OnEnable(){
		isFacingLeft = false;
	}

	void Update () {

		arrowMovement.x=0f;
		if (Input.GetButtonDown ("Jump")) {
			transform.Translate (2*Vector3.up);
		}

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			if (!isFacingLeft){
				transform.Rotate (new Vector3 (0f, 180f, 0));
				isFacingLeft = true;
			}
			arrowMovement.x = -speed;
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			if (isFacingLeft) {
				transform.Rotate (new Vector3 (0f, 180f, 0));
				isFacingLeft = false;
			}
			arrowMovement.x = speed;
		}
	}
}
