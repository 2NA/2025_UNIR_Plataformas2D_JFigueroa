using System;
using UnityEngine;

public class EnemyWithPatrol : EnemyController
{
    [Header("Patrol")]
    [SerializeField] private float distanceTolerance = 0.1f;
    [SerializeField] private Transform[] waypoints;
    private int index = 0;
    private Vector3 actualDestination;

    protected override void Awake()
    {
        base.Awake();
        
        // VALIDACIÓN waypoints
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("EnemyWithPatrol: Asigna waypoints en Inspector!");
            enabled = false;
            return;
        }
    }

    void Start()
    {
        SetNewDestination();
    }

     protected override void Update()
    {
        desiredMove = (actualDestination - transform.position).normalized;

        // Si llegó, cambiar destino
        if (Vector3.Distance(transform.position, actualDestination) < distanceTolerance)
        {
            SetNewDestination();
        }

        base.Update();
    }

    private void SetNewDestination()
    {
        index = (index + 1) % waypoints.Length;
        actualDestination = waypoints[index].position;
    }
}
