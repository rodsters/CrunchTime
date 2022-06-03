using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // TODO: CITE THIS https://www.youtube.com/watch?v=C3VExnf4kmY
    [SerializeField]
    public GameObject globalStateGO; 
    
    [SerializeField]
    public GameObject enemy; 

    private GameObject newEnemy;
    private float xpos, ypos;
    private Vector3 spawnPosition;

    private GlobalGameState globalGameState;

    // Start is called before the first frame update
    void Start()
    {   
        globalGameState = globalStateGO.GetComponent<GlobalGameState>();

        spawnPosition = new Vector3(29,-40,0);
        Vector3[] temp = {spawnPosition};
        SpawnEnemy(temp);

    }
    
    public void SpawnEnemy(Vector3[] ranges)
    {
        spawnPosition = new Vector3(35,-40,0);
        newEnemy = Instantiate(enemy,spawnPosition,Quaternion.identity);
        globalGameState.incrementEnemies();
        Debug.Log("incrementing enemies : "+ globalGameState.getNumEnemies());
    }

}
