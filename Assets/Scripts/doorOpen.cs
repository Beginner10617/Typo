using UnityEngine;
using UnityEngine.InputSystem;

public class doorOpen : MonoBehaviour
{
    public Transform openPosition;     // Destination position
    public float openSpeed = 2f;
    [SerializeField] private doorOpen otherDoor; // Reference to the other door
    [SerializeField] private GameObject doorBoundary;
    [SerializeField] private bool playerInRange = false;
    private bool isOpening = false;

    [SerializeField] private AudioSource audioSource;

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition.position,
                openSpeed * Time.deltaTime
            );
        }
    }

    // Called by the Player when interact is pressed
    public void Interact()
    {
        Debug.Log("Interact called on door");
        if (playerInRange && !isOpening)
        Open();
        
        if(otherDoor != null)
        {
            otherDoor.Open();
        }
    }

    public void Open()
    {
        isOpening = true;
        if (audioSource != null)
        {
            audioSource.Play();
        }
        if (doorBoundary != null)
        {
            doorBoundary.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}