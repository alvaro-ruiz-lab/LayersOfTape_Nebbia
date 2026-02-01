using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    [SerializeField] private Image oxygenFillBar;

    private Color originalBlueColor = new Color (86/255f, 157/255f, 217/255f);
    private Color redColor = new Color (217/255f, 104/255f, 86/255f);



    public void ModifyOxygenAmount(float amount)
    {
        oxygenFillBar.fillAmount = amount;

        oxygenFillBar.color = amount > 0.25f ? originalBlueColor : redColor;
    }
}
