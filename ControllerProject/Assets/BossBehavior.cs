using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] GameObject shield;
    [SerializeField] GameObject tower;
    List<GameObject> towerList = new List<GameObject>();
    private bool canBeAttacked;
    GameObject activeShield;

    Vector3 towerSpawn;
    private float health = 200;




    // Start is called before the first frame update
    void Start()
    {
        towerSpawn = transform.position;
        towerSpawn.x -= 1;
        towerSpawn.y -= 3;
        towerList.Add(Instantiate(tower, towerSpawn, Quaternion.identity));
        activeShield = Instantiate(shield, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveTower(GameObject destroyedTower)
    {
        towerList.Remove(destroyedTower);
        Destroy(destroyedTower);
        print("Towers left " + towerList.Count);
        if (towerList.Count == 0)
        {
            Destroy(activeShield);
        }
    }
}
