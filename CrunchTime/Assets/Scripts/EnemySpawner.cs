using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // TODO: CITE THIS https://www.youtube.com/watch?v=C3VExnf4kmY


    [SerializeField]
    public GameObject spawns; 
    
    private GameObject enemy; 
    private GameObject bulletStormRangedEnemy;
    private GameObject giantEnemy;
    private GameObject meleeAndRangedEnemy;
    private GameObject miniEnemy;
    private GameObject rangedEnemy;
    private GameObject shotgunRangedEnemy;
    private GameObject gameManager;
    private GameObject speedyEnemy;

    private EnemyTracker  enemyTracker;
    private LevelManager levelManager;


    private GameObject newEnemy;
    private float xpos, ypos;
    private Vector3 spawnPosition;

    private bool spawnLock = true;

    private bool[] levelLocks = {false,false,false, false,false ,false,false};
    // Start is called before the first frame update
    
    void Start()
    {   
        enemy = Resources.Load("Enemy") as GameObject;
        bulletStormRangedEnemy = Resources.Load("BulletStormRangedEnemy") as GameObject;
        giantEnemy = Resources.Load("GiantEnemy") as GameObject;
        meleeAndRangedEnemy = Resources.Load("MeleeAndRangedEnemy") as GameObject;
        miniEnemy = Resources.Load("MiniEnemy") as GameObject;
        rangedEnemy = Resources.Load("RangedEnemy") as GameObject;
        shotgunRangedEnemy = Resources.Load("ShotgunRangedEnemy") as GameObject;
        speedyEnemy = Resources.Load("SpeedyEnemy") as GameObject;
        gameManager = GameObject.Find("GameManager");
        enemyTracker = gameManager.GetComponent<EnemyTracker>();
        levelManager = gameManager.GetComponent<LevelManager>();
        
        //level 1 spawn
        // level1.Add((1, new Vector3(35f,-40f,0f)));
        // level1.Add((1, new Vector3(37,-37,0f)));

        // {
        //     (1, new Vector3(35f,-40f,0f)),
        //     (1, new Vector3(37,-37,0f))
        // };

        GameObject level1Spawn = spawns.transform.GetChild(0).gameObject;

        // for(int i  =0;i< level1Spawn.transform.childCount; i++)
        // {
        //     GameObject child =  level1Spawn.transform.GetChild(i).gameObject;
        //     var pos = child.transform.position;
        //     level1.Add((1, new Vector3(pos.x,pos.y,0)));
        // }

        SpawnEnemy(CreateLocationsAndTypes(level1Spawn));
    }

    private List<(GameObject, Vector3)> CreateLocationsAndTypes(GameObject spwn)
    {        
        var lvlSpwn = new List<(GameObject, Vector3)>();
        GameObject tempGO;
        for(int i  =0;i< spwn.transform.childCount; i++)
        {
            GameObject child =  spwn.transform.GetChild(i).gameObject;
            var pos = child.transform.position;

            if(child.name == "Enemy")
            {
                tempGO = enemy;
            }
            else if(child.name == "BulletStormRangedEnemy")
            {
                tempGO = bulletStormRangedEnemy;
            }
            else if(child.name == "GiantEnemy")
            {
                tempGO = giantEnemy;
            }
            else if(child.name == "MeleeAndRangedEnemy")
            {
                tempGO = meleeAndRangedEnemy;
            }
            else if(child.name == "MiniEnemy")
            {
                tempGO = miniEnemy;
            }
            else if(child.name == "ShotgunRangedEnemy")
            {
                tempGO = shotgunRangedEnemy;
            }
            else if(child.name == "SpeedyEnemy")
            {
                tempGO = speedyEnemy;
            }
            else
            {
                tempGO = rangedEnemy;
            }


            lvlSpwn.Add((tempGO, new Vector3(pos.x,pos.y,0)));
        }
        return lvlSpwn;
    }
    
    public void SpawnEnemy(List<(GameObject typeEnemy, Vector3 position)> ranges)
    {
        foreach(var lvl in ranges)
        {               
            Debug.Log("position spwning "+ lvl.position);
            
            newEnemy = Instantiate(lvl.typeEnemy,lvl.position,Quaternion.identity);
            enemyTracker.incrementEnemies();
            //Debug.Log("incrementing enemies : "+ enemyTracker.getNumEnemies());
        }
        Debug.Log("total new enemies: "+ enemyTracker.getNumEnemies());

    }


    void Update()
    {
        if(enemyTracker.getNumEnemies() <= 0)
        {
            levelManager.incrementLevel();
        }

        int curLvl = levelManager.getCurLevel();
        if(curLvl == 2)
        {
            if(!levelLocks[curLvl-1])
            {   
                // var level2 = new List<(int, Vector3)>
                // {
                //     (1, new Vector3(27.3f,5.75f,0)),
                //     (1, new Vector3(30.3f,5.75f,0))
                // };

                GameObject level2Spawn = spawns.transform.GetChild(curLvl-1).gameObject;


                SpawnEnemy(CreateLocationsAndTypes(level2Spawn));
                levelLocks[curLvl-1] = true;
            }
        }
        else if(curLvl == 3)
        {
            if(!levelLocks[curLvl-1])
            {
                // var tempPosition = new Vector3(-54.13f,2.55f,0);
                // List<Vector3> temp = new List<Vector3>();
                // temp.Add(tempPosition);

                // var level3 = new List<(int, Vector3)>
                // {
                //     (1, new Vector3(-54.13f,2.55f,0)),
                //     (1, new Vector3(-54.13f,2.55f,0)),
                //     (1, new Vector3(-54.13f,2.55f,0))
                // };
                // SpawnEnemy(level3);

                GameObject level3Spawn = spawns.transform.GetChild(curLvl-1).gameObject;


                SpawnEnemy(CreateLocationsAndTypes(level3Spawn));
                levelLocks[curLvl-1] = true;  
            }

        }else if(curLvl == 4)
        {
            if(!levelLocks[curLvl-1])
            {
                // var tempPosition = new Vector3(-66.59f,62.68f,0);
                // List<Vector3> temp = new List<Vector3>();
                // temp.Add(tempPosition);
                // var level4 = new List<(int, Vector3)>
                // {
                //     (1, new Vector3(-66.59f,62.68f,0)),
                //     (1, new Vector3(-66.59f,62.68f,0)),
                //     (1, new Vector3(-66.59f,62.68f,0)),
                //     (1, new Vector3(-66.59f,62.68f,0))

                // };
                // SpawnEnemy(level4);
                GameObject level4Spawn = spawns.transform.GetChild(curLvl-1).gameObject;
                SpawnEnemy(CreateLocationsAndTypes(level4Spawn));
                levelLocks[curLvl-1] = true;  
            }
        }else if(curLvl == 5)
        {
            if(!levelLocks[curLvl-1])
            {
                // var tempPosition = new Vector3(10.4f,78.6f,0);
                // List<Vector3> temp = new List<Vector3>();
                // temp.Add(tempPosition);
                
                // var level5 = new List<(int, Vector3)>
                // {
                //     (1, new Vector3(10.4f,78.6f,0)),
                //     (1, new Vector3(10.4f,78.6f,0)),
                //     (1, new Vector3(10.4f,78.6f,0)),
                //     (1, new Vector3(10.4f,78.6f,0)),
                //     (1, new Vector3(10.4f,78.6f,0))
                // };
                // SpawnEnemy(level5);
                GameObject level5Spawn = spawns.transform.GetChild(curLvl-1).gameObject;
                SpawnEnemy(CreateLocationsAndTypes(level5Spawn));
                levelLocks[curLvl-1] = true;  
            }
        }else if(curLvl == 6)
        {
            if(!levelLocks[curLvl-1])
            {
                // var tempPosition = new Vector3(80.6f,76.9f,0);
                // List<Vector3> temp = new List<Vector3>();
                // temp.Add(tempPosition);

                // var level6 = new List<(int, Vector3)>
                // {
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0))
                // };
                // SpawnEnemy(level6);
                GameObject level6Spawn = spawns.transform.GetChild(curLvl-1).gameObject;
                SpawnEnemy(CreateLocationsAndTypes(level6Spawn));

                levelLocks[curLvl-1] = true;  
            }
            
        }else if(curLvl == 7)
        {
            if(!levelLocks[curLvl-1])
            {
                // var tempPosition = new Vector3(80.6f,76.9f,0);
                // List<Vector3> temp = new List<Vector3>();
                // temp.Add(tempPosition);

                // var level6 = new List<(int, Vector3)>
                // {
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0)),
                //     (1, new Vector3(81.6f,76.9f,0))
                // };
                // SpawnEnemy(level6);
                GameObject level7Spawn = spawns.transform.GetChild(curLvl-1).gameObject;
                SpawnEnemy(CreateLocationsAndTypes(level7Spawn));

                levelLocks[curLvl-1] = true;  
            }
            
        }
    } 

}
