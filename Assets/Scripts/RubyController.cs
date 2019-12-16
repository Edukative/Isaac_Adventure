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

    // Start is called before the first frame update
    void Start()
    {
        rubyRB2D = GetComponent<Rigidbody2D>(); // Get the player's rigidbody
        currentHealth = maxHealth; // the current health is the max health available to the player

        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // get the horizontal input
        float vertical = Input.GetAxis("Vertical"); // get the vertical input

        Vector2 position = transform.position; // makes a vector based on current position

        position.x = position.x + speed * horizontal * Time.deltaTime; // the position is equal to the same position but a little bit bigger
        position.y = position.y + speed * vertical * Time.deltaTime; // called each second instead of each frame

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
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
