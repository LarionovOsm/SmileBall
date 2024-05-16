using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : BasePojectile
{
    public override void ProjectileBehaviour()
    {
        base.ProjectileBehaviour();
        _weaponType = TypeOfWeapon.Laser;
        _projectileRigidbody.AddForce(_shotPosition.transform.up * _speed, ForceMode.Force);
    }

    public override void GoalAim()
    {
        base.GoalAim();
        this.gameObject.SetActive(false);
        this._damage = 0;
        this._isLaucnh = false;
        this._projectileRigidbody.velocity = Vector3.zero;
        this._shotPosition = null;
        this._speed = 0;
    }
}
