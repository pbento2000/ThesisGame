using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void updateWeaponRotation(Vector2 position){
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        position = new Vector2(transform.position.x, transform.position.y);
        
        float angle = Vector2.Angle(mousePosition - position, new Vector2(1f, 0f));
        
        if (transform.position.y <= mousePosition.y){
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }else{
            transform.rotation = Quaternion.Euler(0f, 0f, 360f - angle);
        }
    }
}
