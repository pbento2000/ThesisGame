using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBehavior : MonoBehaviour
{

    enum effects {buffNPCEnemy, nerfNPCEnemy, buffPlayerEnemy, nerfPlayerEnemy};
    [SerializeField] effects effect;

    float maskRadius = 0.95f;
    float pulseRadius = 0f;

    Transform mask;

    // Start is called before the first frame update
    void Start()
    {
        mask = transform.GetChild(0).gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(maskRadius < 1.01f){
            maskRadius += Time.fixedDeltaTime * 0.02f;
            mask.localScale = new Vector3(maskRadius, maskRadius, 0);
            pulseRadius += Time.fixedDeltaTime * 10f;
            transform.localScale = new Vector3(pulseRadius, pulseRadius, 0);
        }else{
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        switch(effect){
            case effects.buffNPCEnemy:
                if(other.tag == "NPCEnemy"){
                    other.gameObject.GetComponent<EnemyBehavior>().buff();
                }
                break;
            case effects.nerfNPCEnemy:
                if(other.tag == "NPCEnemy"){
                    other.gameObject.GetComponent<EnemyBehavior>().nerf();
                }
                break;
            case effects.buffPlayerEnemy:
                if(other.tag == "PlayerEnemy"){
                    other.gameObject.GetComponent<EnemyBehavior>().buff();
                }
                break;
            case effects.nerfPlayerEnemy:
            if(other.tag == "PlayerEnemy"){
                    other.gameObject.GetComponent<EnemyBehavior>().nerf();
                }
                break;
        }
    }

    internal void setEffect(int v)
    {
        effect = (effects) v;
    }
}
