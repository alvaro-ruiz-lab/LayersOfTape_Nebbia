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
        oxygenLevel = amount / 100f;
    }
}
