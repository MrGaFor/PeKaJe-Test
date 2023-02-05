using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLine : MonoBehaviour
{

    [SerializeField] private List<ObstacleObject> ObstaclesOnLine;

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.GetComponent<ObstacleObject>())
        {
            ObstaclesOnLine.Add(obj.GetComponent<ObstacleObject>());
            obj.GetComponent<ObstacleObject>().WarningObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.GetComponent<ObstacleObject>())
        {
            ObstaclesOnLine.Remove(obj.GetComponent<ObstacleObject>());
            obj.GetComponent<ObstacleObject>().WarningObject.SetActive(false);
        }
    }

    public void RemoveObjFromList(ObstacleObject obj)
    {
        ObstaclesOnLine.Remove(obj);
        obj.WarningObject.SetActive(false);
    }

    public Vector3 GetNearObstacleDirection(Vector3 fromPos)
    {
        if (ObstaclesOnLine.Count > 0)
        {
            ObstacleObject lastObj = ObstaclesOnLine[0];
            for (int i = 0; i < ObstaclesOnLine.Count; i++)
            {
                if (Vector3.Distance(ObstaclesOnLine[i].transform.position, fromPos) < Vector3.Distance(lastObj.transform.position, fromPos))
                {
                    lastObj = ObstaclesOnLine[i];
                }
            }
            return lastObj.transform.position - fromPos;
        }
        else
            return Vector3.zero;
    }

}
