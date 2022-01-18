using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rock: MonoBehaviour {

    // ����ö�ٱ��� ����ʯͷ��ͬ״̬
    public enum RockStates { HitPlayer, HitEnemy, HitNothing}
    // ���治ͬ��״̬ public ����������Է���
    public RockStates rockStates;

    // �����������
    private Rigidbody rb;

    [Header("Basic Settings")]
    // �������Ļ�������
    public float force;
    // ����ʯͷ�˺�
    public int damage;
    // ��������Ŀ��
    public GameObject target;
    // ������������
    private Vector3 direction;
    // ����ʯͷ�ƻ���Ч��
    public GameObject breakEffect;

    private void Start() {
        // ����ʱ���и�ֵ
        rb = GetComponent<Rigidbody>();

        // ��������ʱ�ĳ�ʼ�ٶ� ��ֹ��ʼ�ٶ�Ϊ 0
        // ���º���ķ����ж��ٶ�С�� 1 ʱ �ڿ�ʼʱ��Ϊ HitNothing ״̬
        rb.velocity = Vector3.one;

        // ��ʼ״̬���ù���player
        rockStates = RockStates.HitPlayer;

        FlyToTarget();
    }

    // ���������������·���
    void FixedUpdate() {
        // ��ʯͷ�ٶ�С�� 1 ʱ�л�Ϊ HitNothing ״̬
        if(rb.velocity.sqrMagnitude < 1f) {
            rockStates = RockStates.HitNothing;
        }
    }


    public void FlyToTarget() {

        // �������Զ�̶��� �����빥����Χ �����ҵ�player������
        // д��д�� ���ƺ�û����
        if(target == null)
            target = FindObjectOfType<PlayerController>().gameObject;

        // ����һ�����ϵķ��� ��ֹ�Ƕȹ������� ʹ�÷���������һ������
        direction = (target.transform.position - transform.position + Vector3.up).normalized;

        // �ڶ������� ѡ���������� �����
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    // �жϷ�����ײʱ���� �л���ͬ״̬
    void OnCollisionEnter(Collision other) {
        switch(rockStates) {
            case RockStates.HitPlayer:
                if(other.gameObject.CompareTag("Player")){
                    // ��ȡ��Ҷ��� ������ѣ��
                    other.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                    other.gameObject.GetComponent<NavMeshAgent>().velocity = direction * force;
                    other.gameObject.GetComponent<Animator>().SetTrigger("Dizzy");
                    other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage, other.gameObject.GetComponent<CharacterStats>());
                    
                    // �������������˺� ��ΪNothing
                    rockStates = RockStates.HitNothing;
                }
                break;

            case RockStates.HitEnemy:
                // ��ȡ�����������Ƿ��Ƕ��� ����ֱ�ӻ�ȡ�����ж�Ŀ��
                if(other.gameObject.GetComponent<Golem>()) {
                    // ���������ʱ����
                    var otherStats = other.gameObject.GetComponent<CharacterStats>();

                    // �˺������ڵ�������
                    otherStats.TakeDamage(damage, otherStats);

                    // �����ڵ��˺������ƻ�Ч��
                    Instantiate(breakEffect, transform.position, Quaternion.identity);

                    // ���ú�����
                    Destroy(gameObject);
                }
                break;
        }
    }
}
