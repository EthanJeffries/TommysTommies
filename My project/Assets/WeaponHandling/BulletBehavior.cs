using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float bulletDamage;

    public void setBulletDamage(float bDamage)
    {
        this.bulletDamage = bDamage;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthSystem>().DealDamage(bulletDamage);
        }

        Destroy(gameObject);

    }
}
