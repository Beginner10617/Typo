using UnityEngine;

public class spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ThirdPersonShooterController>().TakeDamage(100);
            Debug.Log("Player hit spike!");
            Debug.Log("Game Over!");
        }
    }
}
