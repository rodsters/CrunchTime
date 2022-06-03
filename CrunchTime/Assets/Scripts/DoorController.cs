using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DoorController : MonoBehaviour
{

    [SerializeField]
    public GameObject globalStateGO; 
    private GlobalGameState globalGameState;

    [SerializeField]
    public Tilemap tileMap;

    [SerializeField]
    public int doorLevel;

    [SerializeField]
    public bool destroy = false;

    public int numEnemies;

     
    //create a list of boolean for each level
    //public Bool NewLevelUnlocked;

    public List<Vector3> availablePlaces;

    // Start is called before the first frame update
    void Start()
    {  
        globalGameState = globalStateGO.GetComponent<GlobalGameState>();


        tileMap = transform.GetComponentInParent<Tilemap>();
        availablePlaces = new List<Vector3>();
 
        // TODO : FIND THE CITATION FOR THIS DO NOT FORGET 
        // if the link is missing, remind amaan to find the resource where he fount this from
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }

    }

    //TODO : We need to cite this 
    //  Used this function and idea from https://youtu.be/QRp4V1JTZnM

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision) {       
        if(collision.gameObject.tag == "Player"){   
            if(globalGameState.getNumEnemies() <= 0 )
            {
                foreach (var pos in availablePlaces)
                {   
                    tileMap.SetTile(Vector3Int.FloorToInt(pos), null);
                }
                Debug.Log("opening doors");
            }


        }
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}

