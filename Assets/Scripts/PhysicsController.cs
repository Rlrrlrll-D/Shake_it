
using UnityEngine;
using System.Linq; // Add this line at the top of the file

public class PhysicsController : MonoBehaviour {
    public float ShakeForceMultiplier;
    public int circleCount;
    public GameObject prefab;
    public GameObject[] circles;
    public GameObject Donor;
    private Rigidbody2D rb;
    private Color _donorColor;
    private SpriteRenderer _donorSprite;

    [SerializeField] private float saturationIncrement = 0.01f;
    [SerializeField] private float _minX, _maxX;
    [SerializeField] private float _minY, _maxY;
    [SerializeField] private float _speed;

    public Rigidbody2D[] rigidbodies;

    private void Awake() {
        circles = new GameObject[circleCount];
        rigidbodies = new Rigidbody2D[circleCount];

        rb = Donor.GetComponent<Rigidbody2D>();
        _donorSprite = Donor.GetComponent<SpriteRenderer>();
        _donorColor = _donorSprite.color;

        float donorH, donorS, donorV;
        Color.RGBToHSV(_donorColor, out donorH, out donorS, out donorV);
        Camera mainCamera = Camera.main;
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        CircleInstantiate(donorH, donorV, screenWidth, screenHeight);
    }

    private void Update() {
        CheckGameEnd();
    }

    private void CircleInstantiate(float donorH, float donorV, float screenWidth, float screenHeight) {
        for (int i = 0; i < circleCount; i++) {
            float randomX = Random.Range(-screenWidth + 1, screenWidth - 1);
            float randomY = Random.Range(-screenHeight + 1, screenHeight - 1);
            GameObject circle = Instantiate(prefab, new Vector2(randomX, randomY), Quaternion.identity);
            circles[i] = circle;
            rigidbodies[i] = circle.GetComponent<Rigidbody2D>();
            SpriteRenderer circleSprite = circle.GetComponent<SpriteRenderer>();
            circleSprite.color = Color.HSVToRGB(donorH, saturationIncrement, donorV);
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

    public void ShakeRigitbodies2() {
        foreach (var rigidbody in rigidbodies) {
            float moveX = Random.Range(_minX, _maxX);
            float moveY = Random.Range(_minY, _maxY);
            rigidbody.velocity = new Vector2(moveX, moveY) * _speed * Time.fixedDeltaTime;
        }
    }

    public void ShakeDonor2() {
        float moveX = Random.Range(_minX, _maxX);
        float moveY = Random.Range(_minY, _maxY);
        rb.velocity = new Vector2(moveX, moveY) * _speed * Time.fixedDeltaTime;
    }

    public void IncreaseSaturation() {
        foreach (var circle in circles) {
            SpriteRenderer spriteRenderer = circle.GetComponent<SpriteRenderer>();
            Color currentColor = spriteRenderer.color;
            Color.RGBToHSV(currentColor, out float h, out float s, out float v);
            s = Mathf.Min(s + saturationIncrement, 1.0f);
            spriteRenderer.color = Color.HSVToRGB(h, s, v);
        }
    }

    public void StopGame() {
        Debug.Log("Level complete!");
        Time.timeScale = 0;
        Debug.Break();
    }

    private void CheckGameEnd() {
        // Получаем насыщенность цвета донора
        Color.RGBToHSV(_donorColor, out _, out float donorSaturation, out _);

        // Проверяем, что все объекты имеют насыщенность не ниже, чем у донора
        bool allObjectsSaturated = circles.All(circle => {
            SpriteRenderer spriteRenderer = circle.GetComponent<SpriteRenderer>();
            Color.RGBToHSV(spriteRenderer.color, out _, out float saturation, out _);
            return saturation >= donorSaturation;
        });

        // Если все объекты насыщены, останавливаем игру
        if (allObjectsSaturated) {
            StopGame();
        }
    }
}
   