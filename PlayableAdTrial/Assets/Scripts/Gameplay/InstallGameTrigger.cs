using UnityEngine;
using Luna.Unity; 

public class InstallGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            #if UNITY_EDITOR
            Debug.Log("🛠️ Install Game Triggered!"); return;
            #endif

            Playable.InstallFullGame(); 
        }


    }
}
