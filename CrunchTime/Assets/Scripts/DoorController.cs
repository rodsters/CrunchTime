using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DoorController : MonoBehaviour
{

    //[SerializeField]
    //public GameObject globalStateGO; 

    [SerializeField]
    public Tilemap tileMap;

    [SerializeField]
    public int doorLevel;

    [SerializeField]
    public bool destroy = false;


    private GameObject gameManager;

    private EnemyTracker  enemyTracker;
    private LevelManager levelManager;
    private int curLvl; 
    
    //create a list of boolean for each level
    //public Bool NewLevelUnlocked;

    public List<Vector3> availablePlaces;
    private bool levelLock = false;

    // Start is called before the first frame update
    void Start()
    {  

        gameManager = GameObject.Find("GameManager");
        enemyTracker = gameManager.GetComponent<EnemyTracker>();
        levelManager = gameManager.GetComponent<LevelManager>();

        // TODO : FIND THE CITATION FOR THIS DO NOT FORGET 
        // https://forum.unity.com/threads/tilemap-tile-positions-assistance.485867/
        // by username: DDaddySupreme https://forum.unity.com/members/ddaddysupreme.1403037/
        tileMap = transform.GetComponentInParent<Tilemap>();
        availablePlaces = new List<Vector3>();

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
        if (!levelLock)
        {
           
            if (levelManager.getCurLevel() == doorLevel)
            {
                foreach (var pos in availablePlaces)
                {   
                    tileMap.SetTile(Vector3Int.FloorToInt(pos), null);
                }
                Debug.Log("opening doors "+doorLevel );
                levelLock = true;
            }
        }

    }

    // private void OnCollisionEnter2D(Collision2D collision) {    

    //     //Debug.Log(globalGameState.getNumEnemies());   
    //     if(collision.gameObject.tag == "Player"){  

    //        // GlobalGameState globalGameState = globalStateGO.GetComponent<GlobalGameState>();

    //         if(enemyTracker.getNumEnemies() <= 0 )
    //         {
    //             foreach (var pos in availablePlaces)
    //             {   
    //                 tileMap.SetTile(Vector3Int.FloorToInt(pos), null);
    //             }
    //             Debug.Log("opening doors");

    //             //increment level here 
    //             levelManager.incrementLevel();

    //         }
            
    //     }


    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}

