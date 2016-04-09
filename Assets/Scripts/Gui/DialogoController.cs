using UnityEngine;
using System.Collections;

public  class DialogoController : MonoBehaviour {

    GameObject selectedObject;
    string baseText;
    bool onSwitch;
    bool offSwitch;
   public UILabel dialog;

	// Use this for initialization
	void Start () {
        baseText = dialog.text;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void SwitchInfo(string info, bool on)
    {
        if (!offSwitch)
        {
           
            if (on)
            {
                dialog.text = info;
                onSwitch = true;
                Debug.Log("on");
            }
            else if (onSwitch)
            {
                Reset();
                onSwitch = false;
            }
        }

    }

    public void LockSwitch(string info)
    {
        StopAllCoroutines();
        offSwitch = true;
        dialog.text = info;
        StartCoroutine(SwitchUnlock());
    }

    IEnumerator SwitchUnlock()
    {
        yield return new WaitForSeconds(2*Time.timeScale);
        offSwitch = false;
    }

    public void TurretInfo(Turret t)
    {
        //  StopAllCoroutines();
        StopAllCoroutines();

        dialog.text = t.TurretDialogo();
//        StartCoroutine(Reset(5));

    }

    public void TurretUpInfo(string info)
    {
        StopAllCoroutines();

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
