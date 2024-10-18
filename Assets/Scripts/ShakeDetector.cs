
using UnityEngine;

[RequireComponent(typeof(PhysicsController))]

public class ShakeDetector: MonoBehaviour
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
    void FixedUpdate()
    {
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinInterval) {
            physicsController.ShakeRigitbodies(Input.acceleration);
            physicsController.ShakeDonor(Input.acceleration);
            timeSinceLastShake = Time.unscaledTime;

        }
    }
}
