using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���� EnemyController �ڴ˻������޸�
public class Golem : EnemyController
{
    [Header("Skill")]
    // ���������Ļ�����ֵ
    public float kickForce = 25;
    // ����ʯͷ����
    public GameObject rockPrefab;
    // ����ʯͷ��Ͷ��ʱ�ֵ�����
    public Transform handPos;

    // ��ս���������¼�
    public void KickOff() {
        if(attackTarget != null && transform.IsFacingTarget(attackTarget.transform)) {
            var targetStats = attackTarget.GetComponent<CharacterStats>();

            // �������������� direction.Normalize();
            Vector3 direction = (attackTarget.transform.position - transform.position).normalized;

            targetStats.GetComponent<NavMeshAgent>().isStopped = true;
            targetStats.GetComponent<NavMeshAgent>().velocity = direction * kickForce;
            targetStats.GetComponent<Animator>().SetTrigger("Dizzy");
            targetStats.TakeDamage(characterStats, targetStats);
        }
    }

    // Զ�̹��������¼�
    public void ThrowRock() {
        // ����Ŀ�����ʱִ��
        if(attackTarget) {
            // ����ʯͷ���� ��������λ�� ��ʼ��תΪ���� ά��ԭ����״̬
            var rock = Instantiate(rockPrefab, handPos.position, Quaternion.identity);

            // ���ù���Ŀ��
            rock.GetComponent<Rock>().target = attackTarget;
        }
    }
}
