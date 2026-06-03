using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Moving,
    HomingBurst,
    WaveAttack,
    FallingAttack
}

public class BossMovement : MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private Transform[] movePoints;
    private Vector3 lastPoint;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;

    [SerializeField] private Vector3 targetPosition;

    private BossState currentState;

    private BossAttackPatterns attackPatterns;

    private void Awake()
    {
        attackPatterns = GetComponent<BossAttackPatterns>();
    }

    private void Start()
    {
        StartCoroutine(BossLoop());
        lastPoint = targetPosition;
    }

    private void Update()
    {
        if (currentState == BossState.Moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator BossLoop()
    {
        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            yield return MoveToRandomPoint();

            yield return new WaitForSeconds(1.5f);

            yield return ExecuteRandomAttack();
        }
    }

    private IEnumerator MoveToRandomPoint()
    {
        currentState = BossState.Moving;

        do
        {
            targetPosition = movePoints[Random.Range(0, movePoints.Length)].position;
        } while (targetPosition == lastPoint);

        lastPoint = targetPosition;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            yield return null;
        }
    }

    private IEnumerator ExecuteRandomAttack()
    {
        int attack = Random.Range(0, 3);

        switch (attack)
        {
            case 0:
                currentState = BossState.HomingBurst;
                yield return attackPatterns.HomingBurst();
                break;

            case 1:
                currentState = BossState.WaveAttack;
                yield return attackPatterns.WaveAttack();
                break;

            case 2:
                currentState = BossState.FallingAttack;
                yield return attackPatterns.FallingAttack();
                break;
        }
    }
}
