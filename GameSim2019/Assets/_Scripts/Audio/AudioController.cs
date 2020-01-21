using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{

    public AudioMixer musicChannel;
    public AudioMixer SFX;


    public void SetVolume ( float vol )
    {
        musicChannel.SetFloat("MusicVol", Mathf.Log10(vol) * 20);
    }

    public void SetVolumeSFX ( float vol )
    {
        SFX.SetFloat("SFXVol", Mathf.Log10(vol) * 20);
    }

}
