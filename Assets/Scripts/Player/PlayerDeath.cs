using UnityEngine;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] TMP_Text deathScreenTimer;
    [SerializeField] GameObject deathScreen;

    private void Start()
    {
        deathScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void onDeath()
    {
        Time.timeScale = 0;
        deathScreenTimer.text = "You survived for " + GameManager.getManager().getTimeAlive();
        deathScreen.SetActive(true);
    }
}
