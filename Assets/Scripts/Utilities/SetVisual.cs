using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SetVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    
    public void Show(int index = 0)
    {
        if (objects.Length <= index)
        {
            Debug.LogError("Не указан объект");
            return;
        }
        
        objects[index].SetActive(true);
    }
   
    public void Hide(int index = 0)
    {
        if (objects.Length <= index)
        {
            Debug.LogError("Не указан объект");
            return;
        }
        
        objects[index].SetActive(false);
    }


    public void IfStateShow(int index)
    {
        if (objects.Length <= index)
        {
            Debug.LogError("Не указан объект");
            return;
        }

        if (objects[index].activeSelf)
            Hide(index);
        else
            Show(index);
        
        
    }
}
