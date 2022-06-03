using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // TODO: CITE THIS https://www.youtube.com/watch?v=C3VExnf4kmY
    //[SerializeField]
    //public GameObject globalStateGO; 
    
    private GameObject enemy; 

    private GameObject gameManager;

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

        gameManager = GameObject.Find("GameManager");
        enemyTracker = gameManager.GetComponent<EnemyTracker>();
        levelManager = gameManager.GetComponent<LevelManager>();

        //level 1 spawn
        var tempPosition = new Vector3(35f,-40f,0f);
        var tempPosition2 = new Vector3(37,-37,0f);
        List<Vector3> temp = new List<Vector3>();
        temp.Add(tempPosition);
        temp.Add(tempPosition2);

        SpawnEnemy(temp);
    }
    
    public void SpawnEnemy(List<Vector3> ranges)
    {
        //spawnPosition = new Vector3(35,-40,0);
        foreach(var pos in ranges)
        {               
            Debug.Log("position spwning "+ pos);
            newEnemy = Instantiate(enemy,pos,Quaternion.identity);
            enemyTracker.incrementEnemies();
            Debug.Log("incrementing enemies : "+ enemyTracker.getNumEnemies());
        }


       // GlobalGameState globalGameState = globalStateGO.GetComponent<GlobalGameState>();
        //globalGameState.incrementEnemies();
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
                var tempPosition = new Vector3(27.3f,5.75f,0);
                List<Vector3> temp = new List<Vector3>();
                temp.Add(tempPosition);
                SpawnEnemy(temp);
                levelLocks[curLvl-1] = true;
            }
        }else if(curLvl == 3)
        {
            if(!levelLocks[curLvl-1])
            {
                var tempPosition = new Vector3(-54.13f,2.55f,0);
                List<Vector3> temp = new List<Vector3>();
                temp.Add(tempPosition);
                SpawnEnemy(temp);
                levelLocks[curLvl-1] = true;  
            }

        }else if(curLvl == 4)
        {
            if(!levelLocks[curLvl-1])
            {
                var tempPosition = new Vector3(-66.59f,62.68f,0);
                List<Vector3> temp = new List<Vector3>();
                temp.Add(tempPosition);
                SpawnEnemy(temp);
                levelLocks[curLvl-1] = true;  
            }
        }else if(curLvl == 5)
        {
            if(!levelLocks[curLvl-1])
            {
                var tempPosition = new Vector3(10.4f,78.6f,0);
                List<Vector3> temp = new List<Vector3>();
                temp.Add(tempPosition);
                SpawnEnemy(temp);
                levelLocks[curLvl-1] = true;  
            }
        }else if(curLvl == 6)
        {
            if(!levelLocks[curLvl-1])
            {
                var tempPosition = new Vector3(80.6f,76.9f,0);
                List<Vector3> temp = new List<Vector3>();
                temp.Add(tempPosition);
                SpawnEnemy(temp);
                levelLocks[curLvl-1] = true;  
            }
        }
    } 

}
