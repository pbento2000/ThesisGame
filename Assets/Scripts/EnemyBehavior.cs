using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private float health = 14f;
    private float maxHealth;
    private Vector3 usualScale;
    [SerializeField] bool isPlayerEnemy;
    [SerializeField] SpriteRenderer lvlOne;
    [SerializeField] SpriteRenderer lvlTwo;
    [SerializeField] SpriteRenderer animationHolder;
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
    private int level = 2;
    [SerializeField] Sprite[] effectSprites;
    private int animationTime = 150;

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
        if(level > 0)
        {
            remainingTime = effectTimer;
            impactMultiplier /= 0.75f;
            movementSpeedMultiplier /= 1.1f;
            scoreMultiplier /= 2f;
            level -= 1;
            //scaleCounter -= 1;

            //transform.localScale = usualScale * (1 + scaleCounter * 0.15f);
            health /= 1.5f;
            maxHealth /= 1.5f;

            checkSprite(false);
        }

    }

    public void buff()
    {
        if(level < 4)
        {
            remainingTime = effectTimer;
            impactMultiplier *= 0.75f;
            movementSpeedMultiplier *= 1.1f;
            scoreMultiplier *= 2f;
            level += 1;
            //scaleCounter += 1;

            //transform.localScale = usualScale * (1 + scaleCounter * 0.15f);
            health *= 1.5f;
            maxHealth *= 1.5f;

            checkSprite(true);
        }
    }

    public void setLastImpact(GameObject last)
    {
        lastImpact = last;
    }

    void checkSprite(bool isBuff)
    {
        if(isBuff){
            if(level < 3)
                StartCoroutine(buffEnemy(lvlOne.transform.localScale, effectSprites[level-1]));
            else
                StartCoroutine(buffEnemy(lvlOne.transform.localScale, effectSprites[level-1]));
        }else{
            switch(level){
                case 0:
                    lvlOne.sprite = null;
                    break;
                case 1:
                    lvlOne.sprite = effectSprites[level-1];
                    break;
                case 2:
                    lvlOne.sprite = effectSprites[level-1];
                    lvlTwo.sprite = null;
                    break;
                case 3:
                    lvlTwo.sprite = effectSprites[level-1];
                    break;
                case 4:
                    lvlTwo.sprite = effectSprites[level-1];
                    break;
            }
            if(level < 3)
                StartCoroutine(nerfEnemy(lvlTwo.transform.localScale, effectSprites[level]));
            else
                StartCoroutine(nerfEnemy(lvlTwo.transform.localScale, effectSprites[level]));
        }
        /*
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
        */
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

    IEnumerator buffEnemy(Vector3 scaleUpdate, Sprite spriteUpdate){
        animationHolder.transform.localScale = scaleUpdate + new Vector3(0.01f*animationTime,0.01f*animationTime,0.01f*animationTime);
        animationHolder.sprite = spriteUpdate;
        
        Color tmp = animationHolder.color;
        tmp.a = 0f;
        animationHolder.color = tmp;

        int counter = animationTime;

        while(counter > 0){
            counter--;
            animationHolder.transform.localScale -= new Vector3(0.01f,0.01f,0.01f);
            tmp.a += 1f/animationTime;
            animationHolder.color = tmp;
            yield return null;
        }

        switch(level){
            case 0:
                lvlOne.sprite = null;
                break;
            case 1:
                lvlOne.sprite = effectSprites[level-1];
                break;
            case 2:
                lvlOne.sprite = effectSprites[level-1];
                lvlTwo.sprite = null;
                break;
            case 3:
                lvlTwo.sprite = effectSprites[level-1];
                break;
            case 4:
                lvlTwo.sprite = effectSprites[level-1];
                break;
        }
        yield return null;
    }

    IEnumerator nerfEnemy(Vector3 scaleUpdate, Sprite spriteUpdate){
        animationHolder.transform.localScale = scaleUpdate;
        animationHolder.sprite = spriteUpdate;
        int counter = animationTime;

        Color tmp = animationHolder.color;
        tmp.a = 1f;
        animationHolder.color = tmp;

        while(counter > 0){
            counter--;
            animationHolder.transform.localScale += new Vector3(0.01f,0.01f,0.01f);
            tmp.a -= 1f/animationTime;
            animationHolder.color = tmp;
            yield return null;
        }
        yield return null;
    }
}