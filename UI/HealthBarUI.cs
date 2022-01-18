using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI: MonoBehaviour {
    public GameObject healthUIPrefab;
    // Ѫ������
    public Transform barPoint;
    // �ؼ����ÿɼ�����
    public bool alwaysVisible;
    // �ؼ��ǳ��ÿɼ��³��ֳ���ʱ��
    public float visibleTime;
    // ʣ����ʾʱ��
    private float timeLeft;
    // Ѫ��������
    Image healthSlider;
    // Ѫ��λ��
    Transform UIbar;
    // �����λ��
    Transform cameraPos;
    // ����
    CharacterStats currentStats;

    void Awake() {
        currentStats = GetComponent<CharacterStats>();

        currentStats.UpdateHealthBarOnAttack += UpdateHealthBar;
    }

    // �������������
    void OnEnable() {
        cameraPos = Camera.main.transform;

        // �������� Canvas ���� ����UI 
        foreach(Canvas canvas in FindObjectsOfType<Canvas>()) {
            // ���������Ⱦ����������ģʽ
            if(canvas.renderMode == RenderMode.WorldSpace) {
                UIbar = Instantiate(healthUIPrefab, canvas.transform).transform;
                // ��ȡ��һ��������
                healthSlider = UIbar.GetChild(0).GetComponent<Image>();
                // �����Ƿ�ɼ�
                UIbar.gameObject.SetActive(alwaysVisible);
            }
        }
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth) {

        if (UIbar) {
            if( currentHealth <= 0)
                Destroy(UIbar.gameObject);

            // �ܵ��˺�ʱ UI���뱻����
            UIbar.gameObject.SetActive(true);
            // ����ʣ��ʱ��
            timeLeft = visibleTime;

            // Ѫ���ٷֱ�
            float sliderPercent = (float)currentHealth / maxHealth;
            // �޸�Ѫ��UI��ʾ����
            healthSlider.fillAmount = sliderPercent;
        }
      
    }

    // Update��ÿһִ֡�� LateUpdate ��һ֡��Ⱦ����ִ��
    void LateUpdate() {
        // ��ֹ���ٺ�ִ�� ��Ҫ�ж��Ƿ����
        if(UIbar) {
            UIbar.position = barPoint.position;
            // ����Ѫ����׼����� ����
            UIbar.forward = -cameraPos.forward;

            // û��ʱ�� �� �����ÿɼ�
            if (timeLeft <= 0 && !alwaysVisible)
                UIbar.gameObject.SetActive(false);
            // ��ȥʱ�� ����ʱ
            else timeLeft -= Time.deltaTime;

        }
    }
}
