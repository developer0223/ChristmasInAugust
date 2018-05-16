using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CustomGravity : MonoBehaviour
{
    float GravityPower = 10000.0f;

    Rigidbody2D myRigidBody;
    Transform myTransform;
    // BoxCollider triggerCollider;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        /*
        if (myTransform.childCount > 0)
        {
            triggerCollider = myTransform.GetChild(0).GetComponent<BoxCollider>();
        }
        */
    }

    void Start()
    {
        myRigidBody.simulated = true;
        Vector3 distance = (Vector3.zero - myTransform.position).normalized;
        myRigidBody.AddForce(distance * GravityPower * Time.deltaTime);
    }

    void Update()
    {
        if (myTransform.position != Vector3.zero)
        {
            if (myRigidBody.velocity != Vector2.zero)
            {
                Vector2 distance = (Vector3.zero - myTransform.position).normalized;
                myRigidBody.AddForce(distance * GravityPower * Time.deltaTime);
            }
        }
        else
        {
            // Debug.Log($"velocity : 0 // {myRigidBody.velocity}");
        }
    }
}