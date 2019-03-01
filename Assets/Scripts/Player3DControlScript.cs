using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DControlScript : GenericCollisionCastScript {

	public float speed;
	void Update () {
		arrowMovement.z = 0f;

		if (Input.GetButtonDown ("Jump")) {
			transform.Translate (2*Vector3.up);
		}

		if (Input.GetKey (KeyCode.UpArrow)) 
		{
			arrowMovement = transform.forward*speed;
		}

		if (Input.GetKey (KeyCode.DownArrow)) 
		{
			arrowMovement = -transform.forward*speed*0.8f;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			Debug.Log ("Left arrow");
			arrowRotation.y = -50*speed;
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			Debug.Log ("right arrow");
			arrowRotation.y = 50*speed;
		}
	}
}
