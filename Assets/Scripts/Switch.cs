using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
	
    public bool right;
    public bool down;
    public bool left;
    public bool up;

    public bool m_Path1;
    public bool m_Path2;
    bool m_Path3;

    GameObject sprite;
    GameObject[] position;
	Vector2 path1;
    Vector2 path2;
    Vector2 path3;
    Vector3 currentPath;


    void Awake(){
		position = new GameObject[4];
		for (int i = 0; i < position.Length; i++) {
			position[i] = transform.GetChild(1).GetChild(i).gameObject;
            position[i].SetActive(false);
		}
        sprite = transform.GetChild(0).gameObject;

    }

    void Start()
    {
        for(int i = 0; i < position.Length; i++)
        {
            if(i == 0 && right)
            {
                position[i].SetActive(true);
                AssignPath( position[i].transform.position);
            }
            else if (i == 1 && down)
            {
                position[i].SetActive(true);
                AssignPath(position[i].transform.position);
            }
            else if (i == 2 && left)
            {
                position[i].SetActive(true);
                AssignPath(position[i].transform.position);
            }
            else if (i == 3 && up)
            {
                position[i].SetActive(true);
                AssignPath(position[i].transform.position);
            }

        }

        AssignCurrentPath();
   
    }

    void OnMouseDown()
    {
        ChangePath();
    }


    void ChangePath()
    {
        if (m_Path1)
        {
            m_Path1 = false;
            m_Path2 = true;
            AssignCurrentPath();
        }
        else
        {
            m_Path2 = false;
            m_Path1 = true;
            AssignCurrentPath();
        }
    }


    public void RotateSprite()
    {

        Vector2 vectorToTarget = currentPath - sprite.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        sprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
       

    }
    
    void AssignCurrentPath()
    {
        if (m_Path1 && path1 != null)
            currentPath = path1;
        if (m_Path2 && path2 != null)
            currentPath = path2;
        if (m_Path3 && path3 != null)
            currentPath = path3;

        RotateSprite();

    }

    void AssignPath(Vector2 path)
    {
        if (path1 == Vector2.zero)
        {
            path1 = path;

        }
        else if (path2 == Vector2.zero)
            path2 = path;
        else
            path3 = path;


    }

	public Vector2 GetNextTilePosition(){

        return currentPath;
	}



}
