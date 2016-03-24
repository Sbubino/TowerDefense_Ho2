using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildTile : MonoBehaviour {
    

   public void OnClick() {
		GameController.instance.currentTile = gameObject;
		GameController.instance.turretMenu.gameObject.transform.position = gameObject.transform.position;
		GameController.instance.turretMenu.PlayForward();
		GameController.instance.openMenu = true;
	}

	public void BuildTurret(){
		Debug.Log("torre");
	}
	
}
