using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer2DInputScript : CollisionMomentumScript {

	// Use this for initialization
	void Start () {

		movement3D = Vector3.zero;
		oldMovement = Vector3.zero;
		arrowMovement = Vector3.zero;
		isFacingLeft = false;

		checkSpotSize = 0.01f;
		checkSpotsList = new List<Vector3> (){
			new Vector3(0f,0.74f,0f),
			Vector3.zero,
			new Vector3(0f,-0.74f,0f)
		};
	}

	// Update is called once per frame
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
