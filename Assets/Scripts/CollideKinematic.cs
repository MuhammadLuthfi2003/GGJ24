using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideKinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody != null && collision.gameObject.name.StartsWith("Player"))
        {
            collision.rigidbody.bodyType = RigidbodyType2D.Dynamic;
            StopAllCoroutines();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.rigidbody != null && collision.gameObject.name.StartsWith("Player"))
        {
            StartCoroutine(revertRB(collision.rigidbody));
        }
    }

    IEnumerator revertRB(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(.2f);
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
