using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float verticalForce = 0.05f;
    public float horizontalForce = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(Input.GetAxis("Horizontal") * horizontalForce, 0f, 0f);
        transform.Translate(0f, Input.GetAxis("Vertical") * verticalForce, 0f);
    }
}