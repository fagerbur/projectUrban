using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterHealth : MonoBehaviour
{
    private float fighterHealth = 3;

    public void WeaponDamage(float fighterDamage) 
    {
        fighterHealth -= fighterDamage;
        Debug.Log(fighterHealth);
        if(fighterHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
