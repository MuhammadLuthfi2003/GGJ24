using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{

    [SerializeField] float minSpeedToMove = 0.2f;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] float timelimit = 3f;

    private Rigidbody2D rb;
    private bool shouldMoveAutomatically = false;
    private Vector2 randomPosition;
    private float t;
    private bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = 0;
        boxCollider = GameObject.FindGameObjectWithTag("MoveArea").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((rb.velocity.x < minSpeedToMove || rb.velocity.y < minSpeedToMove) && !shouldMoveAutomatically && !isHit)
        {
            shouldMoveAutomatically = true;
            t = 0;
            // get random pos and move to it
            randomPosition = GetRandomPositionInBox();
            MoveObjectToPosition(randomPosition);
        }
        else if (rb.velocity.x > minSpeedToMove || rb.velocity.y > minSpeedToMove && shouldMoveAutomatically)
        {
            shouldMoveAutomatically = false;
            
        }

        if (shouldMoveAutomatically )
        {
            t += Time.deltaTime;

            if (t >= timelimit)
            {
                t = 0;
            }
        }
    }

    private Vector2 GetRandomPositionInBox()
    {
        // Get the bounds of the BoxCollider2D
        Bounds bounds = boxCollider.bounds;

        // Generate random X and Y coordinates within the bounds
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        // Return the random position as a Vector2
        return new Vector2(randomX, randomY);
    }

    private void MoveObjectToPosition(Vector2 targetPosition)
    {
        // Calculate the direction to the target position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Set the velocity to move the object in the calculated direction
        rb.velocity = direction * 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Audio Effect
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isHit)
            {
                isHit = true;
                StartCoroutine(deleteHit());
                //rb.velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                t = 0;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(deleteHit());
            }
        }
    }

    IEnumerator deleteHit()
    {
        yield return new WaitForSeconds(3);
        isHit = false;
    }
}
