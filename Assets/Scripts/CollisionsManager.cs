using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsManager : MonoBehaviour
{

    bool hitSomeone = false;
    float hitTime = 3f;
    float hitTimer = 0f;
    [SerializeField] InterfaceManager interfaceManager;
    [SerializeField] PlayerController playerController;
    [SerializeField] BulletManager bulletManager;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hitSomeone)
        {
            hitTimer -= Time.fixedDeltaTime;
        }

        if(hitTimer <= 0f)
        {
            hitSomeone = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerEnemy" && this.gameObject.tag == "Player" && !hitSomeone)
        {
            hitSomeone = true;
            hitTimer = hitTime;
            transform.parent.gameObject.GetComponent<PlayerMovement>().getHit(col.transform.position);
            interfaceManager.addToCombo(false);
            playerController.setDeadTimer(hitTime);
            bulletManager.setDeadTimer(hitTime);
        }

        if((col.gameObject.tag == "NPCEnemy" && this.gameObject.tag == "NPC" && !hitSomeone))
        {
            hitSomeone = true;
            hitTimer = hitTime;
            transform.parent.gameObject.GetComponent<NPCShootManager>().getHit(col.transform.position);
            interfaceManager.addToCombo(false);
        }
    }
}
