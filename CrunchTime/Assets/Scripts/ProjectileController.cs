using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float speed = 7;
    // Update is called once per frame
    void Update()
    {
        // get mouse position
        // Vector3 mousePos = Input.mousePosition;   
        transform.position += transform.right * Time.deltaTime * this.speed;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name != "RainbowMan")
        {
            Destroy(gameObject);
        }
    }
}
