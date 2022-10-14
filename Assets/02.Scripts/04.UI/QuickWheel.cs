using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickWheel : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text _towerName;
    [SerializeField] private TMP_Text _towerCost;


    // ������ Ȱ��ȭ ���� ���� index
    [Header("Icon")]
    [SerializeField] private Transform _towerIconsTransform;
    [SerializeField] private TowerInfo[] _towerInfos = new TowerInfo[8];
    private int _deforeindex = 9;


    private void Update()
    {
        // ��ũ�� ���߾� ��ǥ
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        int seletindex = 0;

        // ��ũ�� ���߾ӿ��� ���� �ŷ� ���϶� ���� (ui �̹��� �� ��)
        if (Vector3.Distance(screenCenter, Input.mousePosition) > 150f)
        {

            // �߾ӿ��� ���콺 �����ǰ��� ������ ���  / 12�� 0=8', 3�� : 2, 6�� : 4, 9�� : 6
            seletindex = ((int)(Quaternion.FromToRotation(Vector3.up, Input.mousePosition - screenCenter).eulerAngles.z / 22.5f) + 1) / 2;
            seletindex = 8 - seletindex;
            // 8 �̻��̸� 0���� ����
            seletindex = seletindex >= 8 ? 0 : seletindex;

            // �ش� �ε������� ��Ŭ���� ������ Ÿ���� Ÿ���������� �����ְ� ��ġ
            if (Input.GetMouseButtonUp(0) &&
                _towerInfos[seletindex] != null)
            {
                MainUIManager.instance.isShowBuilder = true;
                MainUIManager.instance.SetTowerBilder(_towerInfos[seletindex]);

                gameObject.SetActive(false);
            }
        }
        else
        {
            // ����ó����
            seletindex = 9;
        }

        // ���� �ε����� ���� �ε����� �ٸ��� ���� ���� (�̹����� ȿ�� ���� ����)
        if (_deforeindex != seletindex)
        {
            ChangedSelect(seletindex);
            _deforeindex = seletindex;
        }
    }

    private void OnEnable()
    {
        // ui Ȱ��ȭ�� Ŀ�� ���� �� ��������
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _towerName.text = "TowerName";
        _towerCost.text = "TowerCost";
    }
    private void OnDisable()
    {
        // ui ��Ȱ��ȭ�� Ŀ�� �Ⱥ��� �� ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        MainUIManager.instance.CheckAllUIClose();
    }

    // �ε����� �ش�Ǵ� ���� ���� ����
    private void ChangedSelect(int index)
    {
        // �� ����
        for (int i = 0; i < _towerIconsTransform.childCount; i++)
        {
            if (index == i)
                _towerIconsTransform.GetChild(i).GetComponent<Image>().color = Color.blue;
            else
                _towerIconsTransform.GetChild(i).GetComponent<Image>().color = Color.white;
        }

        // 9�� ����ó��
        if (index == 9) return;

        if (_towerInfos[index] != null)
        {
            _towerName.text = _towerInfos[index].name;
            _towerCost.text = $"$ {_towerInfos[index].BuyCost}";

            // ��ȭ Ȯ�� �� �� ������ ���� ������
            if (MainGameManager.Instance.Money < _towerInfos[index].BuyCost)
            {
                _towerCost.color = Color.red;
            }
            else
            {
                _towerCost.color = Color.black;
            }
        }
        else
        {
            _towerName.text = "TowerName";
            _towerCost.text = "TowerCost";
        }
    }
}
