
using UnityEngine;
//[RequireComponent(typeof(PhysicsController))]
public class Circle : MonoBehaviour
{

    private GameObject _donorObject;
    private Color _donorColor;
    private PhysicsController _physicsController;
    private bool gameEnded;

    public float saturationIncrement = 0.01f;
    private void Start()
    {

        _physicsController = FindObjectOfType<PhysicsController>();
        _donorObject = GameObject.FindGameObjectWithTag("Donor");
        _donorColor = _donorObject.GetComponent<SpriteRenderer>().color; 

        if (_donorObject == null)
        {
            Debug.LogError("Donor object not found in the scene!");
        }
        if (_physicsController.circles != null)
        {
            Debug.Log("Circles object has founded in the scene!");
            
        }
    }

    public void Update()
    {
        if (gameEnded)
            return;
        CheckGameEnd();
        
    }

    void CheckGameEnd()
    {
        
        if (gameEnded || _physicsController.circles == null || _donorObject == null)
        {
            Debug.LogError("SaturationChecker or Donor object is not set!");
            return;
        }

        float donorSaturation;
        
        Color.RGBToHSV(_donorColor, out _, out donorSaturation, out _);

        bool allObjectsSaturated = true;

        
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

        if (allObjectsSaturated) {
            StopGame();
        }
    }

    private void StopGame() {
        gameEnded = true;
        Debug.Log("Level complete!");
        Time.timeScale = 0;
        Debug.Break();
    }
}


