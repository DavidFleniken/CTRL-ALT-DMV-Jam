using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerUI;
    float startTime;

    static GameManager instance;

    public enum Host
    {
        Worm,
        Cat,
        Dog,
        Child,
        Adult,
        Cop
    };

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("GameManager Already Exists");
        }
    }

    private void Awake()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        timerUI.text = getTimeAlive();
    }

    public static GameManager getManager()
    {
        return instance;
    }

    public string getTimeAlive()
    {
        float time = Time.time;
        int min = (int)((time - startTime) / 60);
        int sec = (int)((time - startTime) - min * 60);

        return min.ToString("D2") + ":" + sec.ToString("D2");
    }
}
