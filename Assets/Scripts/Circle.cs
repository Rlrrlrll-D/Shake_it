using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(PhysicsController))]
public class Circle : MonoBehaviour {

    private GameObject donorObject;
    private PhysicsController physicsController;
    //private GameObject prefab;
   // private GameObject[] circles;


    private bool isCollided;
    private bool likeDonorSaturation;
    private bool gameEnded;

    public float saturationIncrement = 0.01f;
    private void Start() {

        physicsController = FindObjectOfType<PhysicsController>();
        //physicsController = GetComponent<PhysicsController>();
       
        //physicsController.enabled = true;
       // physicsController.prefab = prefab;
        
        donorObject = GameObject.FindGameObjectWithTag("Donor");
        //circles = GameObject.FindGameObjectsWithTag("Circle");
        if (donorObject == null) {
            Debug.LogError("Donor object not found in the scene!");
        }
        if (physicsController.circles != null) {
            Debug.Log("Circles object has founded in the scene!");
            Debug.Log(physicsController.circles.Length);
        }
    }

    public void Update() {
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

    void OnCollisionEnter2D(Collision2D collision) {
        if (donorObject == null) {
            Debug.LogError("Donor object is not set!");
            return;
        }

        if (collision.gameObject == donorObject) {
            Debug.Log("Collision with Donor object detected.");

            Color donorColor = donorObject.GetComponent<SpriteRenderer>().color;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Color currentColor = spriteRenderer.color;
            float h, s, v;
            Color.RGBToHSV(currentColor, out h, out s, out v);
            float donorH, donorS, donorV;
            Color.RGBToHSV(donorColor, out donorH, out donorS, out donorV);

            if (s == 0) {

                spriteRenderer.color = Color.HSVToRGB(donorH, saturationIncrement, donorV);
                isCollided = true;
                Debug.Log("First collision: Saturation set to donor's.");
            } else if (s > donorS) {
                spriteRenderer.color = Color.HSVToRGB(donorH, donorS, donorV);
                isCollided = false;
                likeDonorSaturation = true;
                Debug.Log("Saturation higher than donor's: Reset to donor's saturation.");
                Debug.Log("Bingo");

            } else if (!likeDonorSaturation && isCollided) {
                IncreaseSaturation();
            }
        }
    }

    public void IncreaseSaturation() {

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color currentColor = spriteRenderer.color;
        float h, s, v;
        Color.RGBToHSV(currentColor, out h, out s, out v);
        s = Mathf.Min(s + saturationIncrement, 1.0f);
        spriteRenderer.color = Color.HSVToRGB(h, s, v);
        Debug.Log("Saturation increased to " + s);


    }
    void CheckGameEnd() {
        //if(gameEnded) return;
        if (gameEnded || physicsController.circles == null || donorObject == null) {
            Debug.LogError("SaturationChecker or Donor object is not set!");
            return;
        }

        float donorSaturation;
        Color donorColor = donorObject.GetComponent<SpriteRenderer>().color;
        Color.RGBToHSV(donorColor, out _, out donorSaturation, out _);

        bool allObjectsSaturated = true;

        // Проверяем только объекты с тегом "target"
        foreach (GameObject obj in physicsController.circles) {

            if (obj != null) {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null) {
                    float h, s, v;
                    Color.RGBToHSV(sr.color, out h, out s, out v);
                    Debug.Log("Object saturation: " + s + ", Donor saturation: " + donorSaturation);
                    if (s < donorSaturation) {
                        allObjectsSaturated = false;
                        break;
                    }
                }
            }
        }

        if (allObjectsSaturated) { // Если все объекты достигли насыщенности донорского объекта, завершаем игру
            gameEnded = true;
            Debug.Log("Level complete!");

            // Останавливаем игру
            Time.timeScale = 0;
            Debug.Break();
        }
    }
}


