using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallZone : MonoBehaviour
{

    public List<ObstacleObject> ObjectsInTrigger;

    public void DESTROY(RoadLine Road)
    {
        if (ObjectsInTrigger.Count > 0)
            for (int i = 0; i < ObjectsInTrigger.Count; i++)
            {
                ObjectsInTrigger[i].Curse();
                Road.RemoveObjFromList(ObjectsInTrigger[i]);
            }
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.GetComponent<ObstacleObject>())
        {
            ObjectsInTrigger.Add(obj.gameObject.GetComponent<ObstacleObject>());
        }
    }

}
