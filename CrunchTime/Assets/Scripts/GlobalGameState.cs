using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameState : MonoBehaviour
{
    private int numEnemies; 

    // // Start is called before the first frame update
    void Start()
    {
        numEnemies = 0;
    }

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
