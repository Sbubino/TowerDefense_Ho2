using UnityEngine;
using System.Collections;

public  class DialogoController : MonoBehaviour {

    GameObject selectedObject;
    string baseText;

   public UILabel dialog;

	// Use this for initialization
	void Start () {
        baseText = dialog.text;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void TurretInfo(Turret t)
    {
        StopAllCoroutines();

        dialog.text = t.TurretDialogo();
        StartCoroutine(Reset(5));

    }

    public void TurretUpInfo(string info)
    {
        dialog.text = info;

    }

    public void GeneralInfo(string info)
    {
        StopAllCoroutines();
        dialog.text = info;
        StartCoroutine(Reset(5));

    }

    public IEnumerator Reset(float sec)
    {
       
        yield return new WaitForSeconds(sec*Time.timeScale);
        dialog.text = baseText;

    }

    public void Reset()
    {
        dialog.text = baseText;

    }
}
