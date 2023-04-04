using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private float health = 10f;
    private float maxHealth;
    private Vector3 usualScale;
    [SerializeField] bool isPlayerEnemy;
    [SerializeField] SpriteRenderer buffSprite;
    [SerializeField] SpriteRenderer nerfSprite;
    [SerializeField] Transform lifeTransform;
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
    private float scoreMultiplier = 1f;
    private float effectTimer = 15f;
    private float remainingTime = 0f;
    private bool isAffected;
    private int scaleCounter = 0;

    //Variables for avoiding enemy overlap
    List<float> vectorTimes = new List<float>();
    List<Vector3> vectors = new List<Vector3>();
    Vector3 avoidVector = Vector3.zero;
    GameObject lastImpact;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        if (isPlayerEnemy)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = GameObject.Find("NPC");
        }

        usualScale = transform.localScale;

        interfaceManager = GameObject.Find("Interface").GetComponent<InterfaceManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vectorFromPlayer = transform.position - player.transform.position;
        vectorFromPlayer = vectorFromPlayer.normalized;

        if(avoidVector != Vector3.zero)
        {
            vectorFromPlayer += avoidVector;
            avoidVector = Vector3.zero;
        }

        transform.position = transform.position - vectorFromPlayer.normalized * Time.fixedDeltaTime * movementSpeedMultiplier;

        if (impacted)
        {
            transform.position = transform.position + impactVector * impactTimer * impactStrength * impactMultiplier;
            impactTimer -= Time.deltaTime;
        }

        if (impactTimer <= 0f)
        {
            impacted = false;
            impactVector = new Vector3(0f, 0f, 0f);
            impactStrength = 0f;
        }

        

        /*
         * Code for temporary buffs/nerfs
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

    public void receiveDamage(float dmg, Vector3 impact, float strength)
    {
        health -= dmg;
        if (health <= 0f)
        {
            interfaceManager.changeScore(5f * scoreMultiplier);
            Destroy(gameObject);
        }
        else
        {
            impactTimer = maxImpactTime;
            impacted = true;
            impactVector = impact;
            impactStrength = strength;
            lifeTransform.localScale = new Vector3(1f, health/maxHealth, 1f);
        }
    }

    public void nerf()
    {
        if(scaleCounter > -3)
        {
            remainingTime = effectTimer;
            impactMultiplier /= 0.75f;
            movementSpeedMultiplier /= 1.1f;
            scoreMultiplier /= 2f;
            scaleCounter -= 1;

            transform.localScale = usualScale * (1 + scaleCounter * 0.15f);
            health /= 1.5f;
            maxHealth /= 1.5f;

            checkSprite();
        }
    }

    public void buff()
    {
        if(scaleCounter < 3)
        {
            remainingTime = effectTimer;
            impactMultiplier *= 0.75f;
            movementSpeedMultiplier *= 1.1f;
            scoreMultiplier *= 2f;
            scaleCounter += 1;

            transform.localScale = usualScale * (1 + scaleCounter * 0.15f);
            health *= 1.5f;
            maxHealth *= 1.5f;

            checkSprite();
        }
    }

    public void setLastImpact(GameObject last)
    {
        lastImpact = last;
    }

    void checkSprite()
    {
        if (transform.localScale == usualScale)
        {
            Color nerfColor = nerfSprite.color;
            nerfColor.a = 0f;
            nerfSprite.color = nerfColor;

            Color buffColor = buffSprite.color;
            buffColor.a = 0f;
            buffSprite.color = buffColor;
        }
        else if (transform.localScale.magnitude < usualScale.magnitude)
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "PlayerEnemy" && this.gameObject.tag == "PlayerEnemy") || (col.gameObject.tag == "NPCEnemy" && this.gameObject.tag == "NPCEnemy"))
        {
            if (impacted && col.gameObject != lastImpact)
            {
                EnemyBehavior enemyBehavior = col.gameObject.GetComponent<EnemyBehavior>();
                enemyBehavior.receiveDamage(0f, (-transform.position + col.transform.position).normalized, impactStrength / 1.2f);
                enemyBehavior.setLastImpact(this.gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if ((col.gameObject.tag == "PlayerEnemy" && this.gameObject.tag == "PlayerEnemy") || (col.gameObject.tag == "NPCEnemy" && this.gameObject.tag == "NPCEnemy"))
        {
            Vector3 avoidDirection = transform.position - col.transform.position;
            avoidVector -= avoidDirection.normalized;
        }
    }
}