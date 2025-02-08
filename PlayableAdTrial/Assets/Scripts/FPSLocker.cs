using UnityEngine;

public class FPSLocker : MonoBehaviour
{
    [SerializeField]
    private int frameRate = 30;
    void Start()
    {
        Application.targetFrameRate = frameRate;
    }
}