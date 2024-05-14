using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEntityBrain : BaseEntityBrain
{
    public override void SetStartParams()
    {
        base.SetStartParams();
        _projectileGenerator = GameManager.instance.CannonGenerator;
    }

    public override void GenerateProjectile()
    {
        base.GenerateProjectile();
        _projectileGenerator.CreateProjectile(_shotPosition,  _projectileSpeed, _damage);
    }

    public override void SetSettings()
    {
        base.SetSettings();
        _name = "CannonGun";
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
