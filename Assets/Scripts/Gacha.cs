using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha
{
    public static int RollD20()
    {
        // Create a Random object for rolling the d20

        // Roll the d20 (generate a random number between 1 and 20)
        int roll = Random.Range(1, 21);

        // Determine the result category based on the roll
        int result;
        if (roll >= 1 && roll <= 10)
        {
            result = 0; // Category 1: 1-10
        }
        else if (roll >= 11 && roll <= 15)
        {
            result = 1; // Category 2: 11-15
        }
        else if (roll >= 16 && roll <= 19)
        {
            result = 2; // Category 3: 16-19
        }
        else
        {
            result = 3; // Category 4: 20
        }

        return result;
    }
}
