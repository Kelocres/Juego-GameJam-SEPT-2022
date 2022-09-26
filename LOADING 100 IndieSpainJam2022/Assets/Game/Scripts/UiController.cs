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

    public float EnergyForSpeed { get => energyForSpeed; set => energyForSpeed = value; }

    void Start()
    {
         EnergyForSpeed = 0;
       
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
    

}
