using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}