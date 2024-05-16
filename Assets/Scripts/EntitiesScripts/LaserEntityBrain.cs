using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaserEntityBrain : BaseEntityBrain
{
    [SerializeField] private LineRenderer _laserRay;
    private float _laserAttackTime;
    private Sequence _laserSequence;

    public override void SetStartParams()
    {
        base.SetStartParams();
        _projectileGenerator = GameManager.instance.LaserGenerator;
        _laserRay = gameObject.GetComponent<LineRenderer>();
    }

    public override void GenerateProjectile(Collider aim)
    {
        base.GenerateProjectile(aim);
        _laserRay.enabled = true;
        _mode = Mode.Transition;
        BasePojectile projectile = _projectileGenerator.CreateProjectile(_shotPosition, _projectileSpeed, _damage);
        projectile.transform.DOMove(aim.transform.position, _laserAttackTime/2f)
            .OnUpdate(() => _laserRay.SetPosition(1, projectile.transform.position))
            .OnComplete(() => LaserSequence(aim, projectile));
    }

    public override void TryShoot(Collider aim)
    {
        if (Time.time < _nextShotTime) return;
        else
        {
            _nextShotTime = Time.time + _rechargeDelay + _laserAttackTime;
            GenerateProjectile(aim);
        }
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
                _laserAttackTime = specification.LaserAttackTime;
                _projectileSpeed = specification.ProjectileSpeed;
                _damage = specification.ProjectileDamage * (-1);
                _laserRay.SetPosition(0, transform.position);
                _laserRay.SetPosition(1, transform.position);
            }
        }
    }

    private void LaserSequence(Collider aim, BasePojectile projectile)
    {
        Vector3 aimDirection = Vector3.zero;
        Quaternion quaternion = _cannonObject.rotation;

        _laserSequence = DOTween.Sequence()
                    .AppendInterval(_laserAttackTime)
                    .AppendCallback(() => _laserRay.enabled = false)
                    .AppendCallback(() => _laserRay.SetPosition(1, transform.position))
                    .AppendCallback(() => projectile.GoalAim())
                    .AppendCallback(() => aimDirection = GameManager.instance.PlayerController.transform.position - _cannonObject.position)
                    .AppendCallback(() => quaternion = RequiredAngle(aimDirection, _cannonObject.transform.up, Vector3.forward))
                    .AppendCallback(() => _cannonObject.DORotateQuaternion(quaternion, _aimSearchTime).SetEase(Ease.OutBack)
                                          .OnComplete(() => ContinueAttack(aim)));
    }
}
