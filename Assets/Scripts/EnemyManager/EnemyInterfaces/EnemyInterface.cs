using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemys
{
    public interface IOnDamage
    {
        void OnDamage(int damage);
    }
    public interface IGetBodyHittingAtk
    {
        int GetBodyHittingAtk();
    }
    public interface IDie
    {
        void Die();
    }
    public interface IAttack
    {
        void Attack();
    }
}
