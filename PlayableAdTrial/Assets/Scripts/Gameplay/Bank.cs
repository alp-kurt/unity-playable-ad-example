using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    private void OnEnable()
    {
        TutorialTextController.instance.UpdateText("Setup Your Bank");
    }
}
