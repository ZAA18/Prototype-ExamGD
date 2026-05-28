/*using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    [Header("World References")]
    public Transform player;
    public Transform AI1;
    public Transform AI2;
    public Transform AI3;
    public Transform AI4;

    [Header ("Player 1")]
    public Transform trackStart;
    public Transform trackFinish;

    [Header("AI1")]
    public Transform trackStartAI1;
    public Transform trackFinishAI1;

    [Header("AI2")]
    public Transform trackStartAI2;
    public Transform trackFinishAI2;

    [Header("AI3")]
    public Transform trackStartAI3;
    public Transform trackFinishAI3;

    [Header("AI4")]
    public Transform trackStartAI4;
    public Transform trackFinishAI4;


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
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    [System.Serializable]
    public class Racer
    {
        [Header("World References")]
        public Transform racer;

        [Header("Track References")]
        public Transform trackStart;
        public Transform trackFinish;

        [Header("UI References")]
        public RectTransform trackerBar;

        public RectTransform racerDot;

        public RectTransform fillBar;

        public Image fillImage;

        public TMP_Text progressText;

        [HideInInspector]
        public Vector2 startPosition;

        [HideInInspector]
        public float totalDistance;
    }

    [Header("All Racers")]
    public Racer[] racers = new Racer[5];

    void Start()
    {
        foreach (Racer r in racers)
        {
            if (r.racerDot != null)
            {
                // Save where YOU placed the marker
                r.startPosition =
                    r.racerDot.anchoredPosition;
            }

            if (r.trackStart != null &&
                r.trackFinish != null)
            {
                r.totalDistance =
                    Vector3.Distance(
                        r.trackStart.position,
                        r.trackFinish.position
                    );
            }
        }
    }

    void Update()
    {
        foreach (Racer r in racers)
        {
            if (r.racer == null ||
                r.racerDot == null ||
                r.trackFinish == null ||
                r.trackerBar == null)
                continue;

            // ======================================
            // PROGRESS
            // ======================================

            float distanceToFinish =
                Vector3.Distance(
                    r.racer.position,
                    r.trackFinish.position
                );

            float progress =
                1 - (distanceToFinish / r.totalDistance);

            progress = Mathf.Clamp01(progress);

            // ======================================
            // DOT MOVEMENT
            // ======================================

            float moveRange =
                r.trackerBar.rect.width -
                r.racerDot.rect.width;

            float newX =
                r.startPosition.x +
                (progress * moveRange);

            Vector2 targetPos =
                new Vector2(
                    newX,
                    r.startPosition.y
                );

            r.racerDot.anchoredPosition =
                Vector2.Lerp(
                    r.racerDot.anchoredPosition,
                    targetPos,
                    Time.deltaTime * 10f
                );

            // ======================================
            // FILL BAR
            // ======================================

            if (r.fillBar != null)
            {
                r.fillBar.sizeDelta =
                    new Vector2(
                        progress * r.trackerBar.rect.width,
                        r.fillBar.sizeDelta.y
                    );
            }

            // ======================================
            // PROGRESS TEXT
            // ======================================

            if (r.progressText != null)
            {
                int percentage =
                    Mathf.RoundToInt(progress * 100);

                r.progressText.text =
                    percentage + "%";
            }

            // ======================================
            // COLOR CHANGES
            // ======================================

            if (r.fillImage != null)
            {
                if (progress < 0.5f)
                {
                    r.fillImage.color = Color.green;
                }
                else if (progress < 0.8f)
                {
                    r.fillImage.color = Color.yellow;
                }
                else
                {
                    r.fillImage.color = Color.red;
                }
            }

            // ======================================
            // PULSE EFFECT
            // ======================================

            float scale =
                1 + Mathf.Sin(Time.time * 8f) * 0.1f;

            r.racerDot.localScale =
                new Vector3(scale, scale, scale);
        }
    }
}
