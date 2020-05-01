using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public Transform Tablets;
    public string url;
    Image image;
    TextMeshProUGUI text;
    public Sprite DefaultPhoto;

    private const string DatabaseName = "data.db";

    private int tabletsCount;

    public void Start()
    {
        tabletsCount = Tablets.childCount;
        ReadDataToList();
        UpdateTablets();
    }


    public List<PersonData> personDatas;
    public void ReadDataToList()
    {
        var ds = new DataService(DatabaseName);
        var people = ds.GetPersons();
        personDatas = people.ToList();
        Debug.Log("Загружено данных всего = " + personDatas.Count);
    }
    
    public void UpdateTablets()
    {
        StartCoroutine(SetInfo());
    }

    public void OpenURL()
    {
        Debug.Log("click");
        //Application.OpenURL(PersonDataList.PersonData[0].url);
    }
    

    Texture myTexture;
    IEnumerator SetInfo()
    {
        List<int> currentPersons = new List<int>();
        int currentPerson;
        for (int i = 0; i < tabletsCount; i++)
        {
            do
            {
                currentPerson = Random.Range(0, personDatas.Count);
            } while (currentPersons.IndexOf(currentPerson) >= 0);
            currentPersons.Add(currentPerson);
        }
        int id = 0;
        foreach (Transform Tablet in Tablets)
        {
            var person = personDatas[currentPersons[id]];

            Sprite photo;
            if (person.photo.IndexOf("svg") > 0) photo = DefaultPhoto;
            else
            {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(person.photo);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    photo = DefaultPhoto;
                }
                else
                {
                    myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    photo = Sprite.Create((Texture2D)myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);
                }
            }

            Tablet.GetChild(0).Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 1);
            Tablet.GetChild(0).Find("Image").GetComponent<Image>().sprite = photo;
            Tablet.GetChild(0).Find("Text").GetComponent<TextMeshProUGUI>().text = person.name;
            id++;
        }
    }
    
}
