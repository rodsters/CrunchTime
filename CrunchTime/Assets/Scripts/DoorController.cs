using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DoorController : MonoBehaviour
{
    [SerializeField]
    public Tilemap doorBlock;

    //create a list of boolean for each level
    //public Bool NewLevelUnlocked;


    // Start is called before the first frame update
    void Start()
    {
        doorBlock = GetComponent<Tilemap>();
    }
    //TODO : We need to cite this 
    //  Used this function and idea from https://youtu.be/QRp4V1JTZnM
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Projectile"){
             Debug.Log("INsideeee");
            Vector3 positionCollided = Vector3.zero;
            foreach(ContactPoint2D hit in collision.contacts)
            {
                positionCollided.x = hit.point.x - 0.01f *hit.normal.x;
                positionCollided.y = hit.point.y - 0.01f *hit.normal.y;
                doorBlock.SetTile(doorBlock.WorldToCell(positionCollided),null);
            }
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
