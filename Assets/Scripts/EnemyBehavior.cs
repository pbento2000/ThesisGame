using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private float health = 10f;
    private Vector3 usualScale;
    [SerializeField] bool isPlayerEnemy;
    [SerializeField] SpriteRenderer buffSprite;
    [SerializeField] SpriteRenderer nerfSprite;
    [SerializeField] InterfaceManager interfaceManager;

    //Variables for when enemy is hit
    private float impactTimer = 0f;
    private float maxImpactTime = 0.2f;
    private bool impacted = false;
    private float impactStrength = 0f;
    Vector3 impactVector;
    
    //Multipliers affected by pulse effect and effect related variables
    private float impactMultiplier = 1f;
    private float movementSpeedMultiplier = 1f;
    private float sizeMultiplier = 1f;
    private float healthMultiplier = 1f;
    private float scoreMultiplier = 1f;
    private float effectTimer = 15f;
    private float remainingTime = 0f;
    private bool isAffected;

    // Start is called before the first frame update
    void Start()
    {
        if(isPlayerEnemy){
            player = GameObject.Find("Player");
        }else{
            player = GameObject.Find("NPC");
        }
        
        usualScale = transform.localScale;

        interfaceManager = GameObject.Find("Interface").GetComponent<InterfaceManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vectorFromPlayer = transform.position - player.transform.position;

        transform.position = transform.position - vectorFromPlayer.normalized * Time.fixedDeltaTime * movementSpeedMultiplier;

        if(impacted){
            transform.position = transform.position + impactVector * impactTimer * impactStrength * impactMultiplier;
            impactTimer -= Time.deltaTime;
        }

        if(impactTimer <= 0f){
            impacted = false;
            impactVector = new Vector3(0f,0f,0f);
            impactStrength = 0f;
        }

        /*
        if(isAffected){
            remainingTime -= Time.fixedDeltaTime;
            if(remainingTime < 0f){
                transform.localScale = usualScale;
                health /= healthMultiplier;
                isAffected = false;

                sizeMultiplier = 1f;
                impactMultiplier = 1f;
                movementSpeedMultiplier = 1f;
                healthMultiplier = 1f;
                scoreMultiplier = 1f;

                Color nerfColor = nerfSprite.color;
                nerfColor.a = 0f;
                nerfSprite.color = nerfColor;
                Color buffColor = buffSprite.color;
                buffColor.a = 0f;
                buffSprite.color = buffColor;
            }
        }*/
    }

    public void receiveDamage(float dmg, Vector3 impact, float strength){
        health -= dmg;
        if(health <= 0f){
            interfaceManager.changeScore(5f * scoreMultiplier);
            Destroy(gameObject);
        }else{
            impactTimer = maxImpactTime;
            impacted = true;
            impactVector = impact;
            impactStrength = strength;
        }
    }

    public void nerf()
    {
        remainingTime = effectTimer;
        sizeMultiplier /= 1.1f;
        impactMultiplier /= 0.9f;
        movementSpeedMultiplier /= 1.1f;
        healthMultiplier /= 1.1f;
        scoreMultiplier /= 1.1f;

        transform.localScale = usualScale * sizeMultiplier;
        health *= healthMultiplier;

        checkSprite();
    }

    internal void buff()
    {
        remainingTime = effectTimer;
        sizeMultiplier *= 1.1f;
        impactMultiplier *= 0.9f;
        movementSpeedMultiplier *= 1.1f;
        healthMultiplier *= 1.1f;
        scoreMultiplier *= 1.1f;

        transform.localScale = usualScale * sizeMultiplier;
        health *= healthMultiplier;

        checkSprite();
    }

    void checkSprite()
    {
        if(sizeMultiplier == 1f)
        {
            Color nerfColor = nerfSprite.color;
            nerfColor.a = 0f;
            nerfSprite.color = nerfColor;

            Color buffColor = buffSprite.color;
            buffColor.a = 0f;
            buffSprite.color = buffColor;
        }else if(sizeMultiplier < 1f)
        {
            Color nerfColor = nerfSprite.color;
            nerfColor.a = 1f;
            nerfSprite.color = nerfColor;

            Color buffColor = buffSprite.color;
            buffColor.a = 0f;
            buffSprite.color = buffColor;
        }
        else
        {
            Color buffColor = buffSprite.color;
            buffColor.a = 1f;
            buffSprite.color = buffColor;

            Color nerfColor = nerfSprite.color;
            nerfColor.a = 0f;
            nerfSprite.color = nerfColor;
        }
    }
}