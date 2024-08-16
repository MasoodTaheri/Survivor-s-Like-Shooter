using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider ExperienceSlider;
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Text levelText;
    [SerializeField] private Text KillCountText;
    [SerializeField] private Text AmmoCountText;
    [SerializeField] private GameObject DeadPanel;
    [SerializeField] private int killCount;
    [SerializeField] private int levelValue;
    [Multiline][SerializeField] private string GameoverText;
    [SerializeField] private TMP_Text GameOverTMPText;

    public PlayerModel PlayerModel;

    public void Initialize(PlayerModel playerModel, EnemiesController enemiesController)
    {
        PlayerModel = playerModel;
        KillCountText.text = "0";
        levelValue = 0;
        levelText.text = $"Lv.{levelValue}";

        PlayerModel.OnHealthChanged += (value, max) =>
        {
            HealthSlider.value = 1.0f * value / max;
        };

        PlayerModel.OnAmmoChanged += (value, max) =>
        {
            AmmoCountText.text = value.ToString();
        };
        PlayerModel.OnExperienceChanged += (value, max) =>
        {
            ExperienceSlider.value = 1.0f * value / max;
            if (ExperienceSlider.value == 1)
            {
                LevelUpdate(1);
            }
        };

        PlayerModel.GetUpdatedValue();
        enemiesController.OnEnemyKilledChanged += IncKillCount;

    }

    public void IncKillCount(int count)
    {
        //killCount++;
        killCount = count;
        KillCountText.text = count.ToString();
    }

    public void LevelUpdate(int level)
    {
        levelValue += level;
        levelText.text = $"Lv.{levelValue}";
    }

    //public void ExperienceUpdate(int currentvalue, int maxValue)
    //{
    //    ExperienceSlider.value = 1.0f * currentvalue / maxValue;
    //    if (ExperienceSlider.value == 1)
    //    {
    //        LevelUpdate(1);
    //    }
    //}

    //public void UpdatePlayerModel(PlayerModel model)
    //{
    //    HealthSlider.value = 1.0f * modelcurrentvalue / maxValue;
    //}

    //public void UpdateHealth(int currentvalue, int maxValue)
    //{
    //    HealthSlider.value = 1.0f * currentvalue / maxValue;
    //}

    //public void UpdateAmmo(int ammoCount)
    //{
    //    AmmoCountText.text = ammoCount.ToString();
    //}

    public void PlayerDeadPanelShow()
    {
        DeadPanel.SetActive(true);
        GameOverTMPText.text = GameoverText.Replace("SSSS", killCount.ToString());
    }

    public void Restart() => SceneManager.LoadScene(1);
    //Debug.Log("restart");


    public void ExitGame()
    {

    }
}
