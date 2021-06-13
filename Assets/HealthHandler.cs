using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image healthFill;

    [SerializeField] private bool useHSVColor = false;

    public delegate void SetHealthEvent(int newhealth);
    public static event SetHealthEvent HealthChanged;
    public static event SetHealthEvent SetMaxPlayerHealth;

    private int maxHealth;

    public static void InvokeHealthChanged(int health)
    {
        HealthChanged.Invoke(health);
    }

    public static void InvokeSetMaxHealth(int health)
    {
        SetMaxPlayerHealth.Invoke(health);
    }

    private void OnEnable()
    {
        HealthChanged += UpdateHealth;
        SetMaxPlayerHealth += SetMaxHealth;
    }

    private void OnDisable()
    {
        HealthChanged -= UpdateHealth;
        SetMaxPlayerHealth -= SetMaxHealth;
    }

    private void SetMaxHealth(int health)
    {
        maxHealth = health;
    }

    private void UpdateHealth(int newhealth)
    {
        healthText.text = newhealth.ToString();
        healthFill.fillAmount = (float)newhealth / maxHealth;

        if (useHSVColor)
        {
            float value = GetHSV(newhealth, maxHealth, 0, 125, 0);
            healthFill.color = Color.HSVToRGB(value / 360, 1.0f, 1.0f);
        }
    }

    private static float GetHSV(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
