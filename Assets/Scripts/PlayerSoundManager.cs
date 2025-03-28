using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip step;
    [SerializeField] AudioClip camFocus;
    public void Step()
    {
        SoundManager.Instance.PlaySFX(step);
    }

    public void CameraReady()
    {
        SoundManager.Instance.PlaySFX(camFocus);
    }
}
