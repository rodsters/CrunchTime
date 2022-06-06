using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    // Referenced https://www.youtube.com/watch?v=LziIlLB2Kt4 to open shop panel with button.
    // Referenced https://www.youtube.com/watch?v=EEtOt0Jf7PQ&t=563s when creating actual UI itself.
    public GameObject Panel;

    // This checks the state of the panel, and changes it to the opposite. When the panel is closed, it opens it.
    public void Shop()
    {
        if (Panel != null)
        {
            bool shopOpen = Panel.activeSelf;
            Panel.SetActive(!shopOpen);
            Time.timeScale = 0;
        }
    }
}
