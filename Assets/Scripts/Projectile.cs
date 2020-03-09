using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D projectileRB2D;
    // Start is called before the first frame update
    private void Awake()
    {
        projectileRB2D = GetComponent<Rigidbody2D>(); // Get the rigidbody
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f) // if it's too away from the scene
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        Debug.Log("launched function");
        projectileRB2D.AddForce(direction * force); // Add a force to the rigidbody
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Debug.Log("Projectile Collision with " + collision.gameObject);
        EnemyController e = collision.collider.GetComponent<EnemyController>();
        if (e != null) // if has succesfully retrieved the Enemy script
        {
            e.Fix(); // call the Fix function from the enemy
        }
        Destroy(gameObject); // destroys the projectile
    }
}
