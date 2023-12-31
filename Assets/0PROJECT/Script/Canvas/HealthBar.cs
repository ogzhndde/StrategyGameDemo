using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image IMA_Fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetVisualProperties(Color healthBarColor)
    {
        //Set the color of the health bar changes according to the team the unit belongs to.
        IMA_Fill.color = healthBarColor;
    }
}
