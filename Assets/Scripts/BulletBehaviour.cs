using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private ThirdPersonShooterController thirdPersonShooterController;
    [SerializeField] private string playerCatchTag;
    [SerializeField] private float speed;
    [SerializeField] private float destroyBulletAfterSeconds;
    private Rigidbody rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thirdPersonShooterController = FindObjectOfType<ThirdPersonShooterController>();
        rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyBulletAfterSeconds);
        rigidbody.linearVelocity = transform.forward * speed;
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(collision.gameObject); 
            // TODO : replace with kill animation later
        } 
        else if (collision.gameObject.tag == playerCatchTag) {
            thirdPersonShooterController.setHasBullet();
            Destroy(gameObject);
        }
        else {
            // Reflect the bullet
            Vector3 reflectDir = Vector3.Reflect(rigidbody.linearVelocity.normalized, collision.GetContact(0).normal);
            rigidbody.linearVelocity = reflectDir * speed;
        }
    }
}
