/*using TMPro;
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
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

        [Header("Race Position")]
        public TMP_Text positionText;

        [HideInInspector]
        public Vector2 startPosition;

        [HideInInspector]
        public float totalDistance;

        [HideInInspector]
        public float currentProgress;


    }

    [Header("All Racers")]
    public Racer[] racers = new Racer[5];

    [Header("Win System")]
    public GameObject youWinPanel;

    [Header("PLAYER POSITION UI")]
    public TMP_Text playerPositionUI;
    

    private bool raceFinished = false;

    

    void Start()
    {
        if (youWinPanel != null)
        {
            youWinPanel.SetActive(false);
        }

        foreach (Racer r in racers)
        {
            if (r.racerDot != null)
            {
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
        if (raceFinished)
            return;

        // ======================================
        // UPDATE ALL RACERS
        // ======================================

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

            r.currentProgress = progress;

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

            // ======================================
            // WIN CHECK
            // ======================================

            /*   if (progress >= 1f)
               {
                   // ONLY PLAYER WINS
                   if (r == racers[0])
                   {
                       WinGame();
                   }
               }
            */

            float finishDistance =
    Vector3.Distance(
        r.racer.position,
        r.trackFinish.position
    );

            if (finishDistance < 3f)
            {
                // ONLY PLAYER WINS
                if (r == racers[0])
                {
                    WinGame();
                }
            }
        }

        // ======================================
        // UPDATE POSITIONS
        // ======================================

        UpdateRacePositions();
    }

    /* void UpdateRacePositions()
     {
         // Sort racers by progress
         Racer[] sortedRacers =
             racers.OrderByDescending(
                 x => x.currentProgress
             ).ToArray();

         // Assign positions
         for (int i = 0; i < sortedRacers.Length; i++)
         {
             int position = i + 1;

             if (sortedRacers[i].positionText != null)
             {
                 sortedRacers[i].positionText.text =
                     GetPositionText(position);
             }
         }
     }*/

   void UpdateRacePositions()
    {
        // Sort racers by progress
        Racer[] sortedRacers =
            racers.OrderByDescending(
                x => x.currentProgress
            ).ToArray();

        // Assign positions
        for (int i = 0; i < sortedRacers.Length; i++)
        {
            int position = i + 1;

            string positionString =
                GetPositionText(position);

            // Update each racer's own UI
            if (sortedRacers[i].positionText != null)
            {
                sortedRacers[i].positionText.text =
                    positionString;
            }

            // ======================================
            // PLAYER POSITION UI
            // ======================================

            // racers[0] = PLAYER
            if (sortedRacers[i] == racers[0])
            {
                if (playerPositionUI != null)
                {
                    playerPositionUI.text =
                        positionString;
                }
            }
        }
    }



    string GetPositionText(int position)
    {
        switch (position)
        {
            case 1:
                return "1st";

            case 2:
                return "2nd";

            case 3:
                return "3rd";

            default:
                return position + "th";
        }
    }

    void WinGame()
    {
        raceFinished = true;

        // Stop time
        Time.timeScale = 0f;

        // Show panel
        if (youWinPanel != null)
        {
            youWinPanel.SetActive(true);
        }

        Debug.Log("YOU WIN!");
    }
}