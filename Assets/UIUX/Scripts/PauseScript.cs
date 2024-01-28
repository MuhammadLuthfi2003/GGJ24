using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public PauseManager pausedManager;
    public void Pause()
    {
        pausedManager.Pause();
    }
}
