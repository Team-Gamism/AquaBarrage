using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Fish_Direction
{
    Left,
    Right
}

public class Fish_Squid : MonoBehaviour, ICanFish
{
    [SerializeField] private FishStatSO fishInfo;
    [SerializeField] private float riseSpeed = 15f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float detectionRange = 50f;
    [SerializeField] private float targetHeight = 1.25f;
    public Fish_Direction fish_Direction;
    public string fishName;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private float speed;
    private float weight;
    private float attackCoolTime;
    private float turnVecX = 0f;
    private float time = 0f;
    private bool isPlayerDetect = false;
    private bool isAttack = false;
    private bool isRising = false;
    private bool isExploded = false;

    private void Init()
    {
        skinnedMeshRenderer = transform.Find("Squid").GetComponent<SkinnedMeshRenderer>();

        fishName = fishInfo.fishName;
        speed = fishInfo.speed;
        weight = fishInfo.weight;
        attackCoolTime = fishInfo.attackCoolTime;
    }

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        if (!isExploded)
        {
            Move();
        }
    }

    private void Move()
    {
        if (!isAttack && !isRising)
        {
            transform.Translate((fish_Direction == Fish_Direction.Left ? 1 : -1) * speed * transform.right / 2 * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            Rotate();
            CheckPlayerDetection();
        }
        else if (isRising)
        {
            RiseUp();
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(
            Mathf.Lerp(transform.rotation.eulerAngles.x >= 180 ?
            transform.rotation.eulerAngles.x - 360 : transform.rotation.eulerAngles.x,
            turnVecX, Time.deltaTime * Time.deltaTime) - (fish_Direction == Fish_Direction.Left ? 0 : 180), -90f, fish_Direction == Fish_Direction.Left ? 0 : -180));
    }

    private void CheckPlayerDetection()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, detectionRange))
        {
            if (hit.collider != null && hit.collider.CompareTag("Player") && !isPlayerDetect)
            {
                isPlayerDetect = true;
                DetectPlayerExplosion();
            }
        }
    }

    private void DetectPlayerExplosion()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        isRising = true;
        time = 0f;
    }

    private void RiseUp()
    {
        if (transform.position.y < targetHeight)
        {
            transform.Translate(Vector3.up * riseSpeed * Time.deltaTime, Space.World);
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (!isExploded)
        {
            isExploded = true;

            GameObject explosionPrefab = Resources.Load<GameObject>("Fish/Effect/Explosion");
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            skinnedMeshRenderer.enabled = false;
            StartCoroutine(DestroyParticlePlay(explosion));
        }
    }

    private IEnumerator DestroyParticlePlay(GameObject particleObject)
    {
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
        yield return new WaitUntil(() => !particleSystem.isPlaying);
        Destroy(particleObject);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.up * detectionRange);
    }

    public virtual Transform Fished(Transform hook)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        transform.SetParent(hook);

        transform.localPosition = Vector3.zero;

        GameManager.Instance.money += fishInfo.money;
        GameManager.Instance.fishCount++;

        return transform;
    }
}
