using UnityEngine;
using Luna.Unity; 

public class InstallGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("📲 Player entered trigger zone! Opening Install Full Game...");
            Playable.InstallFullGame(); 
        }
    }
}
