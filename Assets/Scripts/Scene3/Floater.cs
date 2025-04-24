using UnityEngine;

public class BoatFloater : MonoBehaviour
{
    public Rigidbody rigidBody;  
    public Transform[] floatPoints; 
    public float depthBeforeSubmerged = 1f; 
    public float displacementAmount = 3f; 
    public float waterDrag = 0.99f;  
    public float waterAngularDrag = 0.5f;
    public float forwardSpeed = 5f; 
    public float waterLevel = 0f; 

    void Start()
    {
        rigidBody.centerOfMass = new Vector3(0f, -1f, 0f); 
    }

    private void FixedUpdate()
    {
        float floatersInWater = 0;

        foreach (Transform point in floatPoints)
        {
            float waterHeightAtPoint = waterLevel; 
            float depth = waterHeightAtPoint - point.position.y; 

            if (depth > 0f) 
            {
                float floatForce = Mathf.Clamp01(depth / depthBeforeSubmerged) * displacementAmount;
                Vector3 buoyancyForce = Vector3.up * floatForce * Mathf.Abs(Physics.gravity.y);
                rigidBody.AddForceAtPosition(buoyancyForce, point.position, ForceMode.Acceleration);
                floatersInWater++;
            }
        }

        if (floatersInWater > 0)
        {
            rigidBody.drag = waterDrag;
            rigidBody.angularDrag = waterAngularDrag;

            Vector3 forwardForce = transform.right * forwardSpeed;
            rigidBody.AddForce(forwardForce, ForceMode.Acceleration);
        }
        else
        {
            rigidBody.drag = 0.1f;
            rigidBody.angularDrag = 0.1f;
        }
    }

 
}