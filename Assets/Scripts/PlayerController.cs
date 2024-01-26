using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float limit = 1.5f;
    [SerializeField] BoxCollider2D PlayArea;

    [SerializeField] KeyCode upKey = KeyCode.UpArrow;
    [SerializeField] KeyCode downKey = KeyCode.DownArrow;
    [SerializeField] KeyCode leftKey = KeyCode.LeftArrow;
    [SerializeField] KeyCode rightKey = KeyCode.RightArrow;

    private Vector2 top;
    private Vector2 bottom;
    private Vector2 left;
    private Vector2 right;

    private Bounds movementBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        top = new Vector2(PlayArea.bounds.center.x, PlayArea.bounds.max.y);
        bottom = new Vector2(PlayArea.bounds.center.x, PlayArea.bounds.min.y);
        left = new Vector2(PlayArea.bounds.min.x, PlayArea.bounds.center.y);
        right = new Vector2(PlayArea.bounds.max.x, PlayArea.bounds.center.y);

        movementBounds = PlayArea.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(leftKey) )
            horizontalInput = -1f;
            rb.velocity = new Vector2(0, horizontalInput) * speed;

        if (Input.GetKey(rightKey))
            horizontalInput = 1f;
            rb.velocity = new Vector2(0, horizontalInput) * speed;

        if (Input.GetKey(downKey) )
            verticalInput = -1f;
            rb.velocity = new Vector2(0, verticalInput) * speed;

        if (Input.GetKey(upKey))
            verticalInput = 1f;
            rb.velocity = new Vector2(0, verticalInput) * speed;

        // Calculate the new position based on player input
        Vector2 newPosition = transform.position + new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;

        // Limit the player's movement within the bounds
        newPosition.x = Mathf.Clamp(newPosition.x, movementBounds.min.x, movementBounds.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, movementBounds.min.y, movementBounds.max.y);

        // Update the player's position
        transform.position = newPosition;
    }
}

