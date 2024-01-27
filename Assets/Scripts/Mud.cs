using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    public int speedModifier = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<PlayerController>() != null)
        {
            collision.transform.GetComponent<PlayerController>().speed /= speedModifier;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<PlayerController>() != null)
        {
            collision.transform.GetComponent<PlayerController>().speed *= speedModifier;
        }
    }
}
