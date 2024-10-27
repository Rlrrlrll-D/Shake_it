
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
    //[System.Obsolete]
    void FixedUpdate()
    {
<<<<<<< HEAD
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinInterval)
        {
=======
        /*if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinInterval) {
>>>>>>> 2e91d2addfd86843abca48dec9fe36fc0ff858fa
            physicsController.ShakeRigitbodies(Input.acceleration);
            physicsController.ShakeDonor(Input.acceleration);
            timeSinceLastShake = Time.unscaledTime;

        }*/
        if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space)) {
            
            physicsController.ShakeRigitbodies2();
            physicsController.ShakeDonor2();
        }

        physicsController.ShakeWithSpaceButton();

  
    }
}
