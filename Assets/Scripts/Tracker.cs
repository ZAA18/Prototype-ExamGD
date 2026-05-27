using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform trackStart;
    public Transform trackFinish;

    [Header("UI")]
    public RectTransform trackerBar;
    public RectTransform playerDot;

    private float totalDistance;

    void Start()
    {
        totalDistance =
            Vector3.Distance(trackStart.position, trackFinish.position);
    }

    void Update()
    {
        // Distance remaining
        float distanceToFinish =
            Vector3.Distance(player.position, trackFinish.position);

        // Progress
        float progress =
            1 - (distanceToFinish / totalDistance);

        progress = Mathf.Clamp01(progress);

        // Tracker width
        float barWidth = trackerBar.rect.width;

        // Dot width
        float dotWidth = playerDot.rect.width;

        // Prevent going outside
        float moveRange = barWidth - dotWidth;

        // Move dot
        playerDot.anchoredPosition =
            new Vector2(progress * moveRange, 0);
    }
}
