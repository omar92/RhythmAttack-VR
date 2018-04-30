using UnityEngine;
using UnityEngine.UI;

public class TargetMonester : MonoBehaviour {

    public float health = 100f;
    Scrollbar bar;

    private void Start()
    {
        bar = GameObject.FindObjectOfType<Scrollbar>();
        bar.size = 1;
    }

    private void Update()
    {
        bar.transform.position = transform.position;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        bar.size -= 1 / amount;
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
