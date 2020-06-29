using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rubyRB2D; // the player's Rigidbody

    public int maxHealth = 5;
    public int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float InvincibleTimer;

    // animator values
    Animator anim;
    Vector2 lookDirection = new Vector2(1, 0); // the direction is facing the player in the scene

    // projectile values
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rubyRB2D = GetComponent<Rigidbody2D>(); // Get the player's rigidbody
        currentHealth = maxHealth; // the current health is the max health available to the player
        anim = GetComponent<Animator>(); // get the player's Animator

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // get the horizontal input
        float vertical = Input.GetAxis("Vertical"); // get the vertical input

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) // if it's moving
        {
            lookDirection.Set(move.x, move.y); // sets the vector already created
            lookDirection.Normalize(); // just puts the number into 1 or -1
        }

        // animator set values
        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", move.magnitude);

        Vector2 position = rubyRB2D.position; // makes a vector based on current position
        position = position + move * speed * Time.deltaTime;

        //transform.position = position; // saves this position to the current one
        rubyRB2D.MovePosition(position);

        if (isInvincible) // invincible because the player has collided with damage
        {
            InvincibleTimer -= Time.deltaTime; // count down the timer

            if (InvincibleTimer < 0) // the timer ended
            {
                isInvincible = false; // the player is vulnerable again
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0) // as is inferior to 0, it means damage
        {
            if (isInvincible) // already invincible? Don't do anything
            {
                return;
            }
            isInvincible = true; // make the player invincible
        }


        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); // limits the number between 0 to max health

        //calls the static UIHealth to set the current health divided by the max health
        UIHealthBar.instance.SetValue((float)currentHealth / (float)maxHealth);

        //Debug.Log(currentHealth + "/" + maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rubyRB2D.position + Vector2.up * 0.5f, Quaternion.identity);
        // spawns a projectile and stores it inside a GameObject variable

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        // gets the projectile script of the spawned projectile and stores it inside a variable

        projectile.Launch(lookDirection, 300);

        anim.SetTrigger("Launch"); // Sets the shooting animation
    }
}
