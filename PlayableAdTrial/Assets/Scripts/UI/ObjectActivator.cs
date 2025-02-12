using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] GameObject obj;

public void ActivateObject()
    {
        obj.SetActive(true);
    }
}
