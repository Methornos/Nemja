using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuCanvas;

    [SerializeField]
    private GameObject _gameCanvas;

    [SerializeField]
    private GameObject _settingsPanel;

    [SerializeField]
    private GameObject _allSounds;

    [SerializeField]
    private AudioSource _music;

    [SerializeField]
    private Sounds _sounds;

    [SerializeField]
    private ScoreSystem _scoreSystem;

    public void ReloadScene()
    {
        _scoreSystem.AddScoreToCoins();

        Sounds.IsPlayCoins = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        _menuCanvas.SetActive(false);
        _gameCanvas.SetActive(true);

        _sounds.PlayBackgroundMusic();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SettingsPanelOn()
    {
      _settingsPanel.SetActive(true);
    }

    public void SettingsPanelOff()
    {
      _settingsPanel.SetActive(false);
    }

    public void SwitchMusic()
    {
      if(_music.mute == false) _music.mute = true;
      else _music.mute = false;
    }

    public void SwitchSounds()
    {
      _allSounds.SetActive(!_allSounds.activeSelf);
    }
}
