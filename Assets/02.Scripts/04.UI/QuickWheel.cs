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


    // 이전에 활성화 중인 슬롯 index
    [Header("Icon")]
    [SerializeField] private Transform _towerIconsTransform;
    [SerializeField] private TowerInfo[] _towerInfos = new TowerInfo[8];
    private int _deforeindex = 9;


    private void Update()
    {
        // 스크린 정중앙 좌표
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        int seletindex = 0;

        // 스크린 정중앙에서 일정 거래 밖일때 동작 (ui 이미지 원 밖)
        if (Vector3.Distance(screenCenter, Input.mousePosition) > 150f)
        {

            // 중앙에서 마우스 포지션간에 각도를 계산  / 12시 0=8', 3시 : 2, 6시 : 4, 9시 : 6
            seletindex = ((int)(Quaternion.FromToRotation(Vector3.up, Input.mousePosition - screenCenter).eulerAngles.z / 22.5f) + 1) / 2;
            seletindex = 8 - seletindex;
            // 8 이상이면 0으로 수정
            seletindex = seletindex >= 8 ? 0 : seletindex;

            // 해당 인덱스에서 좌클릭시 연동된 타워를 타워빌더에게 보내주고 설치
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
            // 예외처리용
            seletindex = 9;
        }

        // 이전 인덱스와 현재 인데스가 다르면 색상 변경 (이미지나 효과 변경 가능)
        if (_deforeindex != seletindex)
        {
            ChangedSelect(seletindex);
            _deforeindex = seletindex;
        }
    }

    private void OnEnable()
    {
        // ui 활성화시 커서 보임 및 고정해제
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _towerName.text = "TowerName";
        _towerCost.text = "TowerCost";
    }
    private void OnDisable()
    {
        // ui 비활성화시 커서 안보임 및 고정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        MainUIManager.instance.CheckAllUIClose();
    }

    // 인덱스에 해당되는 슬롯 생상 변경
    private void ChangedSelect(int index)
    {
        // 색 변경
        for (int i = 0; i < _towerIconsTransform.childCount; i++)
        {
            if (index == i)
                _towerIconsTransform.GetChild(i).GetComponent<Image>().color = Color.blue;
            else
                _towerIconsTransform.GetChild(i).GetComponent<Image>().color = Color.white;
        }

        // 9는 예외처리
        if (index == 9) return;

        if (_towerInfos[index] != null)
        {
            _towerName.text = _towerInfos[index].name;
            _towerCost.text = $"$ {_towerInfos[index].BuyCost}";

            // 재화 확인 후 돈 부족시 빨간 색으로
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
