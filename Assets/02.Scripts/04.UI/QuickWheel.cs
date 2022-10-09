using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickWheel : MonoBehaviour
{
    // ������ Ȱ��ȭ ���� ���� index
    private int _deforeindex;

    [SerializeField] private Transform _towerIcons;

    [SerializeField] private TowerInfo[] _towerInfos = new TowerInfo[8];

    private void Update()
    {
        // ��ũ�� ���߾� ��ǥ
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        int seletindex = 0;

        // ��ũ�� ���߾ӿ��� ���� �ŷ� ���϶� ���� (ui �̹��� �� ��)
        if (Vector3.Distance(screenCenter, Input.mousePosition) > 150f)
        {

            // �߾ӿ��� ���콺 �����ǰ��� ������ ���  / 12�� 0'. 6�� 180'
            seletindex = ((int)(Quaternion.FromToRotation(Vector3.up, Input.mousePosition - screenCenter).eulerAngles.z / 22.5f) + 1) / 2;

            // 8 �̻��̸� 0���� ����
            seletindex = seletindex >= 8 ? 0 : seletindex;

            // �ش� �ε������� ��Ŭ���� ������ Ÿ���� Ÿ���������� �����ְ� ��ġ
            if (Input.GetMouseButtonUp(0))
            {
                MainUIManager.instance.isShowBuilder = true;
                MainUIManager.instance.SetTowerBilder(_towerInfos[seletindex]);
                
                gameObject.SetActive(false);
            }
        }
        else
        {
            seletindex = 9;
        }

        // ���� �ε����� ���� �ε����� �ٸ��� ���� ���� (�̹����� ȿ�� ���� ����)
        if (_deforeindex != seletindex)
        {
            ChangedColor(seletindex);
            _deforeindex = seletindex;
        }
    }

    private void OnEnable()
    {
        // ui Ȱ��ȭ�� Ŀ�� ���� �� ��������
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        // ui ��Ȱ��ȭ�� Ŀ�� �Ⱥ��� �� ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        MainUIManager.instance.CheckAllUIClose();
    }

    // �ε����� �ش�Ǵ� ���� ���� ����
    private void ChangedColor(int index)
    {
        for (int i = 0; i < _towerIcons.childCount; i++)
        {
            if (index == i)
                _towerIcons.GetChild(i).GetComponent<Image>().color = Color.blue;
            else
                _towerIcons.GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }
}
