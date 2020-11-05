using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource _swipesSource;

    [SerializeField]
    private GameObject _sounds;

    [SerializeField]
    private AudioSource _backgroundSource;

    private static AudioSource _coinsSource;

    public List<AudioClip> AudioList;

    public static bool IsPlayCoins = false;

    private void Start()
    {
        _coinsSource = GameObject.FindGameObjectWithTag("CoinsSource").GetComponent<AudioSource>();
    }

    public void PlayFlySound()
    {
        _swipesSource.clip = RandomFlySound();
        _swipesSource.Play();
    }

    public static void PlayCoinSound()
    {
        if (IsPlayCoins) _coinsSource.Play();
        IsPlayCoins = false;
    }

    public void PlayBackgroundMusic()
    {
        _backgroundSource.Play();
    }

    private AudioClip RandomFlySound()
    {
        return AudioList[Random.Range(0, AudioList.Count)];
    }
}
