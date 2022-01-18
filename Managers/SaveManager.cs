using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager: Singleton<SaveManager> {
    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Update() {
        // ����
        if (Input.GetKeyDown(KeyCode.S)) {
            SavaPlayerData();
        }

        // ��ȡ
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadPlayerData();
        }
    }

    public void SavaPlayerData() {
        Save(GameManager.Instance.playerStats.characterData, GameManager.Instance.playerStats.name);
    }

    public void LoadPlayerData() {
        Load(GameManager.Instance.playerStats.characterData, GameManager.Instance.playerStats.name);
    }

    public void Save(object data, string key) {
        var jsonData = JsonUtility.ToJson(data, true);

        // ���� key jsonData ������ϵͳ������
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    public void Load(object data, string key) {

        // �ж� key �Ƿ�����ֵ
        if (PlayerPrefs.HasKey(key))
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        
    }
}
