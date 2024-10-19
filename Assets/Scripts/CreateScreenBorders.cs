using UnityEngine;

public class CreateScreenBorders : MonoBehaviour {
    private static bool bordersCreated = false;
   
    [SerializeField]private PhysicsMaterial2D bouncyMaterial;
    [SerializeField]private float  friction;
    [SerializeField]private float bounciness;
    
    // Флаг для создания границ
    private void Awake() {
        bouncyMaterial.friction = friction; 
        bouncyMaterial.bounciness = bounciness;
    }
    private void Start() {
        // Создаем границы экрана только один раз
        if (!bordersCreated) {
            CreateBorders();
            bordersCreated = true;
        }
    }

    void CreateBorders() {
        Camera mainCamera = Camera.main;
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        // Координаты для границ
        Vector2 topPosition = new Vector2(0, screenHeight);
        Vector2 bottomPosition = new Vector2(0, -screenHeight);
        Vector2 leftPosition = new Vector2(-screenWidth, 0);
        Vector2 rightPosition = new Vector2(screenWidth, 0);

        // Размеры для границ
        Vector2 horizontalSize = new Vector2(screenWidth * 2, 0.01f);
        Vector2 verticalSize = new Vector2(0.01f, screenHeight * 2);

        // Создаем и настраиваем границы
        CreateBorder("BorderTop", topPosition, horizontalSize);
        CreateBorder("BorderBottom", bottomPosition, horizontalSize);
        CreateBorder("BorderLeft", leftPosition, verticalSize);
        CreateBorder("BorderRight", rightPosition, verticalSize);
    }
    void CreateBorder(string name, Vector2 position, Vector2 size) {
        GameObject border = new GameObject(name);
        border.transform.position = position;
        var rigidbody = border.AddComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Static;
        var collider = border.AddComponent<BoxCollider2D>();
        collider.size = size;
        collider.sharedMaterial = bouncyMaterial;
    }
    
}