using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum powerupType
    {
        Slow,
        Fast,
        Big,
        Small,
        Popup
    }

    [SerializeField] powerupType type;
    [SerializeField] float duration;
    [SerializeField] float value;

    private float defaultvalue;
    public bool isTaken = false ;

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
        if (collision.gameObject.GetComponent<Ball>() != null || collision.gameObject.GetComponent<PlayerController>() != null)
        {
                isTaken = true;
                PlayerController[] players;
                Ball ball;
                switch (type)
                {
                    case powerupType.Slow:
                        players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                        foreach (PlayerController player in players)
                        {
                            defaultvalue = player.speed;
                            player.speed /= value;
                        }
                        break;
                    case powerupType.Fast:
                        players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                        foreach (PlayerController player in players)
                        {
                            defaultvalue = player.speed;
                            player.speed *= value;
                        }
                        break;
                    case powerupType.Big:
                        ball = GameObject.FindObjectOfType<Ball>();
                        //players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                        //foreach (PlayerController player in players)
                        //{
                        //    defaultvalue = player.gameObject.transform.localScale.x;
                        //    player.gameObject.transform.localScale += new Vector3(value, value, 0);
                        //}
                        defaultvalue = ball.gameObject.transform.localScale.x;
                        ball.gameObject.transform.localScale += new Vector3(value, value, 0);
                        break;
                    case powerupType.Small:
                        ball = GameObject.FindObjectOfType<Ball>();
                        //players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                        //foreach (PlayerController player in players)
                        //{
                        //    defaultvalue = player.gameObject.transform.localScale.x;
                        //    player.gameObject.transform.localScale -= new Vector3(value, value, 0);
                        //}
                        defaultvalue = ball.gameObject.transform.localScale.x;
                        ball.gameObject.transform.localScale -= new Vector3(value, value, 0);
                        break;
                    case powerupType.Popup:

                        break;
                }
                GetComponent<SpriteRenderer>().color = new Color32(0,0,0,0);
                if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = false;
            }


                StartCoroutine(turnBackNormal());

            
        }
    }

    IEnumerator turnBackNormal()
    {
        yield return new WaitForSeconds(duration);
        PlayerController[] players;
        Ball ball;
        switch (type)
        {
            case powerupType.Slow:
                players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                foreach (PlayerController player in players)
                {
                    player.speed = defaultvalue;
                }
                break;
            case powerupType.Fast:
                players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                foreach (PlayerController player in players)
                {
                    player.speed = defaultvalue;
                }
                break;
            case powerupType.Big:
                ball = GameObject.FindObjectOfType<Ball>();
                ball.gameObject.transform.localScale = new Vector3(defaultvalue, defaultvalue, 0);
                break;
            case powerupType.Small:
                ball = GameObject.FindObjectOfType<Ball>();
                ball.gameObject.transform.localScale = new Vector3(defaultvalue, defaultvalue, 0);
                break;
            case powerupType.Popup:
                break;
        }
        Destroy(gameObject);
    }
}
