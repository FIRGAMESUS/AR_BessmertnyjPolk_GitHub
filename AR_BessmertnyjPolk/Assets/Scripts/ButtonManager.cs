using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
