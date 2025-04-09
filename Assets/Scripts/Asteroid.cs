using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int scoreValue = 10; // scoreValue variable

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Missile")) // Check if the asteroid is hit by a Missile
        {
            GameManager.Instance.AddScore(scoreValue); // Call the AddScore method from the GameManager
        }

        if (other.gameObject.CompareTag("Rocket"))
        {
            RocketHealth rocketHealth = other.gameObject.GetComponent<RocketHealth>();
            if (rocketHealth != null)
            {
                rocketHealth.TakeDamage(1);
            }
            Destroy(gameObject); // Destroy asteroid on impact
        }
    }

    // ✅ Called by homing missile
    public void TakeHit()
    {
        GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
