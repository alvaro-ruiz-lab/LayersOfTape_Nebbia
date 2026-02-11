using Unity.Mathematics;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    private float oxygenLevel = 1f;
    [SerializeField] private float consumptionTime;



    private void Update()
    {
        ReduceOxygenAmount();
        MainUIController.OxygenBar.ModifyOxygenAmount(oxygenLevel);
    }



    private void ReduceOxygenAmount()
    {
        oxygenLevel -= Time.deltaTime / consumptionTime;
        if (oxygenLevel <= 0f)
        {
            oxygenLevel = 0f;
            MainUIController.Instance.ShowGameOverScreen();
        }
    }



    public void RefillOxygen(float amount)
    {
        // Refill a percentage of what's missing so that it's not so easy to get 100%
        var refill = (1 - oxygenLevel) * amount / 100f;
        oxygenLevel = math.clamp(refill + oxygenLevel, refill, 1);
    }
}
