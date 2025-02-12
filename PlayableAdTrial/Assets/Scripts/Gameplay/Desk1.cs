using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk1 : MonoBehaviour
{
    private void OnEnable()
    {
        TutorialTextController.instance.UpdateText("Earn $1M! To Pay Back");
    }
}
