using UnityEngine;

public class PersistAcrossScenes : MonoBehaviour
{
    private void Awake()
    {
        // Check if another instance already exists and destroy this one if so
        if (GameObject.FindObjectsOfType<PersistAcrossScenes>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // Mark this GameObject and all its children to persist across scenes
        DontDestroyOnLoad(gameObject);
    }
}
