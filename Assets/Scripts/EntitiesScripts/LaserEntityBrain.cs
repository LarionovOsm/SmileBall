using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEntityBrain : BaseEntityBrain
{
    [SerializeField] private LineRenderer _laserRay;

    public override void SetStartParams()
    {
        base.SetStartParams();
        _projectileGenerator = GameManager.instance.LaserGenerator;
        _laserRay = gameObject.GetComponent<LineRenderer>();
    }

    public override void GenerateProjectile()
    {
        base.GenerateProjectile();
        _laserRay.positionCount = 2;
        //_laserRay.SetPosition(1, )
    }

    public override void SetSettings()
    {
        base.SetSettings();
        _name = "LaserGun";
        foreach (EntitySpecification specification in _entitySettings.EntitySpecifications)
        {
            if (specification.Name == _name)
            {
                _triggerRadius = specification.TriggerRadius;
                _triggerZone.radius = specification.TriggerRadius;
                _rotationSpeed = specification.RotationSpeed;
                _aimSearchTime = specification.AimSearchTime;
                _rechargeDelay = specification.RechargeDelay;
                _projectileSpeed = specification.ProjectileSpeed;
                _damage = specification.ProjectileDamage * (-1);
            }
        }
    }
}
