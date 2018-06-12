using UnityEngine;
using UnityEngine.UI;

public class TargetMonester : MonoBehaviour
{
    public GameEvent hitE;
    public void OnHit()
    {
        gameObject.SetActive(false);
        hitE.Raise();
    }
}
