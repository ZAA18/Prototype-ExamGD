using System.Collections;
using TMPro;
using UnityEngine;

public class RaceCountdown : MonoBehaviour
{
    [Header("Countdown UI")]
    public TMP_Text countdownText;

    [Header("Countdown Settings")]
    public int countdownStart = 5;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        // ======================================
        // FREEZE ENTIRE GAME
        // ======================================

        Time.timeScale = 0f;

        int currentCount = countdownStart;

        // ======================================
        // COUNTDOWN LOOP
        // ======================================

        while (currentCount > 0)
        {
            countdownText.text =
                currentCount.ToString();

            yield return new WaitForSecondsRealtime(1f);

            currentCount--;
        }

        // ======================================
        // SHOW GO!!!
        // ======================================

        countdownText.text = "GO!!!";

        // START GAME
        Time.timeScale = 1f;

        // Wait real time
        yield return new WaitForSecondsRealtime(1f);

        // Hide countdown
        countdownText.gameObject.SetActive(false);
    }
}