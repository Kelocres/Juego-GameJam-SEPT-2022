using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected Transform _transform;
    [SerializeField] private ProjectileId _id;
    [SerializeField]
    private float desTroyIn;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float damage;
   
    public string Id { get => _id.Value; }
    //public float Damage { get => damage; set => damage = value; }
    private void FixedUpdate()
    {
        DoMove();
    }

    public void Start()
    {
        _transform = transform;
        DoStart();
        StartCoroutine(DestroyIn(desTroyIn));
    }

    private IEnumerator DestroyIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DestroyProjectile();
    }
    /*public void OnCollisionEnter(Collision collision)
    {
        DestroyProjectile();
    }*/
    private void DestroyProjectile()
    {
        DoDestroy();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D()
    {
        DestroyProjectile();
    }
    protected abstract void DoMove();
    protected abstract void DoDestroy();
    protected abstract void DoStart();
}
