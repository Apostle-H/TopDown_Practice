using UnityEngine;

namespace EnemySystem.EnemyMelee
{
    public class Attack
    {
        public void MakeAttack(GameObject target, int damage)
        {
            Debug.Log($"Урон по {target.name} = {damage}");
        }
    }
}