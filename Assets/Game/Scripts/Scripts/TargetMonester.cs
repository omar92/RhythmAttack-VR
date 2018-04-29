using UnityEngine;

public class TargetMonester : MonoBehaviour {

    public float health = 50f;


	public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
