/*****************************************************************************
// File Name :         SheriffBehavor.cs
// Author :            Cade R. Naylor
// Creation Date :     March 19, 2023
//
// Brief Description : Creates the Sheriff Behavior, links them to PlayerActions, 
                        handles attacks, movement, power up/weapon switching
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SheriffBehavior : MonoBehaviour
{
    #region Variables

    //Create an instance of input
    InputActionAsset inputAsset;
    InputActionMap inputMap;
    PlayerInput pInput;

    //Create a reference for each inputAction
    InputAction playerMovement;
    InputAction scopeMovement;
    InputAction quickAttack;
    InputAction chargeAttack;
    InputAction switchWeapon;
    InputAction switchPowerUp;
    InputAction pauseMenu;
    InputAction playerInteract;
    

    //Temporary Variables
    Vector2 movement;
    Vector2 scopePos;

    //Variables for Attacks
    [SerializeField] private GameObject scope;
    private int scopeRange = 100;
    [SerializeField] private WeaponData weapon;
    private SpriteRenderer gunImage;
    [SerializeField] private GameObject gun;
    private bool chgAtkAvailable = true;
    private bool atkAvailable = true;
    public float scopeDistance;
    public float dmgShot;
    private int ammo;
    private int maxAmmo;
    private bool canAttack;

    //Sounds
    [SerializeField] AudioClip revolverShot;
    [SerializeField] AudioClip pistolShot;
    [SerializeField] AudioClip shotgunShot;
    [SerializeField] AudioClip damage;

    //Other Variables
    [SerializeField] private GameObject sheriff;
    [SerializeField] private Sprite revolver;
    [SerializeField] private Sprite shotgun;
    [SerializeField] private Sprite pistol;
    [SerializeField] private GameObject revolverBullet;
    [SerializeField] private GameObject shotgunBullet;
    [SerializeField] private GameObject pistolBullet;
    [SerializeField] private GameObject atkPoint;
    [SerializeField] private float playerhealth = 200;
    private bool weaponChanged = false;
    private int weaponNumber = 1;
    Coroutine stopMe;
    [SerializeField] private int cells;
    BanditBehavior player2;
    GameObject player2Obj;
    //public static SheriffBehavior instance;
    //public static GameObject player;
    //public static List<GameObject> playerList = new List<GameObject>();


    private UIManagerBehavior uim;
    private NPC npc;
    

    public float Playerhealth { get => playerhealth; set => playerhealth = value; }

    public int Ammo { get => ammo; set => ammo = value; }

    public bool Weaponchanged { get => weaponChanged; set => weaponChanged = value; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public int WeaponNumber { get => weaponNumber; set => weaponNumber = value; }

    public int Cells { get => cells; set => cells = value; }
    #endregion

    #region Functions

    //Sets up control references
    #region Set Up
    /// <summary>
    /// Awake is called before start. Gets references to Player Controls.
    /// </summary>
    private void Awake()
    {
       
        inputAsset = this.GetComponent<PlayerInput>().actions;
        inputMap = inputAsset.FindActionMap("Player1Actions");
        playerMovement = inputMap.FindAction("Movement");
        scopeMovement = inputMap.FindAction("MoveScope");
        switchWeapon = inputMap.FindAction("SwitchWeapon");
        quickAttack = inputMap.FindAction("QuickAttack");
        chargeAttack = inputMap.FindAction("ImpactAttack");
        switchPowerUp = inputMap.FindAction("SwitchPowerup");
        pauseMenu = inputMap.FindAction("PauseMenu");
        playerInteract = inputMap.FindAction("Interact");
       
        Ammo = weapon.Ammo;
        maxAmmo = weapon.MaxAmmo;
        uim = GameObject.Find("UIManager").GetComponent<UIManagerBehavior>();
        pInput = GetComponent<PlayerInput>();
        pInput.camera = Camera.current;

        gunImage = gun.GetComponent<SpriteRenderer>();
        gunImage.sprite = revolver;

        //player = GameObject.Find("Grayboxed Sheriff");

       
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            npc = GameObject.Find("Bartender").GetComponent<NPC>();
        }
        /*
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            playerList.AddRange(GameObject.FindGameObjectsWithTag("player"));
            DontDestroyOnLoad(player);
        }
        */
        
        /*
        //Movement - Left Stick
        //Reads in input from the Left Stick and saves it to a temporary variable
        playerMovement.performed += contx => movement = contx.ReadValue<Vector2>();
        //When the Left Stick is not being pressed, set the temp variable to 0
        playerMovement.canceled += contx => movement = Vector2.zero;


        //Scope Movement - Right Stick
        //Reads in input from the Right Stick and saves it to a temporary variable
        scopeMovement.performed += contx => scopePos = contx.ReadValue<Vector2>();
        //When the Right Stick is not being pressed, set the temp variable to 0
        scopeMovement.canceled += contx => scopePos = Vector2.zero;

        //Weapon Switching - Left Trigger
        switchWeapon.performed += contx => SwitchWeapon();

        //Quick Attack - A button
        quickAttack.started += contx => stopMe=StartCoroutine(QuickAtk());
        quickAttack.canceled += contx => StopShooting();

        //Charged Attack - B Button
        chargeAttack.started += contx => stopMe=StartCoroutine(ChargeAtk());
        chargeAttack.canceled += contx => StopShooting();

        //Powerup Switching - Right Trigger
        switchPowerUp.performed += contx => SwitchPowerUp();

        //Pause Menu - Start Button
        pauseMenu.performed += contx => uim.PauseMenu();
        playerInteract.performed += contx => npc.TriggerDialogue();*/
    }

    /// <summary>
    /// Turns on Action Maps
    /// </summary>
    private void OnEnable()
    {
        playerMovement.performed += contx => movement = contx.ReadValue<Vector2>();
        playerMovement.canceled += contx => movement = Vector2.zero;
        playerMovement.Enable();
        scopeMovement.performed += contx => scopePos = contx.ReadValue<Vector2>();
        scopeMovement.canceled += contx => scopePos = Vector2.zero;
        scopeMovement.Enable();
        switchWeapon.performed += contx => SwitchWeapon();
        switchWeapon.Enable();
        quickAttack.started += contx => stopMe = StartCoroutine(QuickAtk());
        quickAttack.canceled += contx => StopShooting();
        quickAttack.Enable();
        chargeAttack.started += contx => stopMe = StartCoroutine(ChargeAtk());
        chargeAttack.canceled += contx => StopShooting();
        chargeAttack.Enable();
        switchPowerUp.performed += contx => SwitchPowerUp();
        switchPowerUp.Enable();
        pauseMenu.performed += contx => uim.PauseMenu();
        pauseMenu.Enable();
        playerInteract.performed += contx => npc.TriggerDialogue();
        playerInteract.Enable();
    }

    /// <summary>
    /// Turns off action maps
    /// </summary>
    private void OnDisable()
    {
        playerMovement.performed -= contx => movement = contx.ReadValue<Vector2>();
        playerMovement.canceled -= contx => movement = Vector2.zero;
        playerMovement.Disable();
        scopeMovement.performed -= contx => scopePos = contx.ReadValue<Vector2>();
        scopeMovement.canceled -= contx => scopePos = Vector2.zero;
        scopeMovement.Disable();
        switchWeapon.performed -= contx => SwitchWeapon();
        switchWeapon.Disable();
        quickAttack.started -= contx => stopMe = StartCoroutine(QuickAtk());
        quickAttack.canceled -= contx => StopShooting();
        quickAttack.Disable();
        chargeAttack.started -= contx => stopMe = StartCoroutine(ChargeAtk());
        chargeAttack.canceled -= contx => StopShooting();
        chargeAttack.Disable();
        switchPowerUp.performed -= contx => SwitchPowerUp();
        switchPowerUp.Disable();
        pauseMenu.performed -= contx => uim.PauseMenu();
        pauseMenu.Disable();
        playerInteract.performed -= contx => npc.TriggerDialogue();
        playerInteract.Disable();
    }

    
    #endregion Set Up

    //Handles player attacks and switching weapons
    #region Attacks and Weapons
    /// <summary>
    /// Attacks using the player's Charged Attack, if available
    /// </summary>
    IEnumerator  ChargeAtk()
    {
        for(; ; ) 
        {
            if (weapon.Ammo == 0)
            {
                print("Out of Ammo");
            }
            else
            {
                if (chgAtkAvailable && weapon && canAttack)
                {
                    GameObject temp;
                    //Attack, then start the cooldown timer
                    //print(weapon.Weapon + " deals " + weapon.ChargeDmg + " damage.
                    //" + weapon.Ammo + " shots remaining.");
                    if(weapon.Weapon == WeaponData.WeaponID.REVOLVER)
                    {
                        temp = Instantiate(revolverBullet, transform.position, 
                            Quaternion.identity);
                        temp.GetComponent<SheriffBulletBehavior>().damageDealt =
                            weapon.ChargeDmg;
                        temp.GetComponent<SheriffBulletBehavior>().Shoot(scope);
                        temp.GetComponent<SheriffBulletBehavior>().shotByPlayer = 
                            true;
                        AudioSource.PlayClipAtPoint(revolverShot, transform.position, .7f);
                    }
                    if (weapon.Weapon == WeaponData.WeaponID.SHOTGUN)
                    {
                        temp = Instantiate(shotgunBullet, transform.position,
                            Quaternion.identity);
                        temp.GetComponent<ShotgunBulletBehavior>().damageDealt =
                            weapon.ChargeDmg;
                        temp.GetComponent<ShotgunBulletBehavior>().Shoot(scope);
                        temp.GetComponent<ShotgunBulletBehavior>().shotByPlayer =
                            true;
                        AudioSource.PlayClipAtPoint(shotgunShot, transform.position, 5f);
                    }
                    if (weapon.Weapon == WeaponData.WeaponID.PISTOL)
                    {
                        temp = Instantiate(pistolBullet, transform.position, 
                            Quaternion.identity);
                        temp.GetComponent<PistolBulletBehavior>().damageDealt =
                            weapon.ChargeDmg;
                        temp.GetComponent<PistolBulletBehavior>().Shoot(scope);
                        temp.GetComponent<SheriffBulletBehavior>().shotByPlayer =
                            true;
                        AudioSource.PlayClipAtPoint(pistolShot, transform.position, .6f);
                    }
                    chgAtkAvailable = false;
                    StartCoroutine(ChargeWeaponCoolDown());
                    weapon.Ammo--;

                    Ammo = weapon.Ammo;
                }
                else
                {
                    print(weapon.Weapon + " is on cooldown.");
                }
            }
            yield return new WaitForSeconds(weapon.ChargeCD);
        }
    }

    /// <summary>
    /// The cooldown timer for a charged attack
    /// </summary>
    /// <returns>How long before charged attack can occur again</returns>
    IEnumerator ChargeWeaponCoolDown()
    {
        yield return new WaitForSeconds(weapon.ChargeCD);
        chgAtkAvailable = true;
    }


    /// <summary>
    /// Attacks using the player's standard attack, if available
    /// </summary>
    IEnumerator QuickAtk()
    {
        for(; ; )
        {
            if (weapon.Ammo == 0)
            {
                print("Out of Ammo");
            }
            else
            {
                if (atkAvailable && weapon && canAttack)
                {
                    GameObject temp;
                    if (weapon.Weapon == WeaponData.WeaponID.REVOLVER)
                    {
                        temp = Instantiate(revolverBullet, transform.position, 
                            Quaternion.identity);
                        temp.GetComponent<SheriffBulletBehavior>().damageDealt =
                            weapon.Dmg;
                        temp.GetComponent<SheriffBulletBehavior>().Shoot(scope);
                        temp.GetComponent<SheriffBulletBehavior>().shotByPlayer =
                            true;
                        AudioSource.PlayClipAtPoint(revolverShot, transform.position, .7f);
                    }
                    if (weapon.Weapon == WeaponData.WeaponID.SHOTGUN)
                    {
                        temp = Instantiate(shotgunBullet, transform.position, 
                            Quaternion.identity);
                        temp.GetComponent<ShotgunBulletBehavior>().shotByPlayer =
                            true;
                        temp.GetComponent<ShotgunBulletBehavior>().damageDealt =
                            weapon.Dmg;
                        temp.GetComponent<ShotgunBulletBehavior>().Shoot(scope);
                        AudioSource.PlayClipAtPoint(shotgunShot, transform.position, 5f);
                    }
                    if (weapon.Weapon == WeaponData.WeaponID.PISTOL)
                    {
                        temp = Instantiate(pistolBullet, transform.position, 
                            Quaternion.identity);
                        temp.GetComponent<PistolBulletBehavior>().damageDealt =
                            weapon.Dmg;
                        temp.GetComponent<PistolBulletBehavior>().Shoot(scope);
                        temp.GetComponent<PistolBulletBehavior>().shotByPlayer =
                            true;
                        AudioSource.PlayClipAtPoint(pistolShot, transform.position, .6f);
                    }
                    atkAvailable = false;
                    StartCoroutine(WeaponCoolDown());
                    weapon.Ammo--;
                    Ammo = weapon.Ammo;
                }
                else
                {
                    print(weapon.Weapon + " is on cooldown.");
                }
            }
            yield return new WaitForSeconds(weapon.StandardCD);
        }
    }

    /// <summary>
    /// The cooldown timer for an attack
    /// </summary>
    /// <returns>How long before attack can occur again</returns>
    IEnumerator WeaponCoolDown()
    {
        yield return new WaitForSeconds(weapon.StandardCD);
        atkAvailable = true;
    }


    /// <summary>
    /// Stops the Coroutine currently making the player shoot
    /// </summary>
    private void StopShooting()
    {
        StopCoroutine(stopMe);
        print("stop");
    }

    /// <summary>
    /// Switches the WeaponData the player is currently using
    /// </summary>
    private void SwitchWeapon()
    {
        string fileName = "";
        if (weapon.Weapon == WeaponData.WeaponID.REVOLVER)
        {
            fileName = "SHOTGUN_DATA";
            print("Weapon switched to Shotgun");
            gunImage.sprite = shotgun;
            weaponNumber = 2;
        }
        else if (weapon.Weapon == WeaponData.WeaponID.SHOTGUN)
        {
            fileName = "PISTOL_DATA";
            print("Weapon switched to Pistol");
            gunImage.sprite = pistol;
            weaponNumber = 3;
        }
        else if (weapon.Weapon == WeaponData.WeaponID.PISTOL)
        {
            fileName = "REVOLVER_DATA";
            print("Weapon switched to Revolver");
            gunImage.sprite = revolver;
            weaponNumber = 1;
        }
        weapon = Resources.Load<WeaponData>(fileName);

        //Reset the attack cooldowns
        chgAtkAvailable = true;
        atkAvailable = true;
        Ammo = weapon.Ammo;
        maxAmmo = weapon.MaxAmmo;
        weaponChanged = true;
        StartCoroutine(WeaponChange());
    }

    /// <summary>
    /// Resets weaponChanged after a brief pause
    /// </summary>
    /// <returns>Time paused for</returns>
    IEnumerator WeaponChange()
    {
        yield return new WaitForSeconds(.1f);
        weaponChanged = false;
    }

    /// <summary>
    /// Switches the PowerUp Data the player has access to
    /// </summary>
    private void SwitchPowerUp()
    {
        //code here to get list of power ups and move to next index
    }
    

    #endregion

    //Handles player and scope movement
    #region Movement

    /// <summary>
    /// Handles player and scope movement
    /// </summary>
    private void FixedUpdate()
    {
        SheriffArt sherArt = GetComponent<SheriffArt>();
        //Create a reference to the player's position
        Vector2 playerPos = transform.position;
        Vector2 newScopePos;
        Vector2 movementVelocity = new Vector2(movement.x, movement.y) * 5 *
            Time.deltaTime;
        float fAngle;
        float scopeDistance;
        Quaternion playerRot = transform.rotation;

        //Translate is a movement function
        transform.Translate(movementVelocity, Space.Self);

        //Clamp the player's position to stay on screen
        //ClampPlayer(transform.position);

        //Set the scope's position to the new value while ensuring it revolves
        //around the player

        fAngle = Mathf.Atan(scopePos.y / scopePos.x);
        scopeDistance = Mathf.Sqrt((Mathf.Pow(scopePos.x, 2)) +
            (Mathf.Pow(scopePos.y, 2)));


        newScopePos.x = playerPos.x + (scopePos.x * scopeDistance * scopeRange *
            Time.deltaTime);
        newScopePos.y = playerPos.y + (scopePos.y * scopeDistance * scopeRange *
            Time.deltaTime);

        scope.transform.position = newScopePos;

        SheriffArt sherA = sheriff.GetComponent<SheriffArt>();

        //Sets the animation based on the direction the player is walking in
        sherA.SetDirection(movementVelocity, playerRot);

        scopeDistance = Mathf.Sqrt(Mathf.Pow(newScopePos.x - playerPos.x, 2) + Mathf.Pow(newScopePos.y - playerPos.y, 2));

    }

    /// <summary>
    /// Checks if the scope is far enough away from the player
    /// </summary>
    private void Update()
    {
        Vector2 difference = transform.position - scope.transform.position;
        if (difference.magnitude >= 1)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if (playerhealth <= 0)
        {
            if (player2Obj != null)
            {
                player2.freeMove = true;
            }
            Destroy(gameObject);
        }
    }



    /// <summary>
    /// Clamps the player's position to remain onscreen
    /// </summary>
    /// <param name="pos">The player's position</param>
    private void ClampPlayer(Vector2 pos)
    {
        Vector2 playerBind = pos;

        if (pos.x > 8.4f)
        {
            playerBind.x = 8.4f;
        }
        if (pos.x < -8.4f)
        {
            playerBind.x = -8.4f;
        }
        if (pos.y > 4.5f)
        {
            playerBind.y = 4.5f;
        }
        if (pos.y < -4.5f)
        {
            playerBind.y = -4.5f;
        }
        transform.position = playerBind;
    }

    #endregion

    //Handles collisions with Enemies and attacks
    #region Collisions

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Large TumbleFiend(Clone)")
        {
            //take large tumble damage
            LargeTumbleFiendBehavior ltfb = collision.gameObject.GetComponent
                <LargeTumbleFiendBehavior>();
            playerhealth -= ltfb.damageDealt;
            AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
        }
        if (collision.gameObject.name == "Small TumbleFiend(Clone)")
        {
            //take large tumble damage
            SmallTumbleFiendBehavior stfb = collision.gameObject.GetComponent
                <SmallTumbleFiendBehavior>();
            playerhealth -= stfb.sDamageDealt;
            AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "explosion")
        {
            if (collision.name.Contains("Kami"))
            {
                DamageStoreExplodeBehavior dseb = collision.
                    GetComponent<DamageStoreExplodeBehavior>();
                playerhealth -= dseb.damageDealt;
                AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
            }
            if (collision.name.Contains("Spike"))
            {
                ExplodeSpikeBehavior esb = collision.GetComponent
                    <ExplodeSpikeBehavior>();
                playerhealth -= esb.damageDealt; 
                AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
            }
        }
        if (collision.gameObject.tag == "Spike")
        {
            CactusSpikeBehavior csb = collision.GetComponent<CactusSpikeBehavior>();
            playerhealth -= csb.damageDealt;
            AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
        }

        //Boss Attack damage
        if (collision.gameObject.tag == "bullet")
        {
            if (collision.name.Contains("Pistol"))
            {
                PistolBulletBehavior pbb =
                    collision.gameObject.GetComponent<PistolBulletBehavior>();

                //if bullet not shot by a player, take damage
                if (!pbb.shotByPlayer)
                {
                    playerhealth -= pbb.damageDealt;
                    AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
                }
            }
            if (collision.name.Contains("Revolver"))
            {
                SheriffBulletBehavior sbb =
                    collision.gameObject.GetComponent<SheriffBulletBehavior>();

                //if bullet not shot by a player, take damage
                if (!sbb.shotByPlayer)
                {
                    playerhealth -= sbb.damageDealt;
                    AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
                }
            }
            if (collision.name.Contains("Spray"))
            {
                SprayShotgunBulletBehavior ssbb =
                    collision.GetComponent<SprayShotgunBulletBehavior>();

                //if bullet not shot by a player, take damage
                if (!ssbb.shotByPlayer)
                {
                    playerhealth -= ssbb.damageDealt;
                    AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
                }
            }
            else if (collision.name.Contains("Shotgun"))
            {
                ShotgunBulletBehavior shotbb =
                    collision.gameObject.GetComponent<ShotgunBulletBehavior>();

                //if bullet not shot by a player, take damage
                if (!shotbb.shotByPlayer)
                {
                    playerhealth -= shotbb.damageDealt;
                    AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
                }
            }
        }
        if (collision.gameObject.tag == "explodey")
        {
            if (collision.name.Contains("Fire"))
            {
                FireBehavior fb = collision.gameObject.GetComponent<FireBehavior>();

                //If explosion not created by player, take damage
                if (!fb.shotByPlayer)
                {
                    playerhealth -= fb.damageDealt;
                    AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
                }
            }
            else if (collision.name.Contains("Kaboom"))
            {
                DamageStoreExplodeBehavior dseb = collision.gameObject.
                    GetComponent<DamageStoreExplodeBehavior>();

                //If explosion not created by player, take damage
                if (!dseb.shotByPlayer)
                {
                    playerhealth -= dseb.damageDealt;
                    AudioSource.PlayClipAtPoint(damage, transform.position, 2f);
                }
            }
        }
    }

    #endregion Collisions


    #endregion Functions
}