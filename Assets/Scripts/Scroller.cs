using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x = 0.1f, _y = 0f;
    private static Scroller instance;
    private Vector2 scrollOffset = Vector2.zero;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (_img != null)
        {
            scrollOffset += new Vector2(_x, _y) * Time.deltaTime;
            _img.uvRect = new Rect(scrollOffset, _img.uvRect.size);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_img == null)
        {
            GameObject newImg = GameObject.Find("BackgroundImage");
            if (newImg != null)
                _img = newImg.GetComponent<RawImage>();
        }
    }

    public void SetRawImage(RawImage newImg)
    {
        _img = newImg;
    }
}
