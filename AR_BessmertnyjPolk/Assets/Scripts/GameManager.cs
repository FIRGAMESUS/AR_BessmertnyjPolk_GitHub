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

    private string url;
    private int tabletsCount;

    private const string DatabaseName = "data.db";

    public void Start()
    {
        tabletsCount = Tablets.childCount;
        ReadDataToList();
        UpdateTablets();
    }


    public List<PersonData> PersonDataList;
    public void ReadDataToList()
    {
        var ds = new DataService(DatabaseName);
        var people = ds.GetPersons();
        PersonDataList = people.ToList();
        Debug.Log("Загружено данных всего = " + PersonDataList.Count);
    }
    
    public void UpdateTablets()
    {
        SendDataToTablet();
    }

    public void SendDataToTablet()
    {
        List<int> currentPersons = new List<int>();
        int currentPerson;
        for (int i = 0; i < tabletsCount; i++)
        {
            do
            {
                currentPerson = Random.Range(0, PersonDataList.Count);
            } while (currentPersons.IndexOf(currentPerson) >= 0);
            currentPersons.Add(currentPerson);
        }
        int id = 0;
        foreach (Transform Tablet in Tablets)
        {
            var person = PersonDataList[currentPersons[id]];
            Tablet.GetComponent<TabletViewController>().SetData(person);
            id++;
        }
    }

}
