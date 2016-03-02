using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
    public Transform m_Tile1;
    public Transform m_Tile2;
    public bool m_UnoODue;



    public Vector3 GetNextTilePosition()
    {
        if (m_UnoODue)
            return m_Tile1.position;
        else
            return m_Tile2.position;

    }
}
