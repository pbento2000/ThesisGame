using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryAttackBehavior : MonoBehaviour
{

    float attackScale = 0f;
    float damage = 3f;
    [SerializeField] SpriteRenderer sprite;
    float alphaColor = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackScale < 0.15f)
        {
            attackScale += Time.fixedDeltaTime;
            alphaColor -= Time.fixedDeltaTime;
            transform.localScale = new Vector3(attackScale, attackScale, 0f);
            //METER O ALPHA A MUDAR
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            other.gameObject.GetComponent<EnemyBehavior>().receiveDamage(damage, other.gameObject.transform.position - transform.position, 0.2f);
        }
    }
}