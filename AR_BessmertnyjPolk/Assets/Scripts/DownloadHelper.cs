using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DownloadHelper : MonoBehaviour
{
    public static DownloadHelper instance;
    public Sprite DefaultPhoto;

    private void Awake()
    {
        instance = this;
    }

    Texture myTexture;
    public IEnumerator GetImageByUrl(string url, Action<Sprite> callback)
    {
        Sprite downloadedSprite;
        if (url.IndexOf("svg") > 0) downloadedSprite = DefaultPhoto;
        else
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                downloadedSprite = DefaultPhoto;
            }
            else
            {
                myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                downloadedSprite = Sprite.Create((Texture2D)myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);
            }
        }
        callback(downloadedSprite);
    }
}
