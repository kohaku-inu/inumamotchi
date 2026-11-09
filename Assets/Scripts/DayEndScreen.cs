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

    void Start()
    {
        title.text = $"Day {progress.DaysCompleted} complete";

        // The concept says no score screen, so this line is only to SHOW the saved value in class.
        // Leave the slot empty (or delete the text object) to hide it.
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
