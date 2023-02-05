using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    [SerializeField] private int ObstacleVolume = 12;
    [SerializeField] private Vector3 Deviation;
    [SerializeField] private Vector3 DeviationRotation;
    [Range(0f, 1f)]
    [SerializeField] private float Chance = 0.5f;
    [SerializeField] private List<Transform> Prefabs;

    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        float xmin = GetComponent<BoxCollider>().bounds.min.x;
        float zmin = GetComponent<BoxCollider>().bounds.min.z;
        float xmax = GetComponent<BoxCollider>().bounds.max.x;
        float zmax = GetComponent<BoxCollider>().bounds.max.z;

        for (int i = 0; i < ObstacleVolume; i++)
        {
            for (int j = 0; j < ObstacleVolume; j++)
            {
                if (Random.Range(0f, 1f) < Chance)
                {
                    Vector3 pos = new Vector3(Mathf.Lerp(xmin, xmax, (float)i / (float)ObstacleVolume), 0, Mathf.Lerp(zmin, zmax, (float)j / (float)ObstacleVolume));
                    pos += new Vector3(Random.Range(-Deviation.x, Deviation.x), 0f, Random.Range(-Deviation.z, Deviation.z));
                    pos.y = transform.position.y;
                    Quaternion rot = Quaternion.Euler(Random.Range(-DeviationRotation.x, DeviationRotation.x), Random.Range(-DeviationRotation.y, DeviationRotation.y), Random.Range(-DeviationRotation.z, DeviationRotation.z));
                    Transform obj = Instantiate<Transform>(Prefabs[Random.Range(0, Prefabs.Count)], pos, rot, transform.parent);
                }
            }
        }
        Component.DestroyImmediate(this);
    }

#if UNITY_EDITOR

#endif
}
