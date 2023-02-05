using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timer = 10f;
    private void Start()
    {
        StartCoroutine(destroi(timer));
    }
    IEnumerator destroi(float timer)
    {
        bool once = true;
        while (once)
        {
            once = false;
            yield return new WaitForSeconds(timer);
        }
        Object.DestroyImmediate(gameObject);
    }
}
