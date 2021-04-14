using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    public float weaponForce = 500f;
    public int fireDamage = 1; 
    public int projectileTeam;

    void Start()
    {
        Destroy(gameObject, 1);
    }

    void OnCollisionEnter(Collision collision) 
    {
        Destroy(gameObject);

        ContactPoint contact = collision.contacts[0];
        FighterStatus enemyFighter = collision.transform.GetComponent<FighterStatus>();

        if (enemyFighter != null && enemyFighter.fighterTeam != projectileTeam)
        {
            enemyFighter.WeaponDamage (fireDamage);
        }

        if (collision.rigidbody != null)
        {
            collision.rigidbody.AddForce(-contact.normal * weaponForce);
        }
    }
}
