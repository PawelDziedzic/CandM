using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleCollisionCastScript : GenericPhysicsPropertiesScript {

	void Start () {
		movement3D = Vector3.zero;
		oldMovement = Vector3.zero;
		arrowMovement = Vector3.zero;
		rotation3D = Vector3.zero;
		arrowRotation = Vector3.zero;
		checkSpotSize = 0.01f;
		sphereRadii = transform.localScale.z*0.5f-0.05f;

		checkSpotsList = new List<Vector3> (){
			new Vector3(0f,-transform.localScale.y*0.5f,0f),
			new Vector3(0f,transform.localScale.y*0.5f,0f)
		};

		spheresList = new List<SphereCollider> ();
	}
}
