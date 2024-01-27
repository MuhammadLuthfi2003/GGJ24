using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void Back()
    {
        MainMenuManager.instances.MoveGroupObjects(0);
    }
}
