using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Projectiles
{
    internal class NormalProjectile : Projectile
    {
        protected override void DoDestroy()
        {
             
        }

        protected override void DoMove()
        {
            GetComponent<Rigidbody>().velocity += transform.forward * _speed*Time.deltaTime;
            //transform.position += transform.forward * _speed * Time.deltaTime;
        }

        protected override void DoStart()
        {
            
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
