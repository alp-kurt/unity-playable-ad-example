using UnityEngine;
using System.Collections;

public class SelfDisableWithTimer : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(HideWindowAfterDelay());
    }

    private IEnumerator HideWindowAfterDelay()
    {
        yield return new WaitForSeconds(4f); 
        gameObject.SetActive(false); 
    }
}
