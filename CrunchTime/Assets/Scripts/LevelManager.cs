using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel = 1;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    public void incrementLevel()
    {
        currentLevel ++;
    }

    public int getCurLevel()
    {
        return currentLevel;
    }


}
