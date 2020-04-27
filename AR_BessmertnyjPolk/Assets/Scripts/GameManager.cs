﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public Transform Tablets;
    //public GameObject Tablet;
    public string url;
    Image image;
    TextMeshProUGUI text;

    private string path;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        //path = Path.Combine(Application.persistentDataPath, "PersonData/data.json");
        path = Path.Combine(Application.streamingAssetsPath, "PersonData/data.json");
#else
        path = Path.Combine(Application.streamingAssetsPath, "PersonData/data.json");
#endif
        Debug.Log(path);
        Debug.Log("Started");
        ReadJson();
        StartCoroutine(SetInfo());
    }
    
    public PersonDataList PersonDataList = new PersonDataList();
    public void ReadJson()
    {
        string json = File.ReadAllText(path);
        //string json = File.ReadAllText(Application.dataPath + "/PersonData/data.json");
        JsonUtility.FromJsonOverwrite(json, PersonDataList);
        Debug.Log(PersonDataList.PersonData[0].url);
        Debug.Log(PersonDataList.PersonData[1].name);
        Debug.Log(PersonDataList.PersonData.Count);
    }
    

    Texture myTexture;
    IEnumerator SetInfo()
    {
        int id = 0;
        foreach (Transform Tablet in Tablets)
        {
            var person = PersonDataList.PersonData[id];
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(person.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) Debug.Log(www.error);
            else myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite photo = Sprite.Create((Texture2D)myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);
            Debug.Log("Loaded");

            var data = PersonDataList.PersonData[id];
            Tablet.GetChild(0).Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 1);
            Tablet.GetChild(0).Find("Image").GetComponent<Image>().sprite = photo;
            Tablet.GetChild(0).Find("Text").GetComponent<TextMeshProUGUI>().text = person.name;
            id++;
        }
    }
    
}

[System.Serializable]
public class PersonData
{
    public string url;
    public string name;
}
[System.Serializable]
public class PersonDataList
{
    public List<PersonData> PersonData;
}
