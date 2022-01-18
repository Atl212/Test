using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���� EnemyController �ڴ˻������޸�
public class Grunt : EnemyController
{
    [Header("Skill")]
    // ���������Ļ�����ֵ
    public float kickForce = 10;

    public void KickOff() {
        if(attackTarget) {
            // �õ��˿����ɫ
            transform.LookAt(attackTarget.transform);

            // ��û��ɷ��� 
            Vector3 direction = attackTarget.transform.position -transform.position;
            // ��������� 0 or 1
            direction.Normalize();

            // �����ҿ��ܽ��е��κ��ƶ� �ٽ��л���
            attackTarget.GetComponent<NavMeshAgent>().isStopped = true;
            // ����ٶȱ��� ���� �����ٶ� = ���� * ������
            attackTarget.GetComponent<NavMeshAgent>().velocity = direction * kickForce;

            attackTarget.GetComponent<Animator>().SetTrigger("Dizzy");
        }
    }

}
