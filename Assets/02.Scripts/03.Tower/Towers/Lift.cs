using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : TowerBase , IInteraction
{
    [SerializeField] private GameObject _plate; 
    [SerializeField] private float _height;
    [SerializeField] private CapsuleCollider _activeCollider;

    [SerializeField] private GameObject _lense;
    [SerializeField] private float _lenseSpeed;
    
    private Camera _camera;

    private bool _active = false;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    public void Interaction()
    {
        if (_active == false)
        {
            _active=true;
            Player.Instance.PlayerStop();
            Player.Instance.isNotMoveable = true;
            _activeCollider.enabled = true;
            Player.Instance.gameObject.transform.position = transform.position + Vector3.up * _height;
            _plate.transform.position += Vector3.up * (_height);

            _lense.SetActive(true);
        }
        else
        {
            _active = false;
            Player.Instance.isNotMoveable = false;
            _activeCollider.enabled = false;
            Player.Instance.gameObject.transform.position = transform.position;
            _plate.transform.localPosition = Vector3.zero;

            _lense.SetActive(false);
            _lense.transform.eulerAngles = Vector3.right * 90f;
        }
    }

    protected override void Update()
    {
        if (_active)
        {
            _lense.transform.rotation = Quaternion.Lerp(_lense.transform.rotation,_camera.transform.rotation, _lenseSpeed*Time.deltaTime);
        }
    }

    public override bool OnUpgrad(Element addElement)
    {
        if (upgradLevel >= 3) return false;
        _applyelements.Add(addElement);
        switch (upgradLevel)
        {
            case 0:
                attackType = addElement;
                upgradLevel++;
                return true;
            case 1:
                //_bulletName = "ProjectileBullet_" + addElement.ToString();
                upgradLevel++;

                return true;
            case 2:
                upgradLevel++;
                //_afterEffect = addElement.ToString();
                return true;
            default:
                return false;

        }
    }
}
