using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour
{
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpandable;
    [SerializeField] private BasePojectile _projectilePrefab;
    private PoolMono<BasePojectile> _pool;

    private void Start()
    {
        this._pool = new PoolMono<BasePojectile>(this._projectilePrefab, this._poolCount, this.transform);
        this._pool.AutoExpand = _autoExpandable;
    }

    public BasePojectile CreateProjectile(Transform shotPosition, float speed, int damage)
    {
        Vector3 position = shotPosition.position;
        BasePojectile projectile = this._pool.GetFreeElement();
        projectile.transform.position = position;
        projectile.StartLaunch(shotPosition, speed, damage);
        return projectile;
    }
}
