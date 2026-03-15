using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private float loadingTime = 3f; // Time to wait before loading the game scene
    [SerializeField] private GameObject loadingScreen; // Reference to the loading screen UI
    private float timer = 0f;
    void Start()
    {
        loadingScreen.SetActive(true); // Show the loading screen
    }
    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time elapsed since the last frame
        if (timer >= loadingTime)
        {
            loadingScreen.SetActive(false); // Hide the loading screen
            Destroy(gameObject); // Destroy this game object to free up resources
        }
    }
}
