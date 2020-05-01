using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class DataFromDB : MonoBehaviour
{
    public List<PersonData> datas;
    int i = 0;
    void Start()
    {
        var ds = new DataService("data2.db");
        var people = ds.GetPersons();
        datas = people.ToList();
        Debug.Log(datas[10].name);
    }
}
