using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickWheel : MonoBehaviour
{
    // 이전에 활성화 중인 슬롯 index
    private int _deforeindex;

    [SerializeField] private Transform _towerIcons;

    private void Update()
    {
        // 스크린 정중앙 좌표
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        int seletindex = 0;

        // 스크린 정중앙에서 일정 거래 밖일때 동작 (ui 이미지 원 밖)
        if (Vector3.Distance(screenCenter, Input.mousePosition) > 150f)
        {

            // 중앙에서 마우스 포지션간에 각도를 계산  / 12시 0'. 6시 180'
            seletindex = ((int)(Quaternion.FromToRotation(Vector3.up, Input.mousePosition - screenCenter).eulerAngles.z / 22.5f) + 1) / 2;

            // 8 이상이면 0으로 수정
            seletindex = seletindex >= 8 ? 0 : seletindex;

            // 해당 인덱스에서 좌클릭시 연동된 아이템 사용
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(seletindex);
                //Inventory.GetInstance.UsingItemID(Inventory.GetInstance.GetQuickItemID(l_seletindex));
            }
        }
        else
        {
            seletindex = 9;
        }

        // 이전 인덱스와 현재 인데스가 다르면 색상 변경 (이미지나 효과 변경 가능)
        if (_deforeindex != seletindex)
        {
            ChangedColor(seletindex);
            _deforeindex = seletindex;
        }
    }

    private void OnEnable()
    {
        // ui 활성화시 커서 보임 및 고정해제
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        // ui 비활성화시 커서 안보임 및 고정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // 인덱스에 해당되는 슬롯 생상 변경
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
