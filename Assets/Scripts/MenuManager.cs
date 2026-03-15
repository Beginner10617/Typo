using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject AboutPanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private int currentPage = 0;
    [SerializeField] private int totalPages = 3; // Assuming there are 3 pages in the About section
    void Start()
    {
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
        AboutPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void LoadMainMenu()
    {
        AboutPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }   
    public void loadNextPageAbout()
    {
        AboutPanel.transform.GetChild(currentPage).gameObject.SetActive(false);
        currentPage++;
        if (currentPage > totalPages - 1)
        {
            currentPage = 0;
        }
        AboutPanel.transform.GetChild(currentPage).gameObject.SetActive(true);
    }
}
