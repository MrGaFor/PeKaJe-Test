using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    [Header("Set Obstacle Data")]
    [SerializeField] private Material DefaultMaterial;
    [SerializeField] private Material CursedMaterial;
    [Tooltip("Prefab text add power player after dead :D")]
    [SerializeField] private Transform TextPrefab;
    [SerializeField] private Vector2Int PowerGiving;
    // timer before start function cursed
    [SerializeField] private float TimerBeforeDestroy = 0.75f;
    // timer before start changes scaling
    [SerializeField] private float TimerBeforeChanges = 0.2f;
    [SerializeField] private float TimerDestroying = 2f;
    [Header("Objects")]
    [SerializeField] private Renderer MaterialSet;
    [SerializeField] private Transform ScalingObject;
    [SerializeField] private ParticleSystem FireParticle;
    [SerializeField] private ParticleSystem BoomParticle;
    public GameObject WarningObject;

    Material currentMat;
    private void Start()
    {
        currentMat = new Material(Shader.Find("Standard"));
        currentMat.CopyPropertiesFromMaterial(DefaultMaterial);
        MaterialSet.material = currentMat;
        //StartCoroutine(Cursedes());
    }

    private bool live = true;
    public void Curse()
    {
        if (live)
        {
            live = false;
            MaterialSet.material = CursedMaterial;
            StartCoroutine(Cursedes());
        }
    }

    IEnumerator Cursedes()
    {
        bool once = true;
        while (once)
        {
            once = false;
            yield return new WaitForSeconds(TimerBeforeDestroy);
        }
        GetComponent<Collider>().enabled = false;
        float timer = TimerDestroying;
        float allTimer = timer;
        FireParticle.gameObject.SetActive(true);

        once = true;
        while (once)
        {
            once = false;
            yield return new WaitForSeconds(TimerBeforeChanges);
        }

        TMPro.TextMeshPro textPower = Instantiate<Transform>(TextPrefab, transform.position, Quaternion.identity, null).GetComponent<TMPro.TextMeshPro>();
        int givePower = Random.Range(PowerGiving.x, PowerGiving.y);
        FindObjectOfType<Player>().AddPower(givePower);
        textPower.text = "+" + givePower.ToString();
        Vector3 startScale = ScalingObject.localScale;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            ScalingObject.localScale = Vector3.Lerp(Vector3.zero, startScale, timer / allTimer);
            MaterialSet.material.Lerp(CursedMaterial, MaterialSet.material, timer / allTimer);
            yield return new WaitForEndOfFrame();
        }

        BoomParticle.gameObject.SetActive(true);
        gameObject.AddComponent<DestroyTimer>();
    }


}
