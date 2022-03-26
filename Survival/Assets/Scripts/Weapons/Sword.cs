using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sword : Weapon
{
    private void Update()
    {
        _currentTimeBtwAttack -= Time.deltaTime * agility;
        if (Input.GetMouseButtonDown(0) && _currentTimeBtwAttack <= 0)
        {
            //playerAnimator.SetTrigger("SwordAttack");
            playerEnergy.ChangeEnergy(expendedEnergy);
            _currentTimeBtwAttack = timeBtwAttack;
        }
    }
    public override void Attack()
    {
        
    }
}
