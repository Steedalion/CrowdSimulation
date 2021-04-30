using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropOnClick : MonoBehaviour
{
    public GameObject prefab;
    public IFlee[] fleeingAgents;

    private void Start()
    {
        fleeingAgents = FindObjectsOfType<MonoBehaviour>().OfType<IFlee>().ToArray();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnAtClickPosition();
        }
    }

    void SpawnAtClickPosition()
    {
        Vector3 spawnPosition = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(spawnPosition);

        if (Physics.Raycast(mouseRay, out RaycastHit raycastHit))
        {
            InstantiateObject(raycastHit.point);
            InformAgents(raycastHit.point);
        }
    }

    private void InformAgents(Vector3 dangerPosition)
    {
        foreach (IFlee flee in fleeingAgents)
        {
            flee.DetectDanger(dangerPosition);
        }
    }

    private void InstantiateObject(Vector3 intersection)
    {
        Instantiate(prefab, intersection, Quaternion.identity);
    }
}