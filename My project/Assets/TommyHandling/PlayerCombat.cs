using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Class that handles player combat which is simply a range gun mechanic (may implement melee combat?)
//Created by Ethan Jeffries and last edited on 7/10/23
public class PlayerCombat : MonoBehaviour
{
    private UserInput input = null;

    //Objects used to locate where to spawn bullet
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject rightHandGun;
    [SerializeField] private GameObject leftHandGun;
    [SerializeField] private Transform leftBulletFirePoint;
    [SerializeField] private Transform rightBulletFirePoint;
    
    //Boolean checks used to know when to fire/stow/dualwield weapon
    [SerializeField] private bool isShooting;
    [SerializeField] private bool alreadyShooting;
    [SerializeField] public bool weaponReady;
    [SerializeField] public bool dualWield;

    //Basic numeric values for combat
    [SerializeField] private float bulletForce = 10f;
    [SerializeField] private float timeBetweenShots = 0.1f;
    [SerializeField] private float bulletDamage = 1f;
    [SerializeField] private float bulletAccuracyThreshold = 21f;
    [SerializeField] private float singleAccuracy = 21f;
    [SerializeField] private float dualAccuracy = 31;


    private void Awake()
    {
        input = new UserInput();         //Initialize input
    }

    private void OnEnable()     //Enable input and assign input to functions
    {
        //Enable input
        input.Enable();

        //Assign start and stop shooting functions
        input.Player.ShootStart.performed += x => ShootStart();
        input.Player.ShootStop.performed += x => ShootStop();

        //Assign weapon ready and dual wield functions
        input.Player.ToggleWeaponReady.performed += x => ToggleWeaponReady();
        input.Player.ToggleDualWield.performed += x => ToggleDualWield();
    }

    private void OnDisable()    //Disable input and remove input function assignments
    {
        //Disable input
        input.Disable();

        //Remove start and stop shooting function assignments
        input.Player.ShootStart.performed -= x => ShootStart();
        input.Player.ShootStop.performed -= x => ShootStop();

        //Remove weapon ready and dual wield functions
        input.Player.ToggleWeaponReady.performed -= x => ToggleWeaponReady();
        input.Player.ToggleDualWield.performed -= x => ToggleDualWield();
    }

    private void ShootStart()   //isshooting becomes true to start shooting
    {
        isShooting = true;
    }

    private void ShootStop()    //isshooting becomes false to stop shooting
    {
        isShooting = false;
    }

    private IEnumerator PlayerShoot()   //calls firebullet function based on dualwield 
    { 
        alreadyShooting = true;     //assign alreadyshooting to true so method isnt called more than once per frame
        FireBullet(bulletPrefab, rightBulletFirePoint);
        if (dualWield)
        {
            FireBullet(bulletPrefab, leftBulletFirePoint);
        }
        yield return new WaitForSeconds(timeBetweenShots); //time between shots to make sure there is space between each shot
        alreadyShooting = false;
    }

    private void FireBullet(GameObject bPrefab, Transform bFirePoint) //Instantiates and adds velocity to bullet as well as damage value
    {
        //Rotates the path randomly
        float accuracyVariance = Random.Range(0, bulletAccuracyThreshold);
        bFirePoint.Rotate(0, 0, accuracyVariance, Space.Self);
        
        //Creates bullet
        GameObject bullet = Instantiate(bPrefab, bFirePoint.position, bFirePoint.rotation); 
        bullet.GetComponent<BulletBehavior>().setBulletDamage(bulletDamage);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        
        //Adds velocity
        bulletRB.AddForce(bFirePoint.up * bulletForce, ForceMode2D.Impulse);

        //Returns rotation to normal
        bFirePoint.Rotate(0, 0, -accuracyVariance, Space.Self);
    }

    private void ToggleWeaponReady() //Toggles weapon ready and visibility of guns to match
    {
        weaponReady = !weaponReady;
        rightHandGun.SetActive(!rightHandGun.activeSelf);
        if (dualWield)
        {
            leftHandGun.SetActive(!leftHandGun.activeSelf);
        }
    }

    private void ToggleDualWield() //Toggles dual wield and visibility of second gun to match
    {
        dualWield = !dualWield;
        if (weaponReady)
        {
            leftHandGun.SetActive(!leftHandGun.activeSelf);
        }
    }

    void FixedUpdate() //Updates at a fixed rate to see if character is shooting
    {
        if (dualWield)
        {
            bulletAccuracyThreshold = dualAccuracy;
        }
        else
        {
            bulletAccuracyThreshold = singleAccuracy;
        }
        
        if (isShooting && !alreadyShooting && weaponReady)
        {
            StartCoroutine(PlayerShoot());
        }
    }
}
