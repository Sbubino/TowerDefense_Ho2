using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {
	
	public Text Energy;
	public Text NextEnergyTime;

	
	// Update is called once per frame
	void Update () {
		Energy.text = "ENERGY : " + GameController.instance.currentEnergy;
		NextEnergyTime.text = "NEXT ENERGY IN : " + GameController.instance.EnergyUpTime;	
	}
}
