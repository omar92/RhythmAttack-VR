using UnityEngine;
using UnityEngine.UI;

public class TargetMonester : MonoBehaviour {

    public float health = 100f;
    Image img;

    private void Start()
    {
        img = GameObject.FindObjectOfType<Image>();
        img.fillAmount = 1;
    }

    private void Update()
    {
        img.transform.position = transform.position;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        img.fillAmount -= 1 / amount;
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
