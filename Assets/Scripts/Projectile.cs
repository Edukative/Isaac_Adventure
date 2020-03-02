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

    }

    public void Launch(Vector2 direction, float force)
    {
        Debug.Log("launched function");
        projectileRB2D.AddForce(direction * force); // Add a force to the rigidbody
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Debug.Log("Projectile Collision with " + collision.gameObject);
        Destroy(gameObject); // destroys the projectile
    }
}
