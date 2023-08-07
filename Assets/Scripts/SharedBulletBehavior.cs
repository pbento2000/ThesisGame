using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedBulletBehavior : MonoBehaviour
{

    [SerializeField] float lifespan;
    [SerializeField] bool isPlayer;
    InterfaceManager interfaceManager;

    // Start is called before the first frame update
    void Start()
    {
        interfaceManager = GameObject.Find("Interface").GetComponent<InterfaceManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lifespan < 0f){
            if(isPlayer){
                interfaceManager.saveMiss();
            }
            Destroy(gameObject);
        }else{
            lifespan -= Time.fixedDeltaTime;
        }
    }
}
