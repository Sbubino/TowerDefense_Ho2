using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildTile : MonoBehaviour {
    public Button[] m_Button;
    public float m_Speed;
    

    void OnMouseDown()
    {
        StartCoroutine("ActivateInterface");
    }






    IEnumerator ActivateInterface()
    {
        int deltaPosition = 2;
        bool onPosition = false;
        Vector3 destination;

        while (!onPosition)
        {
            for (int i = 0; i < m_Button.Length; i++)
            {

                if (i < 2)
                {
                    m_Button[i].gameObject.SetActive(true);
                    destination = new Vector3(transform.position.x + deltaPosition, transform.position.y, 0);
                    m_Button[i].gameObject.transform.position = Vector3.Lerp(m_Button[i].gameObject.transform.position, destination , m_Speed/ Vector3.Distance(m_Button[i].transform.position, destination));
                    deltaPosition *= -1;

                }
                else
                {
                    m_Button[i].gameObject.SetActive(true);
                    destination = new Vector3(transform.position.x, transform.position.y + deltaPosition, 0);
                    m_Button[i].gameObject.transform.position = Vector3.Lerp(m_Button[i].gameObject.transform.position, destination, m_Speed  / Vector3.Distance(m_Button[i].transform.position, destination));
                    deltaPosition *= -1;

                }

                if (Vector3.Distance(m_Button[i].transform.position, destination) < 0.5f)
                    onPosition = true;


                yield return null;
            }
        }

        
    }
   

}
