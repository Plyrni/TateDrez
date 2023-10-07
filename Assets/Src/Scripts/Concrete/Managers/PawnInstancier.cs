using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PawnInstancier : MonoBehaviour
{
    static public PawnInstancier Instance;

    [SerializeField] BasePawn prefabKnight;
    [SerializeField] BasePawn prefabRook;
    [SerializeField] BasePawn prefabBishop;

    private void Awake()
    {
        Instance = this;
    }

    // Bad open/close, will be replaced by a scriptableObject that store the data
    public BasePawn GetPrefab(ePawnType pawnType)
    {
        switch (pawnType)
        {
            case ePawnType.Knight:
                return prefabKnight;
                //break;
            case ePawnType.Rook:
                return prefabRook;
                //break;
            case ePawnType.Bishop:
                return prefabBishop;
                //break;
            default:
                break;
        }

        Debug.LogWarning("Cant find this type of pawn : " + pawnType);
        return null;
    }

}
