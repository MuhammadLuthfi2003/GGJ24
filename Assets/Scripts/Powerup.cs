using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public static int popusSpawned = 0;

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
    [SerializeField] GameObject popupPrefab;
    [SerializeField] GameObject weirdSheepPrefab;

    private float defaultvalue;
    public bool isTaken = false;
    private GameObject popup;
    private GameObject weirdSheep;

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
                        AudioManager.instance.Play("Slime");
                    foreach (PlayerController player in players)
                        {
                            defaultvalue = player.speed;
                            player.speed /= value;
                        }
                        break;
                    case powerupType.Fast:
                        players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                        AudioManager.instance.Play("Nitro");
                        foreach (PlayerController player in players)
                        {
                            defaultvalue = player.speed;
                            player.speed *= value;
                        }
                        break;
                    case powerupType.Big:
                        ball = GameObject.FindObjectOfType<Ball>();
                        ball.isAffectedByPowerup = true;
                        AudioManager.instance.Play("Danger");

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
                        AudioManager.instance.Play("Small");
                        ball = GameObject.FindObjectOfType<Ball>();
                        ball.isAffectedByPowerup = true;
                        //players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                        //foreach (PlayerController player in players)
                        //{
                        //    defaultvalue = player.gameObject.transform.localScale.x;
                        //    player.gameObject.transform.localScale -= new Vector3(value, value, 0);
                        //}
                        defaultvalue = ball.gameObject.transform.localScale.x;
                        ball.gameObject.transform.localScale = new Vector3(value, value, 0);
                        break;
                    case powerupType.Popup:
                        int determinator = Random.Range(0, 2);
                        if (determinator == 0)
                    {
                        weirdSheep = Instantiate(weirdSheepPrefab, transform.position, Quaternion.identity);
                        duration *= 3;
                    }
                        else if (determinator == 1)
                    {
                        popup = Instantiate(popupPrefab, transform.position, Quaternion.identity);
                    }
                        
                    AudioManager.instance.Play("VineBoom");
                    break;
                }
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
                ball.isAffectedByPowerup = false;
                ball.gameObject.transform.localScale = new Vector3(defaultvalue, defaultvalue, 0);
                break;
            case powerupType.Small:
                ball = GameObject.FindObjectOfType<Ball>();
                ball.isAffectedByPowerup = false;
                ball.gameObject.transform.localScale = new Vector3(defaultvalue, defaultvalue, 0);
                break;
            case powerupType.Popup:
                if (popup != null)
                {
                    popup.GetComponent<Animator>().SetTrigger("close");
                }
                if (weirdSheep != null)
                {
                    Destroy(weirdSheep);
                }
                AudioManager.instance.Play("Error");
                break;
        }

        if (type != powerupType.Popup || weirdSheep != null)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(ClosePopup());
        }
    }

    IEnumerator ClosePopup()
    {
        yield return new WaitForSeconds(3f);
        Destroy(popup);
        Destroy(gameObject);
    }
}
