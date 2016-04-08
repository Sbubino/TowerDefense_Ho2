using UnityEngine;
using System.Collections;

public class Switch : Tile {
    public bool m_Path1;
    public float switchEnergyCost;
    public bool m_Rot1neg;
    public bool m_Rot2neg;


    int distanceToCore1 = -1;
    int distanceToCore2 = -1;
    GameObject path1;
    GameObject path2;

    GameObject sprite;
    float timer;
    float timerSwitch;



    public override void Awake()
    {
        base.Awake();
        sprite = transform.GetChild(0).gameObject;
        timerSwitch = 0.6f;
    }

    void Update()
    {
        timerSwitch += Time.deltaTime;
    }


    public IEnumerator SetDistanceCoreSwitch(GameObject tile)
    {
        yield return new WaitForEndOfFrame();
     
        if (distanceToCore1 == -1)
        {

            distanceToCore1 = tile.GetComponent<Tile>().GetDistanceToCore() + 1;

            path1 = tile;
        }
        else if (distanceToCore2 == -1)
        {


            distanceToCore2 = tile.GetComponent<Tile>().GetDistanceToCore() + 1;

            path2 = tile;
        }

        if (path1 != null && path2 != null)
        {


            if (m_Path1)
            {
                distanceToCore = distanceToCore1;
                nextTile = path1;
            }
            else {
                distanceToCore = distanceToCore2;
                nextTile = path2;
            }



            for (int i = 0; i < positionNextTile.Length; i++)
            {
                if (positionNextTile[i] != null && !positionNextTile[i].transform.name.Equals(path1.transform.name) && !positionNextTile[i].transform.name.Equals(path2.transform.name))
                {
                    checkDistance = true;
                    //Debug.Log("tile problema " + positionNextTile[i] + "Path1 : " + path1 + "Path 2 : " + path2);

                    positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore);
                }

            }

        }
        RotateSprite();
    }


    public void SwitchChange()
    {

        if (timerSwitch >= 0.5f)
        {
            GameController.instance.LoseEnergy(switchEnergyCost);
            m_Path1 = !m_Path1;

            if (m_Path1)
            {
                distanceToCore = distanceToCore1;
                nextTile = path1;
            }
            else {
                distanceToCore = distanceToCore2;
                nextTile = path2;
            }



            for (int i = 0; i < positionNextTile.Length; i++)
            {
                if (positionNextTile[i] != null && positionNextTile[i].transform.position != path1.transform.position && positionNextTile[i].transform.position != path2.transform.position)
                {
                    checkDistance = true;

                    Debug.Log("tile problema Switch " + positionNextTile[i]);
                    positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore);
                }

            }

            RotateSprite();
            timerSwitch = 0;
        }
    }

    public void RotateSprite()
    {


        if (m_Path1 && path1 != null)
        {

            Vector2 vectorToTarget = path1.transform.position - sprite.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            if(m_Rot1neg)
                sprite.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            else
                sprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else if (path2 != null)
        {

            Vector2 vectorToTarget = path2.transform.position - sprite.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Debug.Log(angle);
            if (m_Rot2neg)
                sprite.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            else
                sprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
    }


    
}
