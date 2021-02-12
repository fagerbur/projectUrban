using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterWeaponFire : MonoBehaviour
{
    public int fireDamage = 1; 
    public float fireRate = .25f;
    public float fireRange = 30f; 
    public float weaponForce = 500f;
    public bool isFiring = false;
    private WaitForSeconds fireDuration = new WaitForSeconds(.07f);
    private LineRenderer laserLine;
    private GameObject laserFlash;
    private float nextFire;
    private int fighterTeam;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserFlash = transform.GetChild(0).gameObject;
        laserLine.SetPosition(0, transform.position);
        fighterTeam = transform.parent.transform.parent.GetComponent<AgentStatus>().fighterTeam;
    }

    void Update ()
    {
        //MuzzleFlash.Play();   
        Debug.DrawRay(transform.position, transform.forward * fireRange, Color.green);

        if ((Input.GetKey(KeyCode.X) || isFiring) && Time.time > nextFire)
        {
            isFiring = false;
            StartCoroutine(WeaponFire());

            RaycastHit hit;
            laserLine.SetPosition(0, transform.position);
            Vector3 rayOrigin = transform.position;

            nextFire = Time.time + fireRate;
            if (Physics.Raycast(rayOrigin, transform.forward, out hit, fireRange))
            {
                laserLine.SetPosition(1, hit.point); 
                // Debug.Log(hit.transform.name);

                AgentStatus enemyFighter = hit.collider.GetComponent<AgentStatus>();
                // FighterStatus enemyFighter = hit.collider.GetComponent<FighterStatus>();

                if (enemyFighter != null && enemyFighter.fighterTeam != fighterTeam)
                {
                    enemyFighter.WeaponDamage (fireDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * weaponForce);
                }
            }
            else
            { 
                laserLine.SetPosition(1, rayOrigin + (transform.forward * fireRange)); 
            } 
        }
    }

    private IEnumerator WeaponFire()
    {
        // weaponAudio.Play ();
        laserFlash.SetActive(true);
        laserLine.enabled = true;

        yield return fireDuration;

        laserFlash.SetActive(false);
        laserLine.enabled = false;
    }
}
