using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFX : MonoBehaviour
{
    [SerializeField] private string[] sfxNames;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.instance.Play(sfxNames[Gacha.RollD20()]);
    }
}
