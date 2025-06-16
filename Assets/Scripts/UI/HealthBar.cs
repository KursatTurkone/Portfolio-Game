
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage; 
    public void SetHealth(float healthPercent)
    {
        fillImage.fillAmount = healthPercent;
    }
}
