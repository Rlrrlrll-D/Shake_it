using UnityEngine;



public class AccelerometerMovement : MonoBehaviour {
    public float speed = 10.0f; // �������� ��������

    private float screenWidth;
    private float screenHeight;
    private float objWidth;
    private float objHeight;
    private RectTransform rectTransform;

    void Start() {
        // ���������� ������� ������
        screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        screenHeight = Camera.main.orthographicSize;
        RectTransform rectTransform = GetComponent<RectTransform>();
        objWidth = rectTransform.rect.width;
        objHeight = rectTransform.rect.height;
    }

    void FixedUpdate() {
        Vector3 acceleration = Input.acceleration; // �������� ������ �������������
        Vector2 movement = new Vector2(acceleration.x, acceleration.y) * speed * Time.deltaTime; // ��������� �������� � 2D

        Vector3 newPosition = transform.position + (Vector3)movement;
        var offWidth = objWidth / 2;
        var offHeight = objHeight / 2;
        Debug.Log(objWidth + "  " + objHeight);

        // ������������ ������� ��������� ������
        newPosition.x = Mathf.Clamp(newPosition.x, -screenWidth + offWidth, screenWidth - offWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, -screenHeight + offHeight, screenHeight - offHeight);

        transform.position = newPosition; // ��������� �������� � �������

    }
}


