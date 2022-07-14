using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [SerializeField] private FlockUnit flockUnitPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBounds;
    [SerializeField] private Vector3 moveBounds;

    [SerializeField] private float _cohesionDist;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private GameObject waterPlane;
    public float cohesionDist
    {
        get { return _cohesionDist; }
    }

    
    
    public FlockUnit[] allUnits { get; set; }

    private void Start()
    {
        GenerateUnits();
    }

    private void Update()
    {
        for (int i = 0; i < allUnits.Length; i++)
        {
            allUnits[i].MoveUnit();
            var waterPlaneY = waterPlane.transform.position.y;
            if (allUnits[i].myTransform.position.y > waterPlaneY && allUnits[i].FishActive)
            {
                allUnits[i].DisableFish();
            }
            else if (allUnits[i].myTransform.position.y < waterPlaneY && !allUnits[i].FishActive)
            {
                allUnits[i].EnableFish();
            }
        }
    }

    private void GenerateUnits()
    {
        allUnits = new FlockUnit[flockSize];
        for (int i = 0; i < flockSize; i++)
        {
            Vector3 randomVec = UnityEngine.Random.insideUnitSphere;
            randomVec = new Vector3(randomVec.x * spawnBounds.x, randomVec.y * spawnBounds.y,
                randomVec.z * spawnBounds.z);
            Vector3 spawnPos = transform.position + randomVec;
            Quaternion spawnRot = Quaternion.Euler(0, UnityEngine.Random.Range(0,360), 0);
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPos, spawnRot);
            allUnits[i].AssignFlock(this);
            allUnits[i].InitializeSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
        }
    }
}