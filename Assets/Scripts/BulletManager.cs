using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    
    [SerializeField] GameObject bullet;
    [SerializeField] Transform pistol;
    float delay = 0.5f;
    float delayTimer = 0f;
    bool onCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        pistol = GameObject.Find("Weapon").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButton("Fire") && !onCooldown){
            onCooldown = true;
            Instantiate(bullet, pistol.position, pistol.rotation);
            delayTimer = delay;
        }

        if(delayTimer > 0.0f){
            delayTimer -= Time.fixedDeltaTime;
        }

        if(delayTimer <= 0.0f){
            onCooldown = false;
        }
    }
}