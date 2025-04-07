using UnityEngine;
using UnityEngine.UI;

public class RocketHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject explosionEffect;
    public GameObject hitEffect;

    [Header("UI")]
    public Slider healthSlider; // 🔴 Reference to the health bar slider

    private void Start()
    {
        currentHealth = maxHealth;

        // Set the max value of the UI slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
        }

        // Update UI
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
        Debug.Log("GAME OVER!");
    }
}