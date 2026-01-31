using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    [SerializeField] private Image oxygenFillBar;



    public void ModifyOxygenAmount(float amount)
    {
        oxygenFillBar.fillAmount = amount;
    }
}
