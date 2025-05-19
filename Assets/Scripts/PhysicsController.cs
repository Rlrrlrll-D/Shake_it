
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
    private SpriteRenderer[] _spriteRenderer;

    [SerializeField] private float saturationIncrement = 0.01f;
    [SerializeField] private float _minX, _maxX;
    [SerializeField] private float _minY, _maxY;
    [SerializeField] private float _speed;
    private float _checkEndCooldown = 0.1f; // Проверять не чаще, чем раз в 0.1 сек
    private float _checkEndTimer = 0f;

    public Rigidbody2D[] rigidbodies;

    private void Awake() {
        circles = new GameObject[circleCount];
        rigidbodies = new Rigidbody2D[circleCount];
        _spriteRenderer = new SpriteRenderer[circleCount];
        rb = Donor.GetComponent<Rigidbody2D>();
        
        _donorSprite = Donor.GetComponent<SpriteRenderer>();
        _donorColor = _donorSprite.color;

        float donorH, donorS, donorV;
        Color.RGBToHSV(_donorColor, out donorH, out donorS, out donorV);
        Camera mainCamera = Camera.main;
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        CircleInstantiate(donorH, donorV, screenWidth, screenHeight);
        for (int i = 0; i < circles.Length; i++) {
            _spriteRenderer[i] = circles[i].GetComponent<SpriteRenderer>();
        }

    }

    private void Update() {
        _checkEndTimer += Time.unscaledDeltaTime;
        if (_checkEndTimer >= _checkEndCooldown) {
            CheckGameEnd();
            _checkEndTimer = 0f;
        }
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

        float moveX = Random.Range(_minX, _maxX);
        float moveY = Random.Range(_minY, _maxY);
        //foreach (var rigidbody in rigidbodies) {
            
        //    rigidbody.velocity = new Vector2(moveX, moveY) * _speed ;
        //}
        for (int i = 0; i < rigidbodies.Length; i++) {
            rigidbodies[i].velocity = new Vector2(moveX, moveY) * _speed;
        }
    }

    public void ShakeDonor2() {
        float moveX = Random.Range(_minX, _maxX);
        float moveY = Random.Range(_minY, _maxY);
        rb.velocity = new Vector2(moveX, moveY) * _speed;
    }

    public void IncreaseSaturation() {
        foreach (var spriterenderer in _spriteRenderer) {
            
            Color currentColor = spriterenderer.color;
            Color.RGBToHSV(currentColor, out float h, out float s, out float v);
            s = Mathf.Min(s + saturationIncrement, 1.0f);
            spriterenderer.color = Color.HSVToRGB(h, s, v);
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
        bool allObjectsSaturated = _spriteRenderer.All(spriterenderer => {
            
            Color.RGBToHSV(spriterenderer.color, out _, out float saturation, out _);
            return saturation >= donorSaturation;
        });

        // Если все объекты насыщены, останавливаем игру
        if (allObjectsSaturated) {
            StopGame();
        }
    }
}
   