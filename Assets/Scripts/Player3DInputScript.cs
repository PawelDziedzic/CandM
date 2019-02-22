﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DInputScript : CollisionMomentumScript {

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
			transform.Rotate (0f,-1f,0f);
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			Debug.Log ("right arrow");
			transform.Rotate (new Vector3 (0f, 1f, 0f));
		}
	}
}