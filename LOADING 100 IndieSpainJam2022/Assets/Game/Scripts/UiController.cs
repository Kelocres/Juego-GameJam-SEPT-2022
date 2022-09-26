using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    // Start is called before the first frame update
    // private Image bar_powerRed;
    private float energyForSpeed;
    private float energyForProjectile;
    public float EnergyForSpeed { get => energyForSpeed; set => energyForSpeed = value; }
    public float EnergyForProjectile { get => energyForProjectile; set => energyForProjectile = value; }

    void Start()
    {
         EnergyForSpeed = 0;
         EnergyForProjectile = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool fillBarActivatePower(Image bar_power)
    {

        EnergyForSpeed = Mathf.Clamp(EnergyForSpeed, 0, 100);
        EnergyForSpeed += 20; 
        bar_power.fillAmount = EnergyForSpeed / 100;
        
        if(bar_power.fillAmount == 1)
        {
            return false;
        }
        else
        {
            return true;
        } 
        
    }
    public bool fillBarActivatePowerProjectile(Image bar_power)
    {

        EnergyForProjectile = Mathf.Clamp(EnergyForSpeed, 0, 100);
        EnergyForProjectile += 10;
        bar_power.fillAmount = EnergyForProjectile / 100;

        if (bar_power.fillAmount == 1)
        {
            return false;
        }
        else
        {
            return true;
        }

    }


}