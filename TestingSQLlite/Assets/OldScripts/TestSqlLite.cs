
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
/*
using System;
using System.IO;
using UnityEngine.UI;

public class TestSqlLite : MonoBehaviour
{
    public SqliteConnection dbconnection;
    private string path;

    // Start is called before the first frame update
    void Start()
    {
        SetConnection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetConnection()
    {
        path = Application.streamingAssetsPath + "/data2.db";

        dbconnection = new SqliteConnection("URI=file:" + path);
        dbconnection.Open();
        if (dbconnection.State == ConnectionState.Open)
        {
            Debug.Log("Connect");
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbconnection;
            cmd.CommandText = "SELECT * FROM PersonData";
            SqliteDataReader r = cmd.ExecuteReader();
            List<Data> datas = new List<Data>();
            while (r.Read())
            {
                //Debug.Log(String.Format("{0}   {1}    {2} {3}", r[0], r[1], r[2], r[3]));
                datas.Add(new Data(
                    int.Parse(String.Format("{0}", r[0])),
                    r[1].ToString(),
                    r[2].ToString(),
                    r[3].ToString()));
            }
            Debug.Log(datas[2].name);
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public class Data
    {
        public int id;
        public string url;
        public string photo;
        public string name;

        public Data(int _id, string _url, string _photo, string _name)
        {
            id = _id; url = _url; photo = _photo; name = _name;
        }
    }
}
*/