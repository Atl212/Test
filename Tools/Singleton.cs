using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// where Լ�� T �� Singleton<T> ����
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    // ����ģʽ ���ɶ� get
    public static T Instance {
        get { return instance; }
    }

    // ֻ����̳������ ��д �̳�
    protected virtual void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            // ���� this ��Ӧ��ͬ����
            instance = (T)this;
        }
    }

    // �жϵ�ǰ���͵���ģʽ�Ƿ��ѱ�����
    public static bool IsInitialized {
        // �������� == true
        get { return instance != null; }
    }

    // 
    protected virtual void OnDestory() {
        
        // ���������� ��Ϊ��
        if(instance == this) {
            instance = null;
        }
    }

}
