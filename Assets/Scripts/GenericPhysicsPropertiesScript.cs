﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPhysicsPropertiesScript : MonoBehaviour {

	protected Vector3 movement3D;
	protected Vector3 oldMovement;
	protected Vector3 newMovement3D;

	protected Vector3 rotation3D;

	protected Vector3 arrowMovement;
	protected Vector3 arrowRotation;
	protected RaycastHit hitInfo;
	protected float checkSpotSize;
	protected float sphereRadii;
	protected List<Vector3> checkSpotsList;

	void Start () {
		movement3D = Vector3.zero;
		oldMovement = Vector3.zero;
		arrowMovement = Vector3.zero;
		rotation3D = Vector3.zero;
		arrowRotation = Vector3.zero;
		checkSpotSize = 0.01f;
		sphereRadii = 0.5f;

		checkSpotsList = new List<Vector3> (){
			new Vector3(0f,0f,0f)
		};
	}
}
