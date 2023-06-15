using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VRController : MonoBehaviour
{
    #region Elements
    [SerializeField]
    private bool _isRight;

    public LineRenderer _laser;
    public Color _laserColor;

    private OVRInput.RawButton _triggerButton;

    private Transform _castedObject;

    private ObjectBase _castedComponent;

    private Define.CatingType _targetType;

    private Button _button;

    private InputField _inputField;
    #endregion

    private Vector3 _origin;
    private Vector3 _dir;
    private float _rayLength = 100f;
    private RaycastHit _hit;
    private Transform _hitTransform;

    private void Start()
    {
        SetLaser();
        SetButton();
    }

    private void SetLaser()
    {
        _laser = transform.GetComponent<LineRenderer>();

        _laserColor = Color.cyan;
        SetLaserColor(_laserColor);

        _laser.positionCount = 2;
        _laser.startWidth = 0.01f;
        _laser.endWidth = 0.01f;
    }

    public void SetLaserColor(Color color)
    {
        _laserColor = color;
        _laser.material.color = _laserColor;
    }

    public void DrawLaser(Vector3 destnation)
    {
        _laser.SetPosition(0, transform.position);
        _laser.SetPosition(1, destnation);
    }

    public void LaserEnable(bool enable)
    {
        _laser.enabled = enable;
    }

    private void SetButton()
    {
        if (_isRight)
        {
            _triggerButton = OVRInput.RawButton.RIndexTrigger;
        }
        else
        {
            _triggerButton = OVRInput.RawButton.LIndexTrigger;
        }
    }

    private void Update()
    {
        ControllerCycle();
    }

    private void ControllerCycle()
    {
        _dir = transform.forward;
        _origin = transform.position;

        //if (OVRInput.GetDown(_triggerButton))
        //{ GetDownTrigger(); }

        //if (OVRInput.GetUp(_triggerButton))
        //{ GetUpTrigger(); }

        if (IsHitRay())
        {
            _hitTransform = _hit.transform;
            DrawLaser(_hit.point);
            //ObjectCasting();
        }
        else
        {
            DrawLaser(transform.forward * _rayLength);

            //if (_castedObject != null)
            //{
            //    ExitCasting();
            //}
        }
    }

    private bool IsHitRay(int layer = -1)
    {
        if (layer == -1)
        {
            return Physics.Raycast(_origin, _dir, out _hit, _rayLength);
        }
        return Physics.Raycast(_origin, _dir, out _hit, _rayLength, layer);
    }

    private void ObjectCasting()
    {
        _castedComponent = null;
        if (_hitTransform.TryGetComponent(out _castedComponent))
        {
            _targetType = _castedComponent._type;
            Debug.Log(_castedComponent._type);
        }
        _castedObject = _hitTransform;
    }

    private void ExitCasting()
    {
        if (_targetType == Define.CatingType.Button)
        {
            _button = null;
        }

        if (_targetType == Define.CatingType.InputField)
        {
            _inputField = null;
        }
        _castedObject = null;
    }

    private void GetDownTrigger()
    {
        SetLaserColor(Color.white);

        if (_castedObject == null)
            return;

        EnterInteract();
    }

    private void EnterInteract()
    {
        if (_castedComponent == null)
            return;

        if (_targetType == Define.CatingType.Button)
        {
            _button = _castedComponent.Button;
            _button.onClick.Invoke();
        }

        if (_targetType == Define.CatingType.InputField)
        {
            _inputField = _castedComponent.InputField;
            _inputField.ActivateInputField();
        }
    }

    private void GetUpTrigger()
    {
        ExitInteract();
        SetLaserColor(Color.cyan);
    }

    public void ExitInteract()
    {
        if (_castedComponent == null)
            return;

        if (_targetType == Define.CatingType.Button)
        {
            _button = _castedComponent.Button;
            _button.OnPointerExit(null);
            _button = null;
        }

        if (_targetType == Define.CatingType.InputField)
        {
            _inputField = null;
        }
        _castedObject = null;
    }
}
