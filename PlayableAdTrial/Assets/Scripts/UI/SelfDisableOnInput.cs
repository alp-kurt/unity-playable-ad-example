using UnityEngine;
using System.Collections;

public class SelfDisableOnInput : MonoBehaviour
{
    [SerializeField] private float duration;

    private bool hasInputReceived = false; // Track input

    private void Update()
    {
        if (!hasInputReceived && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            hasInputReceived = true;
            StartCoroutine(DisableAfterDelay(duration));
        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
