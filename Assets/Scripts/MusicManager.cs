using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy any duplicates when returning to titlescreen from EndScene
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
