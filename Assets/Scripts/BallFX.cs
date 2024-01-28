using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFX : MonoBehaviour
{
    [SerializeField] private string[] sfxNames;
    private float pitchControl;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        pitchControl = Random.Range(0.5f, 3f);
        AudioManager.instance.Play(sfxNames[Gacha.RollD20()], pitchControl);
    }
}
