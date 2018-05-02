using UnityEngine;
using UnityEngine.UI;

public class TargetMonester : MonoBehaviour
{
    RangedTargetScript parentTarget;

    private void Awake() 
    {
        parentTarget = transform.GetComponentInParent<RangedTargetScript>();
    }
    public void isHited(float damage)
    {
        parentTarget.TakeDamage(damage);
        Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

}
