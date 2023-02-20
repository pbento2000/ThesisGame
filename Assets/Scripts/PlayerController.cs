using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] WeaponHandler weaponHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        weaponHandler.updateWeaponRotation(new Vector2(transform.position.x, transform.position.y));
    }
}
