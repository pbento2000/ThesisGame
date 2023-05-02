using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBehavior : MonoBehaviour
{

    enum effects {buffNPCEnemy, nerfNPCEnemy, buffPlayerEnemy, nerfPlayerEnemy};
    [SerializeField] effects effect;
    float pulseRadius = 0f;
    SpriteRenderer pulseSprite;
    [SerializeField] Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        pulseSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(pulseRadius < 25f){
            pulseRadius += Time.fixedDeltaTime * 10f;
            transform.localScale = new Vector3(pulseRadius, pulseRadius, 0);
            Color pulseColor = pulseSprite.color;
            pulseColor.a -= Time.fixedDeltaTime * 10f /25f;
            pulseSprite.color = pulseColor;
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
        if(v < 2)
        this.gameObject.GetComponent<SpriteRenderer>().color = colors[1];
        else
        this.gameObject.GetComponent<SpriteRenderer>().color = colors[0];
    }
}
