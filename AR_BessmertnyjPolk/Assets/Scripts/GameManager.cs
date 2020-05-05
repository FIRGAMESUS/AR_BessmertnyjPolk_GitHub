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

    private int tabletsCount;

    private const string DatabaseName = "data.db";

    public Slider slider;
    public int sliderValue;

    private void Start()
    {
        tabletsCount = Tablets.childCount;
        Debug.Log(tabletsCount);
        slider.maxValue = tabletsCount;
        ReadDataToList();
        UpdateTablets();

    }

    private void Update()
    {
        slider.value = sliderValue;
        if (slider.value <= 0 || slider.value >= tabletsCount) slider.gameObject.SetActive(false);
        else slider.gameObject.SetActive(true);
    }

    public List<PersonData> PersonDataList;
    private void ReadDataToList()
    {
        var ds = new DataService(DatabaseName);
        var people = ds.GetPersons();
        PersonDataList = people.ToList();
        Debug.Log("Загружено данных всего = " + PersonDataList.Count);
    }
    
    public void UpdateTablets()
    {
        sliderValue = 0;
        SendDataToTablet();
    }

    private void SendDataToTablet()
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
