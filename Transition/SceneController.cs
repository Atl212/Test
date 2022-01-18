using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SceneController: Singleton<SceneController> {

    public GameObject playerPrefab;
    GameObject player;
    NavMeshAgent playerAgent;

    protected override void Awake() {
        base.Awake();
        // ��֤�����л��󲻻����ٵ�ǰ�ű� �����ҵ��ű��µ��յ�
        DontDestroyOnLoad(this);
    }

    public void TransitionToDestination(TransitionPoint transitionPoint) {
        // �ж��ǵ�ǰ���ǲ�ͬ����
        switch (transitionPoint.transitionType) {
            case TransitionPoint.TransitionType.SameScene:
                // ��ʼЯ�� ��ó����õ�������
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint.destinationTag));
                break;

            case TransitionPoint.TransitionType.DifferentScene:
                StartCoroutine(Transition(transitionPoint.sceneName, transitionPoint.destinationTag));
                break;
        }
    }

    // �������� �յ㳡�����ͱ�ǩ
    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag) {

        // ����ǰ������������
        SaveManager.Instance.SavaPlayerData();

        // �ж���ͬ���������쳡������
        if(SceneManager.GetActiveScene().name != sceneName) {
            // �ȳ���������� �첽����
            yield return SceneManager.LoadSceneAsync(sceneName);
            // ������ɺ���������
            yield return Instantiate(playerPrefab, GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);

            // �쳡�����Ͷ�ȡ��������
            SaveManager.Instance.LoadPlayerData();
            
            // ��������Я��������
            yield break;
        }
        else {
            // ���player����
            player = GameManager.Instance.playerStats.gameObject;
            playerAgent = player.GetComponent<NavMeshAgent>();

            // ֹͣ���õ��� ��ֹ���ͺ��߻�ԭλ
            playerAgent.enabled = false;

            // ���ô���Ŀ����λ�ú���ת����
            player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);

            // ���ͺ��ٻָ�����
            playerAgent.enabled = true;

            // û��return����ʱ����
            yield return null;
        }
        
    }

    private TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag) {
        // ��������յ������
        var entrances = FindObjectsOfType<TransitionDestination>();

        // �ҵ���ǩ��ͬ �����յ㳡������
        foreach (var e in entrances)
            if (e.destinationTag == destinationTag)
                return e;

        return null;
    }
}
