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

    private GameObject newEnemy;
    private float xpos, ypos;
    private Vector3 spawnPosition;

   // private GlobalGameState globalGameState;

    // Start is called before the first frame update
    void Start()
    {   
        enemy = Resources.Load("Enemy") as GameObject;

        gameManager = GameObject.Find("GameManager");
        enemyTracker = gameManager.GetComponent<EnemyTracker>();

        spawnPosition = new Vector3(29,-40,0);
        Vector3[] temp = {spawnPosition};
        SpawnEnemy(temp);
    }
    
    public void SpawnEnemy(Vector3[] ranges)
    {
        spawnPosition = new Vector3(35,-40,0);
        newEnemy = Instantiate(enemy,spawnPosition,Quaternion.identity);
        enemyTracker.incrementEnemies();
        Debug.Log("incrementing enemies : "+ enemyTracker.getNumEnemies());

       // GlobalGameState globalGameState = globalStateGO.GetComponent<GlobalGameState>();
        //globalGameState.incrementEnemies();
    }

}
