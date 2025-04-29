using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public UnityEvent onPlayerDeath;      // Call game over, respawn, etc.
    public UnityEvent<float> onHealthChanged; // For UI updates

    public GameOver gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged.Invoke(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        onHealthChanged.Invoke(currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        onHealthChanged.Invoke(currentHealth);
    }

    void Die()
    {
        gameOver.ShowGameOver();
        onPlayerDeath.Invoke();
        // Add extra death logic like disabling movement, triggering game over UI, etc.
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}
