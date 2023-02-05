using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.GetComponent<Player>())
        {
            FindObjectOfType<Player>().Win();
        }
    }
}
