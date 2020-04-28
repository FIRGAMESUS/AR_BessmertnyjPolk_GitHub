using System.Collections;
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
    public Sprite DefaultPhoto;

    private string path;
    private int tabletsCount;

    void Start()
    {
        tabletsCount = Tablets.childCount;
        path = Application.streamingAssetsPath + "/PersonData/data_file.json";
        ReadJson();
        UpdateTablets();
        Debug.Log("Started");
    }

    public void UpdateTablets()
    {
        StartCoroutine(SetInfo());
    }
    
    public PersonDataList PersonDataList = new PersonDataList();
    public void ReadJson()
    {
        string json;
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(path);
            www.SendWebRequest();
            json = www.downloadHandler.text;
        }
        else
        {
            json = File.ReadAllText(path);
        }
        JsonUtility.FromJsonOverwrite(json, PersonDataList);
        Debug.Log(PersonDataList.PersonData.Count);
    }

    public void OpenURL()
    {
        Debug.Log("click");
        Application.OpenURL(PersonDataList.PersonData[0].url);
    }
    

    Texture myTexture;
    IEnumerator SetInfo()
    {
        List<int> currentPersons = new List<int>();
        //List<int> currentTablets = new List<int>();
        int currentPerson;
        for (int i = 0; i < tabletsCount; i++)
        {
            do
            {
                currentPerson = Random.Range(0, PersonDataList.PersonData.Count);
            } while (currentPersons.IndexOf(currentPerson) >= 0);
            currentPersons.Add(currentPerson);
        }
        int id = 0;
        foreach (Transform Tablet in Tablets)
        {
            var person = PersonDataList.PersonData[currentPersons[id]];

            Sprite photo;
            if (person.photo.IndexOf("svg") > 0) photo = DefaultPhoto;
            else
            {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(person.photo);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError) Debug.Log(www.error);
                else myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                photo = Sprite.Create((Texture2D)myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);
            }

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
    public string photo;
    public string name;
}
[System.Serializable]
public class PersonDataList
{
    public List<PersonData> PersonData;
}
