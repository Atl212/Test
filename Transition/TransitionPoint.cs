using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint: MonoBehaviour {

    // ͬ���� or �쳡�� ����
    public enum TransitionType {
        SameScene, DifferentScene
    }

    [Header("Transition Info")]
    public string sceneName;
    public TransitionType transitionType;
    // �����յ�ı�ǩ
    public TransitionDestination.DestinationTag destinationTag;
    // ���Ա����͵ı�ǩ
    private bool canTrans;

    void Update() {
        // ��ⰴ��
        if(Input.GetKeyDown(KeyCode.E) && canTrans) {
            // ���͵�ǰ�����
            SceneController.Instance.TransitionToDestination(this);
        }    
    }

    void OnTriggerStay(Collider other) {

        // ֻ��Player���ܱ����� ���ñ�ǩ
        if (other.CompareTag("Player"))
            canTrans = true;
    }

    void OnTriggerExit(Collider other) {

        // �뿪����false
        if (other.CompareTag("Player"))
            canTrans = false;
    }
}
