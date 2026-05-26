using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{
    public List<ItemWeapon> itemWeapons;

    public void InitWeapon(WeaponType weaponType, StrengthScoreType strengthType, int score)
    {
        strengthScore.InitStrengthScore(strengthType, score);

        for (int i = 0; i < itemWeapons.Count; i++)
        {
            if (itemWeapons[i].weaponType == weaponType)
            {
                itemWeapons[i].gameObject.SetActive(true);
            }
            else
            {
                itemWeapons[i].gameObject.SetActive(false);
            }
        }
    }
}
