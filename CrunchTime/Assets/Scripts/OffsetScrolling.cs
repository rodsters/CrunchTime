// Initial Idea:
// https://gist.github.com/satanas/5766d9d9d34f94be25cb0f85ffc50ad1

// Harrison Nguyen
// Poor scrolling background. Other idea: Background is image of dungeon.

 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;

public class OffsetScrolling : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float xFactor = 1.0f;
    [SerializeField] private float yFactor = 1.0f;

    private Image image;

    void Start () {
        image = GetComponent<Image> ();
    }

    void Update () {
        float x = (Time.deltaTime * scrollSpeed);
        float y = (Time.time % 40.0f);
        float zx = xFactor;
        float zy = yFactor;
        if (y < 10.0f){
            zx = -zx;
            zy = -zy;
        } else if (y < 20.0f) {
            zy = -zy;
        } else if (y > 30.0f) {
            zx = -zx;
        }
        Vector3 offset = new Vector3 (x*zx, x*zy, 0);
        image.transform.Translate(offset);
    }
}