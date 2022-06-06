using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCondition : MonoBehaviour
{
    // When the player clicks the exit, load the victory screen.
    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }

}
