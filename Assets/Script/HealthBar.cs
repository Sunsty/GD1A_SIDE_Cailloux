using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;
    private Vector3 baseScale;
    private void Start()
    {
        baseScale = new Vector3(1.35f, 0.2372647f, 1.360416f);
        SetMaxHealth();
    }
    public void SetMaxHealth()
    {
        fill.transform.localScale = baseScale;
    }

    public void SetHealth(int health)
    {
        float healthAmount = TranslateScale(health);
        fill.transform.localScale = new Vector3(healthAmount, baseScale[1], baseScale[2]);
    }
    private float TranslateScale(int health)
    {
        float healthAmount = (baseScale[0] * health) / 100 ;
        return healthAmount;
    }
}
