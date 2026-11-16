using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// The "Day complete" screen: shows the saved values and offers two choices.
public class DayEndScreen : MonoBehaviour
{
    [SerializeField] private GameProgress progress;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text trustText;          // optional: demo line for the assignment
    [SerializeField] private string nextDayScene = "Tutorial";
    [SerializeField] private GameObject exitButton;      // hidden in the browser: a web page can't quit

    [Header("One of these is shown, depending on how the day ended")]
    [SerializeField] private GameObject warmVersion;     // owner brighter, warm light
    [SerializeField] private GameObject quietVersion;    // house dim and quiet

    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer && exitButton != null)
            exitButton.SetActive(false);
        title.text = $"Day {progress.DaysCompleted} complete";

        bool quiet = progress.LastDayOutcome == DayOutcome.Quiet;
        if (warmVersion != null) warmVersion.SetActive(!quiet);
        if (quietVersion != null) quietVersion.SetActive(quiet);

        if (trustText != null)
            trustText.text = $"Trust: {progress.Trust}";
    }

    // Wired to the "Begin next day" button's On Click event in the Inspector
    public void OnNextDayClicked()
    {
        SceneManager.LoadScene(nextDayScene);
    }

    // Wired to the "Exit" button
    public void OnExitClicked()
    {
        Application.Quit();                                   // does nothing inside the Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;      // so the button also stops Play mode
#endif
    }
}
