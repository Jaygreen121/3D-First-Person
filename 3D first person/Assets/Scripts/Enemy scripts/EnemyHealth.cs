using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;

    private Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = transform.position; // TODO: this is only noticable if the enemy moves from the respawn position
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        
        if(health >= 1)
        {
            Debug.Log(health);
        }
        else
        {
            Debug.Log("Respawning");
            health = 100f;
            transform.position = respawnPosition;
        }

    }
}
