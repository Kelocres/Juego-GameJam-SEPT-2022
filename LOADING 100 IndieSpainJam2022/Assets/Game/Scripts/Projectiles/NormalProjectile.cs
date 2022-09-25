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
            GetComponent<Rigidbody>().velocity = transform.forward * _speed*Time.deltaTime;
            //transform.position += transform.forward * _speed * Time.deltaTime;
        }

        protected override void DoStart()
        {
            
        }
    }
}
