using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStat : MonoBehaviour
{
    SheriffBehavior sb;
    [Range(5, 20)]
    [SerializeField] private int healthAdded;
    [Range(5, 20)]
    [SerializeField] private int ammoAdded;
    private bool valAdded;
    // Start is called before the first frame update
    void Start()
    {
        sb = GameObject.Find("Grayboxed Sheriff").GetComponent<SheriffBehavior>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Destroy(gameObject);
            if (name.Contains("heart")&&!valAdded)
            {
                sb.Playerhealth += healthAdded;
                print(sb.Playerhealth);
                valAdded = true;
            }
            if (name.Contains("ammo")&&!valAdded)
            {
                sb.Ammo += ammoAdded;
                print(sb.Ammo);
                valAdded = true;
                if (sb.Ammo > sb.MaxAmmo)
                {
                    sb.Ammo = sb.MaxAmmo;
                }
            }
            Destroy(gameObject);
        }
    }

}
