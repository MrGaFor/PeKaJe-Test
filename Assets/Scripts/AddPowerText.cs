using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddPowerText : MonoBehaviour
{

    [Header("Settings Script Animation")]
    [SerializeField] private float UpLength = 1f;
    [SerializeField] private Vector3 FinishDeviation = Vector3.one;
    [SerializeField] private float SpeedMoving = 5f;
    [SerializeField] private float TimerWait = 1f;
    [SerializeField] private Color ColorHide;
    [SerializeField] private Color ColorShow;

    private TextMeshPro TMP;

    void Start()
    {
        transform.localScale = Vector3.zero;
        TMP = GetComponent<TextMeshPro>();
        TMP.color = ColorHide;
        transform.LookAt(FindObjectOfType<Camera>().transform, Vector3.right);
        transform.Rotate(Vector3.right, 180);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        Vector3 finishPos = transform.position + new Vector3(Random.Range(-FinishDeviation.x, FinishDeviation.x), Random.Range(-FinishDeviation.y, FinishDeviation.y) + UpLength, Random.Range(-FinishDeviation.z, FinishDeviation.z));
        while (true)
        {
            TMP.color = Color.Lerp(TMP.color, ColorShow, Time.deltaTime * SpeedMoving);
            transform.position = Vector3.Lerp(transform.position, finishPos, Time.deltaTime * SpeedMoving);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * SpeedMoving);
            if (finishPos.y - transform.position.y < 0.01f)
                break;
            yield return new WaitForEndOfFrame();
        }
        float timer = TimerWait;
        while (timer > 0f)
        {
            timer = 0f;
            yield return new WaitForSeconds(TimerWait);
        }
        while (true)
        {
            TMP.color = Color.Lerp(TMP.color, ColorHide, Time.deltaTime * SpeedMoving);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.5f, Time.deltaTime * SpeedMoving);
            if (transform.localScale.y < 0.51f)
                break;
            yield return new WaitForEndOfFrame();
        }
        Object.DestroyImmediate(gameObject);
    }
}
