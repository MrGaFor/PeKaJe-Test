using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    [Header("Info")]
    [SerializeField] private int Power = 5;
    [SerializeField] private int PowerMult = 2;
    [SerializeField] private float Speed = 3f;

    [Header("Visible Data")]
    [SerializeField] private float TimerCenterSmaller = 0.7f;
    [SerializeField] private float TimerZoneBigger = 0.3f;

    [Header("Objects")]
    [SerializeField] private Transform ZoneObject;
    [SerializeField] private Transform BoomEffect;

    private RoadLine Road;

    Coroutine moving;
    public void StartFlying(Vector3 direction, RoadLine road)
    {
        Road = road;
        if (direction == Vector3.zero)
            direction = new Vector3(-1f, 0f, 0f);
        moving = StartCoroutine(Moving(direction));
        GetComponent<Collider>().enabled = true;
    }

    public void AddSize(int count)
    {
        Power += count * PowerMult;
        if (sizeChanging == null)
            sizeChanging = StartCoroutine(ChangeSize());
    }
    public int GetPower()
    {
        return Power;
    }

    Coroutine sizeChanging;
    IEnumerator ChangeSize()
    {
        while (true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(Power, Power, Power) * 0.0001f, Time.deltaTime * 5f);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Moving(Vector3 direction)
    {
        GetComponent<TrailRenderer>().startWidth = Power * 0.00005f;
        if (sizeChanging != null)
        {
            StopCoroutine(sizeChanging);
            sizeChanging = null;
        }
        while (true)
        {
            transform.Translate(direction * Time.deltaTime * Speed);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ZoneMoving()
    {
        ZoneObject.SetParent(null);
        float timer = TimerCenterSmaller;
        float allTimer = timer;
        Vector3 startScale = transform.localScale;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, startScale, timer / allTimer);
            yield return new WaitForEndOfFrame();
        }
        timer = TimerZoneBigger;
        allTimer = timer;
        startScale = ZoneObject.localScale;
        Vector3 zoneSize = new Vector3(Power, Power, Power) * 0.0002f;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            ZoneObject.localScale = Vector3.Lerp(zoneSize, startScale, timer / allTimer);
            yield return new WaitForEndOfFrame();
        }
        ZoneObject.GetComponent<FireBallZone>().DESTROY(Road);
        timer = TimerCenterSmaller;
        allTimer = timer;
        startScale = ZoneObject.localScale;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            ZoneObject.localScale = Vector3.Lerp(Vector3.zero, startScale, timer / allTimer);
            yield return new WaitForEndOfFrame();
        }
        ZoneObject.SetParent(transform);
        Object.DestroyImmediate(gameObject);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.GetComponent<ObstacleObject>())
        {
            StopCoroutine(moving);
            StartCoroutine(ZoneMoving());
            BoomEffect.gameObject.SetActive(true);
        }
    }

}
