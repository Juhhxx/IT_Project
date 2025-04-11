using System;
using UnityEngine;

public class ObjectiveSquare : Square, IComparable<ObjectiveSquare>
{
    private PlayerChoose _player;
    private Renderer _cosmetic;

    public float DistanceFromPlayer()
    {
        Vector2 disVec = transform.position - _player.CurrentSquare.transform.position;
        return disVec.sqrMagnitude;
    }

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerChoose>();
    }

    public void ActivateDebug()
    {
        _cosmetic.material.color = Color.red;
    }


    public int CompareTo(ObjectiveSquare other)
    {
        if ( DistanceFromPlayer() == other.DistanceFromPlayer() )
            return 0;

        if ( DistanceFromPlayer() < other.DistanceFromPlayer() )
            return -1;
        
        return 1;
    }
}
