using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private float health = 1000f;

    //Variables for when enemy is hit
    private float impactTimer = 0f;
    private float maxImpactTime = 0.2f;
    private bool impacted = false;
    private float impactStrength = 0f;
    Vector3 impactVector;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vectorFromPlayer = transform.position - player.transform.position;

        transform.position = transform.position - vectorFromPlayer.normalized*Time.fixedDeltaTime;

        if(impacted){
            transform.position = transform.position + impactVector * impactTimer * impactStrength;
            impactTimer -= Time.deltaTime;
        }

        if(impactTimer <= 0f){
            impacted = false;
            impactVector = new Vector3(0f,0f,0f);
            impactStrength = 0f;
        }
    }

    public void receiveDamage(float dmg, Vector3 impact, float strength){
        health -= dmg;
        if(health <= 0f){
            Destroy(gameObject);
        }else{
            impactTimer = maxImpactTime;
            impacted = true;
            impactVector = impact;
            impactStrength = strength;
        }
    }

}