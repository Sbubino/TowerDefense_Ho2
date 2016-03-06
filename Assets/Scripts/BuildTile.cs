using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildTile : MonoBehaviour {
	public Canvas m_Interface;

	void OnMouseDown()
	{
		m_Interface.gameObject.SetActive (true);
	}

}
