using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
 {
    public IDictionary<string, List<(int, Vector3)> > levelDictionary ;   // = new Dictionary<int, string>();

    void Start()
    {
        levelDictionary =  new Dictionary<string, List<(int, Vector3)> >();
        var level1 = new List<(int, Vector3)>
        {
            (1, new Vector3(35f,-40f,0f)),
            (1, new Vector3(37,-37,0f))
        };

        levelDictionary.Add("1",level1);

        var level2 = new List<(int, Vector3)>
        {
            (1, new Vector3(27.3f,5.75f,0)),
            (1, new Vector3(30.3f,5.75f,0))
        };

        levelDictionary.Add("2",level2);


    }

 }