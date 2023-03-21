using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] WeaponHandler weaponHandler;
    [SerializeField] GameObject specialEffect;
    [SerializeField] GameObject secondaryAttack;
    bool effectActive = false;
    GameObject effectObject;
    float effectCooldown = 10f;
    float effectTimer = 0f;

    float secondaryAttackCooldown = 1f;
    float secondaryAttackTimer = 0f;
    GameObject secondaryAttackObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        weaponHandler.updateWeaponRotation(new Vector2(transform.position.x, transform.position.y));

        if(!effectActive){
            if(Input.GetButton("BuffNPCEnemy")){
                effectObject = Instantiate(specialEffect, transform.position, Quaternion.identity);
                effectObject.GetComponent<PulseBehavior>().setEffect(0);
                effectActive = true;
                effectTimer = effectCooldown;
            }
            if(Input.GetButton("NerfNPCEnemy")){
                effectObject = Instantiate(specialEffect, transform.position, Quaternion.identity);
                effectObject.GetComponent<PulseBehavior>().setEffect(1);
                effectActive = true;
                effectTimer = effectCooldown;
            }
            if(Input.GetButton("BuffEnemy")){
                effectObject = Instantiate(specialEffect, transform.position, Quaternion.identity);
                effectObject.GetComponent<PulseBehavior>().setEffect(2);
                effectActive = true;
                effectTimer = effectCooldown;
            }
            if(Input.GetButton("NerfEnemy")){
                effectObject = Instantiate(specialEffect, transform.position, Quaternion.identity);
                effectObject.GetComponent<PulseBehavior>().setEffect(3);
                effectActive = true;
                effectTimer = effectCooldown;
            }
        }

        if(secondaryAttackTimer <= 0f && Input.GetButton("SecondaryAttack")){
            secondaryAttackTimer = secondaryAttackCooldown;
            secondaryAttackObject = Instantiate(secondaryAttack, transform.position, Quaternion.identity);
            secondaryAttackObject.GetComponent<SecondaryAttackBehavior>().setEnemy("PlayerEnemy");
        }

        if(secondaryAttackTimer > 0f)
        {
            secondaryAttackTimer -= Time.fixedDeltaTime;
            if(secondaryAttackTimer < 0f)
            {
                secondaryAttackTimer = 0f;
            }
        }

        if(effectTimer > 0f){
            effectTimer -= Time.fixedDeltaTime;
            if(effectTimer < 0f){
                effectActive = false;
            }
        }
    }
}