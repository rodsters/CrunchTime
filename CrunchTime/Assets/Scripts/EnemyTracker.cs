using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    private int numEnemies = 0; 

    // // Start is called before the first frame update
    // void Start()
    // {
    //     numEnemies = 0;
    // }

    public void incrementEnemies()
    {
        numEnemies++ ; 
    }
    public void DecrementEnemies()
    {
        numEnemies-- ; 
    }
    public int getNumEnemies()
    {
        return numEnemies;
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
