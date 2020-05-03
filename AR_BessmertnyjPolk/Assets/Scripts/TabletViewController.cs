using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletViewController : MonoBehaviour
{
    private PersonData person;
    
    public void SetData(PersonData data)
    {
        person = data;
    }
}
