using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    [SerializeField] private float FOVAngle;
    [SerializeField] private float smoothDamp;
    private Vector3 currentVelocity;
    private float speed;

    private List<FlockUnit> cohesionNeighbors = new List<FlockUnit>();
    private Flock assignedFlock;

    public Transform myTransform { get; set; }
    public bool FishActive { get; set; }
    [SerializeField] private GameObject fishModel;

    private void Awake()
    {
        myTransform = transform;
        FishActive = true;
    }

    public void DisableFish()
    {
        FishActive = false;
        fishModel.SetActive(false);
    }

    public void EnableFish()
    {
        FishActive = true;
        fishModel.SetActive(true);
    }

    public void AssignFlock(Flock flock)
    {
        assignedFlock = flock;
    }

    public void InitializeSpeed(float speed)
    {
        this.speed = speed;
    }
    public void MoveUnit()
    {
        FindNeighbors();
        Vector3 cohesionVec = CalculateCohesionVector();
        Vector3 moveVec = Vector3.SmoothDamp(myTransform.forward, cohesionVec, ref currentVelocity, smoothDamp);
        moveVec = moveVec.normalized * speed;
        myTransform.forward = moveVec;
        myTransform.position += moveVec * Time.deltaTime;
    }

    private void FindNeighbors()
    {
        cohesionNeighbors.Clear();
        var allUnits = assignedFlock.allUnits;
        for (int i = 0; i < allUnits.Length; i++)
        {
            var currentUnit = allUnits[i];
            if (currentUnit != this)
            {
                float dist = Vector3.SqrMagnitude(currentUnit.myTransform.position - myTransform.position);
                if (dist <= assignedFlock.cohesionDist * assignedFlock.cohesionDist)
                {
                    cohesionNeighbors.Add(currentUnit);
                }
            }
        }
    }

    private Vector3 CalculateCohesionVector()
    {
        Vector3 cohesionVector = Vector3.zero;
        if (cohesionNeighbors.Count == 0)
        {
            return cohesionVector;
        }
        int neighborsInFOV = 0;
        for (int i = 0; i < cohesionNeighbors.Count; i++)
        {
            if (IsInFOV(cohesionNeighbors[i].myTransform.position))
            {
                neighborsInFOV++;
                cohesionVector += cohesionNeighbors[i].myTransform.position;
            }
        }

        if (neighborsInFOV == 0)
        {
            return cohesionVector;
        }

        cohesionVector /= neighborsInFOV;
        cohesionVector -= myTransform.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    private bool IsInFOV(Vector3 pos)
    {
        return Vector3.Angle(myTransform.forward, pos - myTransform.position) <= FOVAngle;
    }
}
