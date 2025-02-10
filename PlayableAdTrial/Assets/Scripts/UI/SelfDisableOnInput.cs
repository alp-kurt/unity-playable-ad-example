using UnityEngine;
using System.Collections;

public class SelfDisableOnInput : MonoBehaviour
{
    private bool hasInputReceived = false; // Track input

    private void Update()
    {
        if (!hasInputReceived && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            hasInputReceived = true;
            StartCoroutine(DisableAfterDelay(2f));
        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
