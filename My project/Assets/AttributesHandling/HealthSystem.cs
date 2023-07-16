using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 5f;
    
    void Start()
    {
        health = maxHealth;
    }

    
    public void DealDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Enemy took " + damageAmount.ToString() + " damage");

        if (health <= 0)
        {
            Debug.Log("Enemy Dead");
            Destroy(gameObject);
        }
    }
}
