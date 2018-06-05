using UnityEngine;
using UnityEngine.UI;

public class TargetMonester : MonoBehaviour
{
    public void OnHit()
    {
        gameObject.SetActive(false);
    }
}
