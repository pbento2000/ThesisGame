using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShootManager : MonoBehaviour
{

    //Variables for Shooting
    [SerializeField] GameObject bullet;
    [SerializeField] Transform pistol;
    [SerializeField] Collider2D[] enemies;
    [SerializeField] float radius;
    [SerializeField] LayerMask mask;
    [SerializeField] Transform player;
    float delay = 0.7f;
    float delayTimer = 0f;
    bool onCooldown = false;
    bool foundEnemy;

    //Variables for Movement

    // Start is called before the first frame update
    void Start()
    {
        pistol = GameObject.Find("NPCWeapon").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        enemies = Physics2D.OverlapCircleAll(transform.position, radius, mask);
        Vector2 npcPosition = transform.position;

        //Code for Shooting

        if (!onCooldown)
        {
            //Find closest enemy
            foundEnemy = false;
            float minDistance = 10f;
            Collider2D minEnemy = null;
            
            for(int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].tag == "NPCEnemy")
                {
                    if (Vector3.Distance(transform.position, enemies[i].gameObject.transform.position) < minDistance)
                    {
                        minDistance = Vector3.Distance(transform.position, enemies[i].gameObject.transform.position);
                        minEnemy = enemies[i];
                        foundEnemy = true;
                    }
                }
            }

            if (foundEnemy)
            {
                Vector2 enemyPosition = minEnemy.transform.position;
                Quaternion bulletRotation = Quaternion.identity;

                //Find rotation for bullet
                float angle = Vector2.Angle(enemyPosition - npcPosition, new Vector2(1f, 0f));

                if (transform.position.y <= enemyPosition.y)
                {
                    bulletRotation = Quaternion.Euler(0f, 0f, angle);
                    pistol.rotation = Quaternion.Euler(0f, 0f, angle);
                }
                else
                {
                    bulletRotation = Quaternion.Euler(0f, 0f, 360f - angle);
                    pistol.rotation = Quaternion.Euler(0f, 0f, 360f - angle);
                }

                Instantiate(bullet, pistol.position, bulletRotation);
                onCooldown = true;
                delayTimer = delay;
            }
        }

        if (delayTimer > 0.0f)
        {
            delayTimer -= Time.fixedDeltaTime;
        }

        if (delayTimer <= 0.0f)
        {
            onCooldown = false;
        }

        //Code for Movement

        Vector3 movementVector = new Vector2(0, 0);
        Vector3 playerPosition = player.position;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].tag == "NPCEnemy")
            {
                
                Vector2 enemyPosition = enemies[i].gameObject.transform.position;
                Vector3 npcEnemyVector = (npcPosition - enemyPosition) / Mathf.Pow(Vector3.Distance(transform.position, enemyPosition), 6f);

                //movementVector += finalVector;

                movementVector += npcEnemyVector;

                /*
                Vector3 enemyPosition = enemies[i].gameObject.transform.position;
                Vector3 playerNpcVector = (playerPosition - transform.position);
                Vector3 npcEnemyVector = (transform.position - enemyPosition);

                float enemyThreat = Mathf.Pow(10/npcEnemyVector.magnitude,2f);
                float playerImportance = playerNpcVector.magnitude;

                Debug.Log(playerImportance / enemyThreat);
                */
            }
        }

        Vector3 playerNpcVector = (playerPosition - transform.position) / (1 / Mathf.Pow(Vector3.Distance(transform.position, playerPosition), 2f));
        
        float enemyThreat = movementVector.magnitude;
        float playerImportance = playerNpcVector.magnitude;

        if (movementVector.magnitude == 0f && playerNpcVector.magnitude > 3f)
        {
            movementVector = ((playerPosition - transform.position) /(1/Mathf.Pow(Vector3.Distance(transform.position, playerPosition), 6f))).normalized;
        }
        else if(movementVector.magnitude == 0f && playerNpcVector.magnitude <= 3f)
        {
            movementVector = Vector3.zero;
        }
        else
        {
            movementVector += Vector3.Slerp(movementVector, playerNpcVector, playerImportance / enemyThreat).normalized;
        }

        Vector3 movement = movementVector.normalized;

        transform.position += movement * Time.deltaTime;
    }
}