using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player gets hit");
            other.GetComponent<Health>().TakeDamage(5);
        }
    }
}
