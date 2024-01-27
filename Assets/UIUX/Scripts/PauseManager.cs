using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] Animator pauseAnimator;
    public static bool isPaused;
    private void Start()
    {
        isPaused = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseAnimator.SetTrigger("Pause");
        }
        Time.timeScale = (isPaused) ? 0 : 1;
    }
}
