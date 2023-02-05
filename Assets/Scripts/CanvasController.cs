using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] private Player PlayerObj;
    [SerializeField] private Transform PausePanel;
    [SerializeField] private Transform WinPanel;
    [SerializeField] private Transform LosePanel;

    public void StartGame()
    {
        PlayerObj.Played = true;
        PausePanel.gameObject.SetActive(false);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WinLose(bool win)
    {
        if (win)
        {
            // WIN
            WinPanel.gameObject.SetActive(true);
        }
        else
        {
            // LOSE
            LosePanel.gameObject.SetActive(true);
        }
    }

}
