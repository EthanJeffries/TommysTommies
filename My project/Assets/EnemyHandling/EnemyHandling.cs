using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHandling : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsPlayer;

    protected bool CheckWithinRange(float range)
    {
        return Physics2D.OverlapCircle(transform.position, range, whatIsPlayer);
    }
}
