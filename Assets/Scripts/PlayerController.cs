using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float limit = 1.5f;

    [SerializeField] KeyCode upKey = KeyCode.UpArrow;
    [SerializeField] KeyCode downKey = KeyCode.DownArrow;
    [SerializeField] KeyCode leftKey = KeyCode.LeftArrow;
    [SerializeField] KeyCode rightKey = KeyCode.RightArrow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upKey))
        {
            rb.velocity = new Vector2(0, 1) * speed;
        }
        else if (Input.GetKey(downKey))
        {
            rb.velocity = new Vector2(0, -1) * speed;
        }
        else if (Input.GetKey(leftKey))
        {
            rb.velocity = new Vector2(-1, 0) * speed;
        }
        else if (Input.GetKey(rightKey))
        {
            rb.velocity = new Vector2(1, 0) * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
