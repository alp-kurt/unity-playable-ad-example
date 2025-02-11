using UnityEngine;
using System.Collections;

public class SelfDisable : MonoBehaviour
{
public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
