using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _bgmClip;

    private void OnEnable()
    {
        SoundManager.Instance.PlayBGM(_bgmClip);
    }
}
