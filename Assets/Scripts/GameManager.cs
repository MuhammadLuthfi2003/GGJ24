using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Powerup> powerups;
    [SerializeField] BoxCollider2D area;
    public float interval = 10f;
    public float lifetime = 10f;
    private float t = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if (t >= interval)
        {
            t = 0;
            Vector2 randomPos = GetRandomPositionInBox();
            int randomIndex = Random.Range(0, powerups.Count);
            Powerup powerup = Instantiate(powerups[randomIndex], randomPos, Quaternion.identity);
            StartCoroutine(DepletePowerup(powerup));
        }
    }

    public Vector2 GetRandomPositionInBox()
    {

        // Get the bounds of the BoxCollider2D
        Bounds bounds = area.bounds;

        // Generate random X and Y coordinates within the bounds
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        // Return the random position as a Vector2
        return new Vector2(randomX, randomY);
    }

    IEnumerator DepletePowerup(Powerup powerup)
    {
        yield return new WaitForSeconds(lifetime);
        if (powerup != null)
        {
            if (!powerup.isTaken)
            {
                Destroy(powerup.gameObject);
            }
        }
    }
}
