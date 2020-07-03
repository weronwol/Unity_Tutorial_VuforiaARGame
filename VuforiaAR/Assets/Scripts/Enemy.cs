using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("速度"), Range(0, 10)]
    public float speed = 1.5f;
    [Header("停止距離"), Range(0, 5)]
    public float stopDistance = 2;
    [Header("追蹤目標名稱")]
    public string targetName = "塔";
    [Header("爆炸效果")]
    public GameObject explosion;
    [Header("傷害"), Range(10, 100)]
    public float damage;

    private Transform target;

    private NavMeshAgent nma;

    private void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        nma.speed = speed;
        nma.stoppingDistance = stopDistance;

        target = GameObject.Find(targetName).transform;
        nma.SetDestination(target.position);
    }
    private void CreateEffect()
    {
        GameObject expl = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(expl, 0.5f);
        Destroy(gameObject);
    }
    private void Track()
    {
        nma.SetDestination(target.position);

        if (nma.remainingDistance <= stopDistance)
        {
            CreateEffect();
            target.GetComponent<Tower>().Damage(damage);
        }
    }
    private void ClickAndDead()
    {
        CreateEffect();
        Tower.target = transform;
    }
    private void OnMouseDown()
    {
        ClickAndDead();
    }
    private void Update()
    {
        Track();
    }
}
