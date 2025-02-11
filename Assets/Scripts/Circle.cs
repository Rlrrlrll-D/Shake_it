
using UnityEngine;
//[RequireComponent(typeof(PhysicsController))]
public class Circle : MonoBehaviour
{
    private GameObject _donorObject;
    private Color _donorColor;
    private PhysicsController _physicsController;
    //

    private void Start()
    {

        _physicsController = FindObjectOfType<PhysicsController>();
        _donorObject = GameObject.FindGameObjectWithTag("Donor");
        _donorColor = _donorObject.GetComponent<SpriteRenderer>().color; 

           
    }

    void Update()
    {
       
        CheckGameEnd();
        
    }

    void CheckGameEnd()
    {
        

        float donorSaturation;
        
        Color.RGBToHSV(_donorColor, out _, out donorSaturation, out _);

        bool allObjectsSaturated = true;

        
        foreach (GameObject obj in _physicsController.circles)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

            float h, s, v;
            Color.RGBToHSV(sr.color, out h, out s, out v);
           if (s < donorSaturation)
           {
              allObjectsSaturated = false;
              break;
           }
                
            
        }

        if (allObjectsSaturated) {
            _physicsController.StopGame();
        }
    }

    
}


