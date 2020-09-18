using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class NavMeshAgentController : NetworkBehaviour
{
    NavMeshAgent vehicle;
    Vector3 objective;
    BoxCollider myCollider;

    // Start is called before the first frame update

    [Server]
    void Start()
    {
        vehicle = GetComponent<NavMeshAgent>();
        objective = GetRandomGameBoardLocation();
        myCollider = GetComponent<BoxCollider>();
        GenerateNewObjective();
    }

    // Update is called once per frame
    [Server]
    void Update()
    {
       if (vehicle.pathStatus == NavMeshPathStatus.PathComplete && vehicle.remainingDistance == 0)
       {
            GenerateNewObjective();
       }
    }

    [Server]
    private void GenerateNewObjective()
    {
        objective = GetRandomGameBoardLocation();
        vehicle.destination = objective;
    }

    private Vector3 GetRandomGameBoardLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int maxIndices = navMeshData.indices.Length - 3;

        // pick the first indice of a random triangle in the nav mesh
        int firstVertexSelected = UnityEngine.Random.Range(0, maxIndices);
        int secondVertexSelected = UnityEngine.Random.Range(0, maxIndices);

        // spawn on verticies
        Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];

        Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
        Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];

        // eliminate points that share a similar X or Z position to stop spawining in square grid line formations
        if ((int)firstVertexPosition.x == (int)secondVertexPosition.x || (int)firstVertexPosition.z == (int)secondVertexPosition.z)
        {
            point = GetRandomGameBoardLocation(); // re-roll a position - I'm not happy with this recursion it could be better
        }
        else
        {
            // select a random point on it
            point = Vector3.Lerp(firstVertexPosition, secondVertexPosition, UnityEngine.Random.Range(0.05f, 0.95f));
        }

        return point;
    }

    //[Server]
    private void OnTriggerEnter(Collider c)
    {
        float velocity = 1000.0f;
        if (c.tag == "Player")
        {
//RpcAddForce(c.gameObject, velocity);
            c.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000f, ForceMode.VelocityChange);
        }
    }

    //[ClientRpc]
    private void RpcAddForce(GameObject go, float force)
    {
        go.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.VelocityChange);
    }

    //[Server]
    void OnTriggerExit(Collider c)
    {
        RpcAddForce(c.gameObject, 1000f);
    }
}
