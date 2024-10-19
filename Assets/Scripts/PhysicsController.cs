
using UnityEngine;

public class PhysicsController : MonoBehaviour {
    // Start is called before the first frame update
    public float ShakeForceMultiplier;
    public int circleCount;
    public GameObject prefab;
    public GameObject[] circles;
    public GameObject Donor;
    private Rigidbody2D rb;




    public Rigidbody2D[] rigidbodies;
    private void Awake() {
        circles = new GameObject[circleCount];
        rigidbodies = new Rigidbody2D[circleCount];

        rb = Donor.GetComponent<Rigidbody2D>();
        Camera mainCamera = Camera.main;
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;
        for (int i = 0; i < circleCount; i++) {
            float randomX = Random.Range(-screenWidth + 1, screenWidth - 1);
            float randomY = Random.Range(-screenHeight + 1, screenHeight - 1);
            circles[i] = Instantiate(prefab, new Vector2(randomX, randomY), Quaternion.identity);
            rigidbodies[i] = circles[i].GetComponent<Rigidbody2D>();
        }
    }


    public void ShakeRigitbodies(Vector3 devAcceleration) {

        foreach (var rigidbody in rigidbodies) {

            rigidbody.AddForce(devAcceleration * ShakeForceMultiplier, ForceMode2D.Impulse);

        }
    }
    public void ShakeDonor(Vector3 devAcceleration) {
        rb.AddForce(devAcceleration * ShakeForceMultiplier, ForceMode2D.Impulse);
    }
}
