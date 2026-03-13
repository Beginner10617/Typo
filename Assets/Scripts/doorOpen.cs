using UnityEngine;
using UnityEngine.InputSystem;

public class doorOpen : MonoBehaviour
{
    public Transform openPosition;     // Destination position
    public float openSpeed = 2f;

    [SerializeField] private bool playerInRange = false;
    private bool isOpening = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        {
            isOpening = true;

            if (audioSource != null)
                audioSource.Play();
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