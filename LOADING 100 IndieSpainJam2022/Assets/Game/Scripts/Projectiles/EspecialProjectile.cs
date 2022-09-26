using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Projectiles
{
    internal class EspecialProjectile : Projectile
    {
        protected override void DoDestroy()
        {
            
        }

        protected override void DoMove()
        {
            GetComponent<Rigidbody>().velocity += transform.forward * _speed * Time.deltaTime;
        }

        protected override void DoStart()
        {
            _speed = _speed + (_speed / 2);
            damage = damage * 2;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyStats>().GetDamage(1);
                collision.rigidbody.velocity = Vector3.zero;
                Destroy(gameObject);
            }
        }
    }
}
