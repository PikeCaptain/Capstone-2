using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    
    private Slider healthSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.value = health.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != health.currentHealth)
        {
            healthSlider.value = health.currentHealth;
        }
    }
}
