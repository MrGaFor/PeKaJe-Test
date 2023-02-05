using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerActive : MonoBehaviour
{
    [SerializeField] private GameObject Obj;

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.GetComponent<Player>())
        {
            Obj.SetActive(true);
        }
    }

}
