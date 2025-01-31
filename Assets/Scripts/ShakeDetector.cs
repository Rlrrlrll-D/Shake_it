
using UnityEngine;

[RequireComponent(typeof(PhysicsController))]

public class ShakeDetector : MonoBehaviour
{
    public float ShakeDetectionThreshold;
    public float MinInterval;
    public float ShakeForceDonor;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;
    private PhysicsController physicsController;


    void Start()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
        physicsController = GetComponent<PhysicsController>();

    }

    // Update is called once per frame
    //[System.Obsolete]
    //[System.Obsolete]
    void FixedUpdate()
    {
        /*if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinInterval) {
            physicsController.ShakeRigitbodies(Input.acceleration);
            physicsController.ShakeDonor(Input.acceleration);
            timeSinceLastShake = Time.unscaledTime;

        }*/
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {

            physicsController.ShakeRigitbodies2();
            physicsController.ShakeDonor2();
        }
    }
}
