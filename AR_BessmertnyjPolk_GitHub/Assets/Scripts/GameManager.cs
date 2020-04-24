using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Tablet;
    public string url;
    Sprite photo;

    void Start()
    {
        Debug.Log("Started");
        url = "https://cdn.moypolk.ru/static/resize/w390/soldiers/photo/2020/04/24/2bb3b6945058ac76fc4a9c0667d37f0f.jpeg";
        StartCoroutine(GetTexture(url));
        //StartCoroutine(TextureToSprite());


        //Tablet.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Photo/TestPhoto");
        //Tablet.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = photo;
    }


    void Update()
    {
        
    }

    Texture myTexture;
    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        Debug.Log("Loaded");
        photo = Sprite.Create((Texture2D)myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);
        Tablet.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = photo;
    }


    private class PersonData
    {
        public string url;
        public string name;
    }
}
