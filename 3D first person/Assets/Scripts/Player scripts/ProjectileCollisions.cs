using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisions : MonoBehaviour
{
    private float damage = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null) // If the thing you hit is an enemy (i.e. has an EnemyHealth script)
        {
            enemyHealth.takeDamage(damage);
            // TODO destroy the projectile
        }

    }
}
