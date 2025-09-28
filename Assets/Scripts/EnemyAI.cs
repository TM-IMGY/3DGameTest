using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float detectionRadius = 1f; // 追跡を開始する距離
    private bool isChasing = false; // 追跡しているかどうかのフラグ
    public float chaseSpeed = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;      
    }

    // Update is called once per frame
    void Update()
    {
        // 敵とプレイヤーの距離を計算
        float distance = Vector3.Distance(transform.position,player.position);

        if (distance < detectionRadius)
        {
            isChasing = true;
        }
        else if (distance >= detectionRadius)
        {
            isChasing = false;
            agent.ResetPath();
        }

        if (isChasing)
        {
            // 敵の目的地をプレイヤーに設定
            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 衝突したオブジェクトのタグがプレイヤーかどうか
        if (other.CompareTag("Player"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("ゲームオーバー");
        GameManager.Instance.GameOver(true);
    }
}
