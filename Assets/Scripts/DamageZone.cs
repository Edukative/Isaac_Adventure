using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("object that entered the trigger:" + other);


        RubyController Controller = other.GetComponent<RubyController>();
        // get the player controller from the other thing collided with the trigger
        if (Controller != null) // if the controller retrevied is not empty
        {
            // ! the exclamation is a negation value
            Controller.ChangeHealth(-1);
            // call the health function and add 1 to the health of the player
            
        }
    }
}
