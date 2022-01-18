using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    public CharacterStats playerStats;

    private CinemachineFreeLook followCamera;

    // �б� �㼯�۲���
    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();

    // ע��player
    public void RigisterPlayer(CharacterStats player) {
        playerStats = player;
        // �õ����
        followCamera = FindObjectOfType<CinemachineFreeLook>();

        if(followCamera != null) {
            // �õ� LookAtPoint ��λ��
            followCamera.Follow = playerStats.transform.GetChild(2);
            followCamera.LookAt = playerStats.transform.GetChild(2);
        }
    }

    protected override void Awake() {
        base.Awake();
        // ��֤�����л��󲻻����ٵ�ǰ�ű� �����ҵ��ű���Ŀ��
        DontDestroyOnLoad(this);
    }

    // �����ɵĵ�����ӵ��۲����б�
    public void AddObserver(IEndGameObserver observer) {
        // ����ʱ�Ż���� �ʲ�������ظ����� ����Ҫ�ж��Ƿ����б��д���
        endGameObservers.Add(observer);
    }

    // �Ƴ��۲���
    public void RemoveObserver(IEndGameObserver observer) {
        endGameObservers.Remove(observer);
    }

    // �����й۲��߹㲥
    public void NotifyObservers() {

        // ѭ��ÿһ���۲��� ������Ϸ�����㲥
        foreach (var observer in endGameObservers) 
            observer.EndNotify();
        
    }
}
