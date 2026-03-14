using UnityEngine;
using UnityEngine.InputSystem;

public class doorOpen : MonoBehaviour
{
    public Transform openPosition;
    public float openSpeed = 2f;

    public float closeDelay = 5f; // time before door closes

    [SerializeField] private doorOpen otherDoor;
    [SerializeField] private GameObject doorBoundary;
    [SerializeField] private bool playerInRange = false;

    private bool isOpening = false;
    private bool isClosing = false;

    private float closeTimer;

    private Vector3 closedPosition;

    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        closedPosition = transform.position;
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

            if (Vector3.Distance(transform.position, openPosition.position) < 0.01f)
            {
                isOpening = false;
                closeTimer = Time.time + closeDelay;
            }
        }

        if (!isOpening && !isClosing && Time.time > closeTimer)
        {
            StartClosing();
        }

        if (isClosing)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                closedPosition,
                openSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, closedPosition) < 0.01f)
            {
                isClosing = false;

                if (doorBoundary != null)
                    doorBoundary.SetActive(true);
            }
        }
    }

    public void Interact()
    {
        Debug.Log("Interact called on door");

        if (playerInRange && !isOpening)
            Open();

        if (otherDoor != null)
        {
            otherDoor.Open();
        }
    }

    public void Open()
    {
        isOpening = true;
        isClosing = false;

        if (audioSource != null)
            audioSource.Play();

        if (doorBoundary != null)
            doorBoundary.SetActive(false);
    }

    void StartClosing()
    {
        isClosing = true;
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
