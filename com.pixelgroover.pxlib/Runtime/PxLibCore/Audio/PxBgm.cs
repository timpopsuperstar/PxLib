using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PxAddressables;

[System.Serializable]
public class PxBgm 
{
    public AudioClip BgmIntro => _bgmIntro;
    [SerializeField] private AudioClip _bgmIntro;
    public AudioClip BgmLoop => _bgmLoop;
    [SerializeField] private AudioClip _bgmLoop;
}
