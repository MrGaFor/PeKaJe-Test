using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Info")]
    [SerializeField] private int Power = 10000;
    [SerializeField] private int MinPower = 100;
    [SerializeField] private float SendingRepeating = 0.1f;
    [SerializeField] private int SendingPower = 2;
    [SerializeField] private float SpeedMoving = 2f;
    [SerializeField] private float SpeedRunning = 4f;
    [SerializeField] private float AddPowerRunTimer = 1f;
    [SerializeField] private int AddPowerRunPower = 100;

    [SerializeField] private Transform FireBallPrefab;
    FireBall lastBall;
    [SerializeField] private Transform FireBallPosition;
    [SerializeField] private Transform ModelScaling;
    [SerializeField] private Transform RoadObject;
    [SerializeField] private ParticleSystem ChargingParticle;
    [SerializeField] private TMPro.TextMeshPro PowerCountText;
    private SphereCollider PCollider;
    [SerializeField] private RoadLine Road;

    public bool Played = false;

    private void Start()
    {
        PCollider = GetComponent<SphereCollider>();
        PowerCountText.text = Power.ToString();
    }

    private void FixedUpdate()
    {
        if (Played)
        {
            ModelScaling.localScale = Vector3.Lerp(ModelScaling.localScale, new Vector3(Power, Power, Power) * 0.0001f, Time.deltaTime * 5f);
            RoadObject.localScale = new Vector3(RoadObject.localScale.x, RoadObject.localScale.y, ModelScaling.localScale.z);
            transform.Translate(new Vector3(0.1f * Time.deltaTime * -(Running ? SpeedRunning : SpeedMoving), 0f, 0f));
            PCollider.radius = ModelScaling.localScale.x * 0.5f;
            PowerCountText.text = Power.ToString();
        }
    }

        Coroutine AddPowerTimerCoroutine;
    IEnumerator AddPowerTimer()
    {
        bool once = true;
        while (once)
        {
            once = false;
            yield return new WaitForSeconds(AddPowerRunTimer);
        }
        while (true)
        {
            AddPower(AddPowerRunPower);
            yield return new WaitForSeconds(AddPowerRunTimer);
        }
    }

    public void StartCreateFireBall()
    {
        lastBall = Instantiate<Transform>(FireBallPrefab, FireBallPosition.position, Quaternion.identity, null).GetComponent<FireBall>();
        sending = StartCoroutine(Sending());
    }

    public void StopSending()
    {
        if (sending != null)
            StopCoroutine(sending);
        if (lastBall)
            lastBall.StartFlying(Road.GetNearObstacleDirection(lastBall.transform.position).normalized, Road);
    }

    Coroutine sending;
    IEnumerator Sending()
    {
        while (true)
        {
            if (Power <= MinPower)
            {
                Lose();
                StopSending();
                break;
            }
            Power -= SendingPower;
            lastBall.AddSize(SendingPower);
            ChargingParticle.Play();
            yield return new WaitForSeconds(SendingRepeating);
        }
    }
    public void AddPower(int count)
    {
        Power += count;
    }
    private void Lose()
    {
        Played = false;
        FindObjectOfType<CanvasController>().WinLose(false);
    }
    public void Win()
    {
        Played = false;
        FindObjectOfType<CanvasController>().WinLose(true);
    }
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.GetComponent<ObstacleObject>())
        {
            Lose();
        }
    }

    public void SetDefaultClick(bool value)
    {
        if (Played)
        {
            if (value)
            {
                StartCreateFireBall();
            }
            else
            {
                StopSending();
            }
        }
    }

    private bool Running = false;
    public void SetRunning(bool value)
    {
        Running = value;
        if (Running)
        {
            AddPowerTimerCoroutine = StartCoroutine(AddPowerTimer());
        }
        else if (!Running && AddPowerTimerCoroutine != null)
        {
            StopCoroutine(AddPowerTimerCoroutine);
            AddPowerTimerCoroutine = null;
        }

    }

}
