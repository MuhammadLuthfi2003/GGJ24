using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] Animator pauseAnimator;
    public static bool isPaused;
    public static bool isWin;
    private void Start()
    {
        isPaused = false;
        isWin = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseAnimator.SetTrigger("Pause");
        }
        if (!isWin)
        {
            Time.timeScale = (isPaused) ? 0 : 1;
        }
    }
    public void Win()
    {
        isPaused = !isPaused;
        isWin = true;
        AudioManager.instance.Play("Win");
        pauseAnimator.SetTrigger("Win");
    }
}
