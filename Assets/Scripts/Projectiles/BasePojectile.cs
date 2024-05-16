using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Rigidbody))]
public class BasePojectile : MonoBehaviour
{
    [SerializeField] private protected Rigidbody _projectileRigidbody;
    [SerializeField] private LayerMask _aimLayerMask;
    private protected TypeOfWeapon _weaponType;
    private protected int _damage;
    private protected float _speed;
    private protected bool _isLaucnh;
    private protected Transform _shotPosition;

    public enum TypeOfWeapon {Cannon, Laser}

    #region EventFunctions
    private void FixedUpdate()
    {
        if (_isLaucnh)
        {
            ProjectileBehaviour();
        } 
    }
    #endregion

    #region TriggerFunctions
    private void OnTriggerEnter(Collider other)
    {
        if ((_aimLayerMask.value & (1 << other.gameObject.layer)) != 0)
        {
            GameManager.instance.PlayerStatsManager.ChangeHealth(_damage);
        }

        if(_weaponType == TypeOfWeapon.Cannon) GoalAim();
    }
    #endregion

    #region BehaviourFunctions
    public void StartLaunch(Transform shotPosition, float speed, int damage)
    {
        _shotPosition = shotPosition;
        _speed = speed;
        _damage = damage;
        _isLaucnh = true;
    }

    public virtual void GoalAim() { }

    public virtual void ProjectileBehaviour() { }
    #endregion
}
