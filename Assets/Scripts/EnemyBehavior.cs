using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private GameObject player;
    private float health = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vectorFromPlayer = transform.position - player.transform.position;

        transform.position = transform.position - vectorFromPlayer.normalized*Time.deltaTime;
    }

    
}
