using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterWeaponFire : MonoBehaviour
{
    public float fireRate = .25f;
    public float fireRange = 30f; 
    public bool isFiring = false;
    public GameObject projectile;

    private WaitForSeconds fireDuration = new WaitForSeconds(.07f);
    private float nextFire;
    private int fighterTeam;

    void Start()
    {
        fighterTeam = transform.parent.transform.parent.GetComponent<FighterStatus>().fighterTeam;
    }

    void Update ()
    {
        Debug.DrawRay(transform.position, transform.forward * fireRange, Color.green);

        if ((Input.GetKey(KeyCode.Space) || isFiring) && Time.time > nextFire)
        {
            GameObject projectileFired = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
            projectileFired.GetComponent<ProjectileFire>().projectileTeam = fighterTeam;
            projectileFired.transform.eulerAngles = transform.eulerAngles;
            Vector3 forceVector = projectileFired.transform.forward;
 
            projectileFired.GetComponent<Rigidbody>().AddForce(forceVector * 77 * 1.5f, ForceMode.Impulse);
            
            isFiring = false;
            StartCoroutine(WeaponFire());

            nextFire = Time.time + fireRate; 
        }
    }

    private IEnumerator WeaponFire()
    {
        // weaponAudio.Play ();
        yield return fireDuration;
    }
}
