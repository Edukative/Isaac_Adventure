using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed; //speed of the enemy
    Rigidbody2D rb2D;
    public bool isVertical; //if it's not, it will walk horizontally

    //timer values 
    float timer;
    int direction = 1;
    public float changeTime = 3.0f;

    // animator values
    Animator anim;

    // waypoint values
    public GameObject wayPoints;
    public Vector2[] localNodes;
    // private Vector[] worldNodes;
    int currentNode;
    int nextNode;
    Vector2 Velocity;

    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // get the enemy's RigidBody
        anim = GetComponent<Animator>(); // get the enemy's Animator

        // waypoint stuff
        localNodes = new Vector2[wayPoints.transform.childCount];

        for (int i = 0; i <= wayPoints.transform.childCount - 1; ++i)
        {
            // from 0, to the lenght of the list, while is not there, sum 1 to the counter

            Transform child = wayPoints.transform.GetChild(i).transform;
            // get the transform of the child in the loop
            localNodes[i] = new Vector2(child.transform.position.x, child.transform.position.y);
            // put in the index a new Vector that is the position of the child
            Debug.Log("index" + i + "Transform" + localNodes[i]);
        }

        currentNode = 0;
        nextNode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        /*timer -= Time.deltaTime; // decreases the timer by the time of the project built

        if (timer < 0)
        {
            direction = -direction; // turns positive to negative and viceversa
            timer = changeTime; // resets the timer
        }*/

        //Vector2 position = rb2D.position;// get the curreent position of the enemy
        Vector2 wayPointDirection = localNodes[nextNode] - rb2D.position;
        UpdateAnimations(wayPointDirection);
        float dist = speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(wayPointDirection.sqrMagnitude);
        }
        if (wayPointDirection.sqrMagnitude < dist* dist) // if the enemy arrived to the
        {
            dist = wayPointDirection.magnitude;
            currentNode = nextNode; // it arrived to the waypoint so it turns to the next
            nextNode += 1;
            if (nextNode >= localNodes.Length) // it reached the end
            {
                nextNode = 0; // it loops again and goes to the first waypoint
            }
        }

        Velocity = wayPointDirection.normalized * dist;

        rb2D.MovePosition(rb2D.position + Velocity);
        // apply the previous sum position to the enemy's rigidbody

        /*if (isVertical) // if the enemy walks vertically
        {
            // same as isVertical == true;
            position.y = position.y + Time.deltaTime * speed * direction;

            // animator values setting
            anim.SetFloat("Move X", 0);
            anim.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            // sum the position x with the speed and the time 
            // animator values setting
            anim.SetFloat("Move X", direction);
            anim.SetFloat("Move Y", 0);
        }*/

        // apply the previous sum position to the enemy's rigidbody
    }
    void UpdateAnimations (Vector2 direction)
    {
        anim.SetFloat("Move X", direction.x);
        anim.SetFloat("Move Y", direction.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1); // damages the player
        }
    }
}