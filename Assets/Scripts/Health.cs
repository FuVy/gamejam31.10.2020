using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;

    public void DealDamage(int damage, bool fromRight)
    {
        health -= damage;
        if (health <= 0)
        {
            if (GetComponent<Civilian>())
            {
                transform.rotation = fromRight == true ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
                GetComponent<Animator>().SetBool("isDown", true);
                gameObject.layer = LayerMask.NameToLayer("Down");
            }
            else
                GetComponent<Animator>().SetTrigger("Die");
        }
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GetComponent<Animator>().SetTrigger("Die");
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
