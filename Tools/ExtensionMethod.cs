using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��չ��������̳������� ��̬ʹ�ÿ��Կ��ٵ��ú�ִ��
public static class ExtensionMethod {
    // ��̬��ҪΪ���� const ���ɸ���
    private const float dotThreshold = 0.5f;

    // ���� �ж�Ŀ���Ƿ�����ǰ��
    // ��������Ĳ��� ��ǰһ����Ϊ��Ҫ��չ���� ��һ������Ҫ���õ���
    public static bool IsFacingTarget(this Transform transform, Transform target) {
        // ���Ŀ�����Լ������λ��
        var vectorToTarget = target.position - transform.position;
        vectorToTarget.Normalize();

        // ���뵱ǰ���˵ĳ��� 
        float dot = Vector3.Dot(transform.forward, vectorToTarget);

        // ���ع���Ŀ���Ƿ��� 45�㹥����Χ��
        return dot >= dotThreshold;
    }
}
