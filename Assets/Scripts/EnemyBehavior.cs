using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private GameObject player;
    private Vector3 usualScale;
    [SerializeField] bool isPlayerEnemy;
    [SerializeField] SpriteRenderer[] levels;
    [SerializeField] SpriteRenderer animationHolder;
    [SerializeField] Sprite hollowSquare;
    [SerializeField] Sprite fullSquare;
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
    private int health = 2;
    private int animationTime = 150;
    private Vector3 lifeBarScale = new Vector3(0.8f,0.15f,1f);

    //Variables for avoiding enemy overlap
    List<float> vectorTimes = new List<float>();
    List<Vector3> vectors = new List<Vector3>();
    Vector3 avoidVector = Vector3.zero;
    GameObject lastImpact;

    // Start is called before the first frame update
    void Start()
    {
        //maxHealth = health;
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
            transform.position = transform.position + impactVector * impactTimer * impactStrength * impactMultiplier * Time.deltaTime;
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

        if(dmg > 0f){
            levels[health].sprite = hollowSquare;
            health -= 1;
        }
        if (health < 0)
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
            //level -= 1;
            //scaleCounter -= 1;

            //transform.localScale = usualScale * (1 + scaleCounter * 0.15f);
            //health /= 1.5f;
            //maxHealth /= 1.5f;

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
            //level += 1;
            //scaleCounter += 1;

            //transform.localScale = usualScale * (1 + scaleCounter * 0.15f);
            //health *= 1.5f;
            //maxHealth *= 1.5f;

            checkSprite(true);
        }
    }

    public void setLastImpact(GameObject last)
    {
        lastImpact = last;
    }

    void checkSprite(bool isBuff)
    {
        Sprite spriteUpdate;
        if(isBuff){
            if(health == level){
                level += 1;
                health += 1;
                spriteUpdate = fullSquare;
            }else{
                level += 1;
                spriteUpdate = hollowSquare;
            }
            StartCoroutine(buffEnemy(levels[level].transform.position, spriteUpdate));
        }else{
            StartCoroutine(nerfEnemy(levels[level].transform.position, levels[level].sprite));
            levels[level].sprite = null;
            if(health == level){
                level -= 1;
                health -= 1;
                spriteUpdate = fullSquare;
            }else{
                level -= 1;
                spriteUpdate = hollowSquare;
            }
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

    IEnumerator buffEnemy(Vector3 position, Sprite spriteUpdate){
        animationHolder.transform.position = position + new Vector3(0f,0.02f*animationTime,0f);
        animationHolder.sprite = spriteUpdate;
        
        Color tmp = animationHolder.color;
        tmp.a = 0f;
        animationHolder.color = tmp;

        int counter = animationTime;

        while(counter > 0){
            counter--;
            animationHolder.transform.position -= new Vector3(0f,0.02f,0f);
            tmp.a += 1f/animationTime;
            animationHolder.color = tmp;
            yield return null;
        }

        levels[level].sprite = spriteUpdate;
        animationHolder.sprite = null;
        yield return null;
    }   

    IEnumerator nerfEnemy(Vector3 position, Sprite spriteUpdate){
        animationHolder.transform.position = position;
        animationHolder.sprite = spriteUpdate;
        int counter = animationTime;

        Color tmp = animationHolder.color;
        tmp.a = 1f;
        animationHolder.color = tmp;

        while(counter > 0){
            counter--;
            animationHolder.transform.localScale += new Vector3(0.01f,0.001875f,0f);
            tmp.a -= 1f/animationTime;
            animationHolder.color = tmp;
            yield return null;
        }

        animationHolder.sprite = null;
        animationHolder.transform.localScale = lifeBarScale;
        yield return null;
    }
    
}