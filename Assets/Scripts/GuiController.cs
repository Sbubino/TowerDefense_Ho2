using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GuiController : MonoBehaviour {
	
	public Text Energy;
	public Text NextEnergyTime;
    public int NumberOfLevel;
	
	// Update is called once per frame
	void Update () {
		Energy.text = "ENERGY : " + GameController.instance.currentEnergy;
		NextEnergyTime.text = "NEXT ENERGY IN : " + GameController.instance.EnergyUpTime;	
	}

    public void level()
    {
        Application.LoadLevel(NumberOfLevel);
    }



}
