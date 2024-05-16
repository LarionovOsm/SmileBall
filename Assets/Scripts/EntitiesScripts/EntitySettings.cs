using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/EntitySettings")]
public class EntitySettings : ScriptableObject
{
    public List<EntitySpecification> EntitySpecifications = new List<EntitySpecification>();
}

[Serializable]
public class EntitySpecification
{
    [SerializeField] private string _name;
    [SerializeField] private float _triggerRadius;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _aimSearchTime;
    [SerializeField] private float _rechargeDelay;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _laserAttackTime;
    [SerializeField] private int _projectileDamage;

    #region References
    public string Name => _name;
    public float TriggerRadius => _triggerRadius;
    public float RotationSpeed => _rotationSpeed;   
    public float AimSearchTime => _aimSearchTime;
    public float RechargeDelay => _rechargeDelay;
    public float ProjectileSpeed => _projectileSpeed;
    public float LaserAttackTime => _laserAttackTime;
    public int ProjectileDamage => _projectileDamage;
    #endregion
}

