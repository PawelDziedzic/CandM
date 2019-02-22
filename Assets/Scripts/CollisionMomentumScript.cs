using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMomentumScript : MonoBehaviour {

	public float speed;
	public float gForce;

	protected Vector3 movement3D;
	protected Vector3 oldMovement;
	protected Vector3 newMovement3D;

	protected Vector3 arrowMovement;
	protected RaycastHit hitInfo;
	protected bool isFacingLeft;
	protected float checkSpotSize;

	protected List<Vector3> checkSpotsList;

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

	void FixedUpdate()
	{
		transform.Translate (movement3D,Space.World);
		ApplyGravity();
		movement3D += arrowMovement;

		do{
			oldMovement = movement3D;
			ThreePointShortening();
			for(int i=0; i<checkSpotsList.Count;i++)
			{
				Debug.DrawRay(transform.position+checkSpotsList[i],movement3D,Color.cyan);
			}
		}while(!movement3D.Equals(vectorCutOnMargin(oldMovement)));
	}

	void ApplyGravity()
	{
		movement3D.y -= gForce;
	}

	void ThreePointShortening()
	{
		movement3D = vectorCutOnMargin(getShortestCollisionFromPoints(checkSpotsList,movement3D));
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
