using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoose : MonoBehaviour
{
    [SerializeField] private float _edgeRadius;
    [SerializeField] private LayerMask _gridLayer;
    [SerializeField] private bool _debugObjectives;
    [SerializeField] private int _choiceAmount;
    [field:SerializeField] public Square CurrentSquare { get; private set; }
    private ObjectiveSquare[] _objectives;

    private HashSet<Square> _options;

    private void OnEnable()
    {
        _options = new HashSet<Square>();

        _objectives = FindObjectsByType<ObjectiveSquare>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        if ( _debugObjectives )
            foreach ( ObjectiveSquare square in _objectives)
                square.ActivateDebug();
    }

    private void GetNextTwoNear()
    {
        Array.Sort(_objectives);
        _options.Clear();

        foreach ( ObjectiveSquare obj in _objectives )
            if ( obj.DistanceFromPlayer() < _edgeRadius )
            {
                _options.Add(obj);
                if( _options.Count >= _choiceAmount)
                    break;
            }

        while ( _options.Count < _choiceAmount )
        {
            Vector3 point = UnityEngine.Random.insideUnitCircle * _edgeRadius;
            Collider2D hit = Physics2D.OverlapCircle(point, 0.1f, _gridLayer);

            if ( hit.TryGetComponent<Square>(out Square sqr))
                _options.Add(sqr);
        }

    }
}
