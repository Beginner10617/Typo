using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject InstructionsPanel; // Reference to the loading screen UI
    [SerializeField] private GameObject AboutPanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private int currentPage = 0;
    [SerializeField] private int totalPages = 3; // Assuming there are 3 pages in the About section
    void Start()
    {
        InstructionsPanel.SetActive(false); // Ensure the Instructions panel is hidden at the start
        AboutPanel.SetActive(false); // Ensure the About panel is hidden at the start
        MainMenuPanel.SetActive(true); // Ensure the Main Menu panel is visible at the start
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void LoadAbout()
    {
        InstructionsPanel.SetActive(false);
        AboutPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void LoadMainMenu()
    {
        InstructionsPanel.SetActive(false);
        AboutPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }   

    public void LoadInstructions()
    {
        InstructionsPanel.SetActive(true);
        AboutPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
    }
    
}
