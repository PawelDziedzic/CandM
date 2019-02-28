using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatCollisionScript : MonoBehaviour {

	public float speed;
	public float gForce;

	protected Vector3 movement3D;
	protected Vector3 oldMovement;
	protected Vector3 newMovement3D;

	protected Vector3 rotation3D;

	protected Vector3 arrowMovement;
	protected Vector3 arrowRotation;
	protected RaycastHit hitInfo;
	protected bool isFacingLeft;
	protected float checkSpotSize;
	protected float sphereRadii;

	void Start () {
		movement3D = Vector3.zero;
		oldMovement = Vector3.zero;
		arrowMovement = Vector3.zero;
		rotation3D = Vector3.zero;
		arrowRotation = Vector3.zero;
		isFacingLeft = false;
		checkSpotSize = 0.01f;
		sphereRadii = 0.55f;
	}

	void FixedUpdate()
	{
		transform.Translate (movement3D,Space.World);
		transform.Rotate (rotation3D);
		ApplyGravity();
		movement3D += arrowMovement;
		rotation3D = arrowRotation;

		do{
			oldMovement = movement3D;
			CapsuleCastShortening();
			Debug.DrawRay(transform.position+ new Vector3(0f,-0.6f,0f),movement3D,Color.cyan);
			Debug.DrawRay(transform.position+ new Vector3(0.6f,0f,0f),movement3D,Color.cyan);
			Debug.DrawRay(transform.position+ new Vector3(-0.6f,0f,0f),movement3D,Color.cyan);
		}while(!movement3D.Equals(vectorCutOnMargin(oldMovement)));
	}

	void ApplyGravity()
	{
		movement3D.y -= gForce;
	}

	void CapsuleCastShortening()
	{
		movement3D = vectorCutOnMargin(ShorteningByCast (Vector3.zero, movement3D));
	}

	Vector3 vectorCutOnMargin(Vector3 vec)
	{
		if (vec.magnitude >= 0.005f) {
			return vec-vec.normalized*0.0025f;
		} else {
			return Vector3.zero; 
		}
	}

	Vector3 getShortestCollisionFromPoints(List<Vector3> vecList, Vector3 inputVec)
	{
		Vector3 tempShortest = ShorteningByProjections(vecList[0],inputVec);
		for(int i=1; i<vecList.Count;i++)
		{
			tempShortest = getShorterVector (tempShortest, ShorteningByProjections (vecList [i], inputVec));
		}
		return tempShortest;
	}

	Vector3 getShorterVector(Vector3 vec1, Vector3 vec2)
	{
		if (vec1.magnitude > vec2.magnitude)
			return vec2;
		else
			return vec1;
	}

	Vector3 ShorteningByProjections(Vector3 pos, Vector3 vecInput)
	{
		if (Physics.Raycast (transform.position + pos, vecInput.normalized, out hitInfo, vecInput.magnitude)) {
			newMovement3D = Vector3.Project (hitInfo.point - transform.position - pos, hitInfo.normal);
			newMovement3D += Vector3.ProjectOnPlane (movement3D, hitInfo.normal);
			newMovement3D += hitInfo.normal * checkSpotSize;
			return newMovement3D;
		} else {
			return vecInput;
		}
	}

	Vector3 ShorteningByCast(Vector3 pos, Vector3 vecInput)
	{
		if(Physics.SphereCast(transform.position + pos, sphereRadii, vecInput, out hitInfo, vecInput.magnitude)){
			drawCross (hitInfo.point, Color.magenta, 1f);
			//newMovement3D = Vector3.Project (hitInfo.point - transform.position - pos, hitInfo.normal);
			newMovement3D = Vector3.ProjectOnPlane (movement3D, hitInfo.normal);
			newMovement3D += hitInfo.normal * checkSpotSize;
			return newMovement3D;
		} else {
			return vecInput;
		}
	}

	void drawCross(Vector3 pos, Color col, float scale)
	{
		Debug.DrawRay (pos, Vector3.left*scale, col);
		Debug.DrawRay (pos, Vector3.right*scale, col);
		Debug.DrawRay (pos, Vector3.up*scale, col);
		Debug.DrawRay (pos, Vector3.down*scale, col);
		Debug.DrawRay (pos, Vector3.forward*scale, col);
		Debug.DrawRay (pos, Vector3.back*scale, col);
	}
}
