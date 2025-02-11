
using UnityEngine;

[RequireComponent(typeof(PhysicsController))]

public class ShakeDetector : MonoBehaviour
{
    public float ShakeDetectionThreshold;
    public float MinInterval;
    public float ShakeForceDonor;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;
    private PhysicsController _physicsController;


    void Start()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
        _physicsController = GetComponent<PhysicsController>();

    }

    void Update() {
        /*if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinInterval) {
            physicsController.ShakeRigitbodies(Input.acceleration);
            physicsController.ShakeDonor(Input.acceleration);
            timeSinceLastShake = Time.unscaledTime;

        }*/
        ShakeHandling();
    }

    private void ShakeHandling() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {

            _physicsController.IncreaseSaturation();
            _physicsController.ShakeRigitbodies2();
        }
    }
}
