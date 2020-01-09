using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPlayer2DScript : MonoBehaviour
{
    protected Vector3 movement3D;
    protected Vector3 oldMovement;
    protected Vector3 rotation3D;

    protected Vector3 arrowMovement;
    protected Vector3 arrowRotation;

    protected float checkSpotSize;
    protected float sphereRadii;
    protected List<Vector3> checkSpotsList;
    protected List<SphereCollider> spheresList;

    Color shade;


    void Start()
    {
        oldMovement = Vector3.zero;
        arrowMovement = Vector3.zero;
        rotation3D = Vector3.zero;
        arrowRotation = Vector3.zero;
        checkSpotSize = 0.01f;
        //sphereRadii = transform.localScale.z * 0.5f + 0.05f;
        sphereRadii = 0.5f;//<<<<<<<<<<<
        
        checkSpotsList = new List<Vector3>(){
            new Vector3(0f,0f,0f)
        };
        shade = new Color(1f, 0f, 0f);
    }

    public float gForce;


    void FixedUpdate()
    {
        //transform.Translate(movement3D, Space.World);
        //transform.Rotate(rotation3D);

        Debug.Log("1 " + transform.name + " " + Time.realtimeSinceStartup);
        ApplyGravity();
        movement3D += arrowMovement;
        rotation3D = arrowRotation;
        Debug.DrawRay(transform.position + new Vector3(0f, -0.5f, 0f), movement3D, Color.green);

        CapsuleCastShortening();


        transform.Translate(movement3D, Space.World);
        transform.Rotate(rotation3D);
        Debug.Log("2 " + transform.name + " " + Time.realtimeSinceStartup);
    }

    void ApplyGravity()
    {
        movement3D.y -= gForce;
    }

    void CapsuleCastShortening()
    {
        //movement3D = vectorCutOnMargin(getShortestCollisionFromSpheres(checkSpotsList, movement3D));
        movement3D = vectorCutOnMargin(ShorteningBySweep(movement3D));
    }

    Vector3 vectorCutOnMargin(Vector3 vec)
    {
        if (vec.magnitude >= 0.005f)
        {
            return vec - vec.normalized * 0.0025f;
        }
        else
        {
            return Vector3.zero;
        }
    }
    

    Vector3 getShortestCollisionFromSpheres(List<Vector3> vecList, Vector3 inputVec)
    {
        Vector3 tempShortest = ShorteningByCast(vecList[0], inputVec);
       
        for (int i = 1; i < vecList.Count; i++)
        {
            tempShortest = getShorterVector(tempShortest, ShorteningByCast(vecList[i], inputVec));
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

    Vector3 ShorteningBySweep(Vector3 vecInput)
    {
        RaycastHit hitInfo;
        if(myRb.SweepTest(vecInput, out hitInfo, vecInput.magnitude))
        {
            drawCross(hitInfo.point, Color.magenta, 0.1f);
            Debug.DrawRay(hitInfo.point - vecInput.normalized * hitInfo.distance, vecInput, Color.green);
            Debug.DrawRay(transform.position + Vector3.right * 0.375f, vecInput, Color.green);
            Vector3 newMovement3D = Vector3.ProjectOnPlane(vecInput, hitInfo.normal);
            newMovement3D += Vector3.Project(vecInput.normalized * hitInfo.distance, hitInfo.normal);
            newMovement3D += hitInfo.normal * checkSpotSize;
            Debug.DrawRay(hitInfo.point - vecInput.normalized * hitInfo.distance, newMovement3D, Color.cyan);
            Debug.DrawRay(transform.position + Vector3.right*0.375f, newMovement3D, Color.cyan);
            return ShorteningBySweep(newMovement3D);
        }
        else
            return vecInput;
    }

    Vector3 ShorteningByCast(Vector3 pos, Vector3 vecInput)
    {
        RaycastHit hitInfo;
        //if(Physics.CapsuleCast(transform.position + Vector3.up*0.375f, transform.position - Vector3.up * 0.375f,0.375f, vecInput,out hitInfo, vecInput.magnitude))
        if (Physics.SphereCast(transform.position + pos, sphereRadii, vecInput, out hitInfo, vecInput.magnitude))
        {
            drawCross(hitInfo.point, Color.magenta, 0.1f);
            Debug.DrawRay(hitInfo.point - vecInput.normalized * hitInfo.distance, movement3D, Color.green);

            Vector3 newMovement3D = Vector3.ProjectOnPlane(vecInput, hitInfo.normal);
            newMovement3D += Vector3.Project(vecInput.normalized * hitInfo.distance, hitInfo.normal);
            newMovement3D += hitInfo.normal * checkSpotSize;
            Debug.DrawRay(hitInfo.point - vecInput.normalized * hitInfo.distance, newMovement3D, Color.cyan);
            return ShorteningByCast(pos, newMovement3D);
        }
        else
        {
            return vecInput;
        }
    }



    void drawCross(Vector3 pos, Color col, float scale)
    {
        Debug.DrawRay(pos, Vector3.left * scale, col);
        Debug.DrawRay(pos, Vector3.right * scale, col);
        Debug.DrawRay(pos, Vector3.up * scale, col);
        Debug.DrawRay(pos, Vector3.down * scale, col);
        Debug.DrawRay(pos, Vector3.forward * scale, col);
        Debug.DrawRay(pos, Vector3.back * scale, col);
    }


    protected bool isFacingLeft;
    public float speed;
    protected Rigidbody myRb;


    void OnEnable()
    {
        isFacingLeft = false;
        myRb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        arrowMovement.x = 0f;
        if (Input.GetButtonDown("Jump"))
        {
            transform.Translate(2 * Vector3.up);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!isFacingLeft)
            {
                transform.Rotate(new Vector3(0f, 180f, 0));
                isFacingLeft = true;
            }
            arrowMovement.x = -speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (isFacingLeft)
            {
                transform.Rotate(new Vector3(0f, 180f, 0));
                isFacingLeft = false;
            }
            arrowMovement.x = speed;
        }
    }
}

