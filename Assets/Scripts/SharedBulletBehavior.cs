using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedBulletBehavior : MonoBehaviour
{

    [SerializeField] float lifespan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lifespan < 0f){
            Destroy(gameObject);
        }else{
            lifespan -= Time.fixedDeltaTime;
        }
    }
}
