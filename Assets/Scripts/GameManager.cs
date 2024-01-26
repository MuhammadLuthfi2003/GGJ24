using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if ball prefab is in scene
        if (GameObject.FindAnyObjectByType<Ball>() == null)
        {
            // if not, instantiate it
            Instantiate(ballPrefab);
        }
    }
}
