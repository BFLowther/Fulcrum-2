using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFX : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("SFX_Vol", Mathf.Log10(sliderValue) * 20);
    }
}
