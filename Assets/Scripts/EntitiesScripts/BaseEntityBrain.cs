using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public abstract class BaseEntityBrain : MonoBehaviour
{
    [SerializeField] private protected EntitySettings _entitySettings;
    [SerializeField] private protected Transform _cannonObject;
    [SerializeField] private protected Transform _shotPosition;
    [SerializeField] private LayerMask AimRaycastLayer;
    private protected float _triggerRadius;
    private protected float _rotationSpeed;
    private protected float _aimSearchTime;
    private protected float _rechargeDelay;
    private protected float _projectileSpeed;
    private protected int _damage;
    private protected string _name;
    private protected SphereCollider _triggerZone;
    private protected ProjectileGenerator _projectileGenerator;
    private float _rotationDegree;
    private float _nextShotTime = 0f;
    private Vector3 _rotation;
    private Sequence _changeModeSequence;
    private Mode _mode = Mode.Patrol;

    public enum Mode { Patrol, Attack, Transition }

    #region EventFunctions
    private void Start()
    {
        _rotation = _cannonObject.localEulerAngles;
        SetStartParams();
        StartCoroutine(PatrolMode());
    }

    private void OnValidate()
    {
        SetSettings();
    }
    #endregion

    #region StateFunctions
    private IEnumerator PatrolMode()
    {
        while (_mode == Mode.Patrol)
        {
            _rotationDegree += _rotationSpeed * Time.deltaTime;
            _cannonObject.localRotation = Quaternion.Euler(_rotation.x, _rotation.y, _rotationDegree);
            yield return null;
        }
    }

    private IEnumerator AttackMode(Collider aim)
    {
        while (_mode == Mode.Attack)
        {
            Vector3 aimDirection = aim.transform.position - _cannonObject.position;
            Quaternion quaternion = RequiredAngle(aimDirection, _cannonObject.transform.up, Vector3.forward);
            _cannonObject.localRotation = quaternion;
            RaycastHit hit;
            Ray ray = new Ray(transform.position, aim.transform.position - transform.position);
            Physics.Raycast(ray, out hit, _triggerRadius, AimRaycastLayer);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == aim.gameObject)
                {
                    TryShoot();
                }
                Debug.DrawLine(ray.origin, hit.point, Color.red);
            }
            yield return null;
        }
    }

    private void TryShoot()
    {
        if (Time.time < _nextShotTime) return;
        else
        {
            _nextShotTime = Time.time + _rechargeDelay;
            GenerateProjectile();
        }
    }

    public virtual void GenerateProjectile() { }

    #endregion

    #region TriggersFunctions
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 aimDirection = other.transform.position - _cannonObject.position;
            Quaternion quaternion = RequiredAngle(aimDirection, _cannonObject.transform.up, Vector3.forward);
            _changeModeSequence = DOTween.Sequence()
                .AppendCallback(() => StopAllCoroutines())
                .Append(_cannonObject.DORotateQuaternion(quaternion, _aimSearchTime).SetEase(Ease.OutBack))
                .AppendCallback(() => _mode = Mode.Attack)
                .AppendCallback(() => StartCoroutine(AttackMode(other)));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _mode = Mode.Patrol;
            _nextShotTime = 0f;
            StopAllCoroutines();
            _rotation = _cannonObject.localEulerAngles;
            _rotationDegree = _cannonObject.rotation.eulerAngles.z;
            _cannonObject.localRotation = Quaternion.Euler(_rotation.x, _rotation.y, _rotationDegree);
            StartCoroutine(PatrolMode());
        }
    }
    #endregion

    #region SettingsFunctions
    public virtual void SetSettings()
    {
        _triggerZone = GetComponent<SphereCollider>();
        _triggerZone.isTrigger = true;
    }

    public virtual void SetStartParams() { }
    #endregion

    private Quaternion RequiredAngle(Vector3 direction, Vector3 forward, Vector3 axis)
    {
        float angle = Vector3.SignedAngle(direction, forward, axis);
        float requredAngle = _cannonObject.localEulerAngles.z - angle;
        Quaternion requireQuaternion = Quaternion.Euler(0, 0, requredAngle);
        return requireQuaternion;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _triggerRadius);
    }
}
