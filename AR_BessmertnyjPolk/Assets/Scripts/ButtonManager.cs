using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeadMosquito.AndroidGoodies;

public class ButtonManager : MonoBehaviour
{
    public GameManager GameManager;

    public GameObject ButtonCanvas;
    public GameObject InfoCanvas;
    void Start()
    {
        BackButtonPressed();
    }

    public void RefreshButtonPressed()
    {
        GameManager.UpdateTablets();
    }

    public void InfoButtonPressed()
    {
        InfoCanvas.SetActive(true);
        ButtonCanvas.SetActive(false);
    }

    public void BackButtonPressed()
    {
        InfoCanvas.SetActive(false);
        ButtonCanvas.SetActive(true);
    }

    public void onShareButtonClick()
    {
        string text = "Я использую приложение 'Полк AR - 9 мая 2020', посвященное людям участвовавшим в Великой Отечественной Войне - " +
                        "https://play.google.com/store/apps/details?id=com.FIRGAMESUS.PolkAR (доступно в Google Play)!" +
                        " #сидимдома #оставайтесьдома #домалучше #лучшедома #9мая #деньпобеды #спасибодедузапобеду";
        AGShare.ShareScreenshotWithText(text);
    }
}
