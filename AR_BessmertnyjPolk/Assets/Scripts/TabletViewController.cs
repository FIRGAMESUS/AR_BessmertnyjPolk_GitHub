using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabletViewController : MonoBehaviour
{
    private PersonData person;

    public void SetData(PersonData data)
    {
        person = data;
        FillTablet();
    }

    private void FillTablet()
    {
        StartCoroutine(DownloadHelper.instance.GetImageByUrl(person.photo, (downloadedSprite) =>
        {
            transform.GetChild(0).Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 1);
            transform.GetChild(0).Find("Image").GetComponent<Image>().sprite = downloadedSprite;
            
        }));
        transform.GetChild(0).Find("Text").GetComponent<TextMeshProUGUI>().text = person.name;
    }
}
