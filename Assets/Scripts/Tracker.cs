using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    [Header("World References")]
    public Transform player;
    public Transform trackStart;
    public Transform trackFinish;

    [Header("UI References")]
    public RectTransform trackerBar;
    public RectTransform playerDot;
    public RectTransform fillBar;

    public Image fillImage;

    public TMP_Text progressText;

    private float totalDistance;

    void Start()
    {
        // Total length of track
        totalDistance =
            Vector3.Distance(
                trackStart.position,
                trackFinish.position
            );
    }

    void Update()
    {
        // Distance from player to finish
        float distanceToFinish =
            Vector3.Distance(
                player.position,
                trackFinish.position
            );

        // Progress percentage
        float progress =
            1 - (distanceToFinish / totalDistance);

        // Keep between 0 and 1
        progress = Mathf.Clamp01(progress);

        // ======================================
        // PLAYER DOT MOVEMENT
        // ======================================

        float moveRange =
            trackerBar.rect.width -
            playerDot.rect.width;

        Vector2 targetPos =
            new Vector2(progress * moveRange, 0);

        // Smooth movement
        playerDot.anchoredPosition =
            Vector2.Lerp(
                playerDot.anchoredPosition,
                targetPos,
                Time.deltaTime * 10f
            );

        // ======================================
        // FILL BAR
        // ======================================

        fillBar.sizeDelta =
            new Vector2(
                progress * trackerBar.rect.width,
                fillBar.sizeDelta.y
            );

        // ======================================
        // PROGRESS TEXT
        // ======================================

        int percentage =
            Mathf.RoundToInt(progress * 100);

        progressText.text =
            percentage + "%";

        // ======================================
        // COLOR CHANGES
        // ======================================

        if (progress < 0.5f)
        {
            fillImage.color = Color.green;
        }
        else if (progress < 0.8f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }

        // ======================================
        // PULSING DOT EFFECT
        // ======================================

        float scale =
            1 + Mathf.Sin(Time.time * 8f) * 0.1f;

        playerDot.localScale =
            new Vector3(scale, scale, scale);
    }
}
