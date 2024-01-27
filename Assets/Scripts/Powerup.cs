using System.Collections;
using System.Collections.Generic;
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
        if (gameObject.GetComponent<Ball>() != null)
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                PlayerController[] players;
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
                        players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                        foreach (PlayerController player in players)
                        {
                            defaultvalue = player.gameObject.transform.localScale.x;
                            player.gameObject.transform.localScale += new Vector3(value, value, 0);
                        }
                        break;
                    case powerupType.Small:
                        players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                        foreach (PlayerController player in players)
                        {
                            defaultvalue = player.gameObject.transform.localScale.x;
                            player.gameObject.transform.localScale -= new Vector3(value, value, 0);
                        }
                        break;
                    case powerupType.Popup:

                        break;
                }
                GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(turnBackNormal());

            }
        }
    }

    IEnumerator turnBackNormal()
    {
        yield return new WaitForSeconds(duration);
        PlayerController[] players;
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
                players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                foreach (PlayerController player in players)
                {
                    player.gameObject.transform.localScale = new Vector3(defaultvalue, defaultvalue, 0);
                }
                break;
            case powerupType.Small:
                players = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
                foreach (PlayerController player in players)
                {
                    player.gameObject.transform.localScale = new Vector3(defaultvalue, defaultvalue, 0);
                }
                break;
            case powerupType.Popup:
                break;
        }
        Destroy(gameObject);
    }
}
