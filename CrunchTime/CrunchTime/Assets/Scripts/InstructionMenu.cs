using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InstructionMenu : MonoBehaviour
{
    // Only button on menu takes player back to main menu.
    public void BackButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
