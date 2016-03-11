using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildTile : MonoBehaviour {
    
    public float m_Speed;
    public GameObject[] m_Towers;

	GameObject[] button;
	Transform[] position;
	GameObject[] towersPool;

	bool buttonOnPosition;


    void Awake()
    {
		buttonOnPosition = false;
		button = new GameObject[4];
		position = new Transform[4];
        towersPool = new GameObject[m_Towers.Length];


		for (int i = 0; i < position.Length; i++) {
			position[i] = transform.GetChild(1).GetChild(i).gameObject.transform;
		}

		for (int i = 0; i < button.Length; i++) {

			button[i] = transform.GetChild(0).GetChild(i).gameObject;
		}

        for (int i = 0; i < m_Towers.Length; i++)
        {
            towersPool[i] = Instantiate(m_Towers[i], new Vector3(0, -10, 0), m_Towers[i].transform.rotation) as GameObject;
            towersPool[i].SetActive(false);
        }


    }

	void Update(){
		//Modo provvisorio per chiudere l'interfaccia!
		if(Input.GetKeyDown(KeyCode.Space))
			StartCoroutine ("DeactivateInterface");

	}


    void OnMouseDown(){
		StartCoroutine ("ActivateInterface");
	}



    IEnumerator ActivateInterface()
    {
        
        

        while (!buttonOnPosition)
        {
            int check = 0;
            for (int i = 0; i < button.Length; i++)
            {
                
                if (i < 2)
                {
                    button[i].gameObject.SetActive(true);
                  
                    button[i].gameObject.transform.position = Vector3.Lerp(button[i].gameObject.transform.position, position[i].position , m_Speed * Time.deltaTime);
                    

                }
                else
                {
                    button[i].gameObject.SetActive(true);
                    
                    button[i].gameObject.transform.position = Vector3.Lerp(button[i].gameObject.transform.position, position[i].position, m_Speed * Time.deltaTime );
                    

                }


                if (Vector3.Distance(button[i].transform.position, position[i].position) < 0.2f)
                    check += 1;
                if (check >= 4)
                    buttonOnPosition = true;
      
                yield return null;
            }

        }
 
    }

    IEnumerator DeactivateInterface()
    {
        while (buttonOnPosition)
        {
            int check = 0;
            for (int i = 0; i < button.Length; i++)
            {

                if (i < 2)
                {
                    button[i].gameObject.transform.position = Vector3.Lerp(button[i].gameObject.transform.position, transform.position, m_Speed * Time.deltaTime );
               }
                else
                {
                    button[i].gameObject.transform.position = Vector3.Lerp(button[i].gameObject.transform.position, transform.position, m_Speed * Time.deltaTime);   
                }

                if (Vector3.Distance(button[i].transform.position, transform.position) < 0.5)
                {
                    check += 1;
                    button[i].gameObject.SetActive(false);
                }

                if(check >= 4)
                   buttonOnPosition = false;
                
                    yield return null;
            }

        }
            

    }
        
    public void BuildTower(int index)
    {
        towersPool[index].transform.position = transform.position;
        towersPool[index].SetActive(true);

    }
}
