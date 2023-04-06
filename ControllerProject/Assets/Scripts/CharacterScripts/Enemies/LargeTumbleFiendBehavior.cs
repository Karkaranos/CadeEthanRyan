using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeTumbleFiendBehavior : MonoBehaviour
{
    #region Variables
    //TumbleFiend Specific variables
    [SerializeField] private int smallerTumblesSpawned = 3;
    [SerializeField] private GameObject smallTumble;

    //References to players and setting targets
    private int target;
    private GameObject targetObject;
    [SerializeField] GameObject player1;
    //[SerializeField] GameObject player2;
    Vector3 offset;

    //General variables
    [SerializeField] private float speed = 3f;
    [SerializeField] private int cellsForDeath;
    #endregion Variables

    #region Functions
    /// <summary>
    /// Start is called before the first frame
    /// Sets enemy target and initial values for a couple variables
    /// </summary>
    void Start()
    {
        player1 = GameObject.Find("Grayboxed Sheriff");
        target = 1;
        //target = Random.Range(1, 2);
        if (target == 1)
        {
            targetObject = player1;
        }
        /*else
        {
            targetObject = player2;
        }*/
        offset.x = 3;
        offset.y = 3;
        offset.y = 3;

    }

    /// <summary>
    /// Update is called once per frame
    /// Tracks the player
    /// </summary>
    void Update()
    {
        TrackTargetPlayer(targetObject);
    }

    /// <summary>
    /// Moves towards the chosen target
    /// </summary>
    /// <param name="target">The player the enemy is targeting</param>
    void TrackTargetPlayer(GameObject target)
    {
        Vector2 targetPos = target.transform.position;
        Vector2 difference;
        Vector2 moveForce = Vector2.zero;

        difference.x = targetPos.x - transform.position.x;
        difference.y = targetPos.y - transform.position.y;
        if (difference.x < 0)
        {
            moveForce.x += -1 * speed * Time.deltaTime;
        }
        else
        {
            moveForce.x += 1 * speed * Time.deltaTime;
        }
        if (difference.y < 0)
        {
            moveForce.y += -1 * speed * Time.deltaTime;
        }
        else
        {
            moveForce.y += 1 * speed * Time.deltaTime;
        }
        transform.Translate(moveForce, Space.Self);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            OnDeath();
            print("Hit");
        }
    }
    public virtual void OnDeath()
    {
        Vector2 spawnPos = transform.position;
        for (int i=-1; i<smallerTumblesSpawned-1; i++)
        {
            spawnPos.x = Random.Range(-1, 1);
            spawnPos.y = Random.Range(-1, 1);
            Instantiate(smallTumble, spawnPos, transform.rotation);
            spawnPos = transform.position;
        }
        Destroy(gameObject);
    }
    #endregion Functions
}
