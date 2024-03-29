using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float speed;
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
    private Vector2 previousPosition;
    private Animator anim;
    public bool isAffectedByPowerup = false;
    private float defaultSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        top = new Vector2(PlayArea.bounds.center.x, PlayArea.bounds.max.y);
        bottom = new Vector2(PlayArea.bounds.center.x, PlayArea.bounds.min.y);
        left = new Vector2(PlayArea.bounds.min.x, PlayArea.bounds.center.y);
        right = new Vector2(PlayArea.bounds.max.x, PlayArea.bounds.center.y);

        movementBounds = PlayArea.bounds;
        previousPosition = rb.position;
        defaultSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAffectedByPowerup)
        {
            speed = defaultSpeed;
        }

        GetInput();
    }

    private void GetInput()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.anyKey)
        {
            if (Input.GetKey(leftKey))
                horizontalInput = -1f;
                anim.SetFloat("SpeedX", horizontalInput);
            rb.velocity = new Vector2(horizontalInput,0) * speed * 2;

            if (Input.GetKey(rightKey))
                horizontalInput = 1f;
            anim.SetFloat("SpeedX", horizontalInput);
            rb.velocity = new Vector2(horizontalInput,0 ) * speed * 2;

            if (Input.GetKey(downKey))
                verticalInput = -1f;
            anim.SetFloat("SpeedY", verticalInput);
            rb.velocity = new Vector2(0, verticalInput) * speed / 3;

            if (Input.GetKey(upKey))
                verticalInput = 1f;
            anim.SetFloat("SpeedY", verticalInput);
            rb.velocity = new Vector2(0, verticalInput) * speed / 3;
        }
        else
        {
            anim.SetFloat("SpeedX", 0);
            anim.SetFloat("SpeedY", 0);
        }


        // Calculate the new position based on player input
        Vector2 newPosition = transform.position + new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;

        // Limit the player's movement within the bounds
        newPosition.x = Mathf.Clamp(newPosition.x, movementBounds.min.x, movementBounds.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, movementBounds.min.y, movementBounds.max.y);

        // Update the player's position
        transform.position = newPosition;
        //rb.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        StopAllCoroutines();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        StopAllCoroutines();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //StartCoroutine(reenableRB());
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    rb.bodyType = RigidbodyType2D.Dynamic;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    StartCoroutine(reenableRB());
    //}

    IEnumerator reenableRB()
    {
        yield return new WaitForSeconds(0.2f);
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}

