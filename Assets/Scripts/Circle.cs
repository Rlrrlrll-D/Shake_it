
using UnityEngine;
//[RequireComponent(typeof(PhysicsController))]
public class Circle : MonoBehaviour
{

    private GameObject _donorObject;
    private Color _donorColor;
    private PhysicsController _physicsController;
    private SpriteRenderer _spriteRenderer;
    private Color _currentColor;

    //private GameObject prefab;
    // private GameObject[] circles;


    private bool isCollided;
    private bool likeDonorSaturation;
    private bool gameEnded;

    public float saturationIncrement = 0.01f;
    private void Start()
    {

        _physicsController = FindObjectOfType<PhysicsController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _donorObject = GameObject.FindGameObjectWithTag("Donor");
        _donorColor = _donorObject.GetComponent<SpriteRenderer>().color;

        //_currentColor = _spriteRenderer.color;
        //physicsController = GetComponent<PhysicsController>();

        //physicsController.enabled = true;
        // physicsController.prefab = prefab;


        //circles = GameObject.FindGameObjectsWithTag("Circle");
        if (_donorObject == null)
        {
            Debug.LogError("Donor object not found in the scene!");
        }
        if (_physicsController.circles != null)
        {
            Debug.Log("Circles object has founded in the scene!");
            Debug.Log(_physicsController.circles.Length);
        }
    }

    public void Update()
    {
        if (gameEnded)
            return;
        CheckGameEnd();
        /*   if (gameEnded) {
               Debug.Log("Level complete!");

               // Останавливаем игру
               Time.timeScale = 0;
               Debug.Break();
           }*/
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_donorObject == null)
        {
            Debug.LogError("Donor object is not set!");
            return;
        }

        if (collision.gameObject == _donorObject)
        {
            Debug.Log("Collision with Donor object detected.");

            //Color donorColor = _donorObject.GetComponent<SpriteRenderer>().color;
            //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Color _currentColor = _spriteRenderer.color;
            float h, s, v;
            Color.RGBToHSV(_currentColor, out h, out s, out v);
            float donorH, donorS, donorV;
            Color.RGBToHSV(_donorColor, out donorH, out donorS, out donorV);

            if (s == 0)
            {

                _spriteRenderer.color = Color.HSVToRGB(donorH, saturationIncrement, donorV);
                isCollided = true;
                Debug.Log("First collision: Saturation set to donor's.");
            }
            else if (s > donorS)
            {
                _spriteRenderer.color = Color.HSVToRGB(donorH, donorS, donorV);
                isCollided = false;
                likeDonorSaturation = true;
                Debug.Log("Saturation higher than donor's: Reset to donor's saturation.");
                Debug.Log("Bingo");

            }
            else if (!likeDonorSaturation && isCollided)
            {
                IncreaseSaturation();
            }
        }
    }
    /*void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Border")) {
            // Сохраняем позицию внутри границ, но только при значительном смещении
            Vector3 position = transform.position;
            bool outOfBounds = false;

            if (position.x < -Camera.main.orthographicSize * Camera.main.aspect + 0.5f) {
                position.x = -Camera.main.orthographicSize * Camera.main.aspect + 0.5f;
                outOfBounds = true;
            }
            if (position.x > Camera.main.orthographicSize * Camera.main.aspect - 0.5f) {
                position.x = Camera.main.orthographicSize * Camera.main.aspect - 0.5f;
                outOfBounds = true;
            }
            if (position.y < -Camera.main.orthographicSize + 0.5f) {
                position.y = -Camera.main.orthographicSize + 0.5f;
                outOfBounds = true;
            }
            if (position.y > Camera.main.orthographicSize - 0.5f) {
                position.y = Camera.main.orthographicSize - 0.5f;
                outOfBounds = true;
            }

            if (outOfBounds) {
                transform.position = position;
            }
        }
    }*/


    public void IncreaseSaturation()
    {

        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color _currentColor = _spriteRenderer.color;
        float h, s, v;
        Color.RGBToHSV(_currentColor, out h, out s, out v);
        s = Mathf.Min(s + saturationIncrement, 1.0f);
        _spriteRenderer.color = Color.HSVToRGB(h, s, v);
        Debug.Log("Saturation increased to " + s);

    }
    void CheckGameEnd()
    {
        //if(gameEnded) return;
        if (gameEnded || _physicsController.circles == null || _donorObject == null)
        {
            Debug.LogError("SaturationChecker or Donor object is not set!");
            return;
        }

        float donorSaturation;
        //Color donorColor = _donorObject.GetComponent<SpriteRenderer>().color;
        Color.RGBToHSV(_donorColor, out _, out donorSaturation, out _);

        bool allObjectsSaturated = true;

        // Проверяем только объекты с тегом "target"
        foreach (GameObject obj in _physicsController.circles)
        {

            if (obj != null)
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    float h, s, v;
                    Color.RGBToHSV(sr.color, out h, out s, out v);
                    Debug.Log("Object saturation: " + s + ", Donor saturation: " + donorSaturation);
                    if (s < donorSaturation)
                    {
                        allObjectsSaturated = false;
                        break;
                    }
                }
            }
        }

        if (allObjectsSaturated)
        { // Если все объекты достигли насыщенности донорского объекта, завершаем игру
            gameEnded = true;
            Debug.Log("Level complete!");

            // Останавливаем игру
            Time.timeScale = 0;
            Debug.Break();
        }
    }
}


