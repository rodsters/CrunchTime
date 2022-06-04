using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes the spritemask expand and shrink over time.
public class SpriteMaskStretch : MonoBehaviour
{
    private Transform mask;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    [SerializeField] private float duration;
    private float startTime;
    [SerializeField] private bool expand;
    
    void Start()
    {
        mask = GetComponent<Transform>();
        startTime = Time.time;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        if (t >= 1.0f)
        {
            expand = !expand;
            startTime = Time.time;
            t = (Time.time - startTime) / duration;
        }
        
        Vector3 newScale;
        if (expand)
        {
            float temp = Mathf.SmoothStep(minScale, maxScale, t);
            newScale = new Vector3(temp, temp, 1);
        }
        else
        {
            t = 1.0f - t;
            float temp = Mathf.SmoothStep(minScale, maxScale, t);
            newScale = new Vector3(temp, temp, 1);
        }
        
        mask.localScale = newScale;
    }
}
