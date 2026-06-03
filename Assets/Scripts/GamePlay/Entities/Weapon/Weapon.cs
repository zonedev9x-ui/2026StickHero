using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{   
    public WeaponType weaponType = WeaponType.None;
    public List<ItemWeapon> itemWeapons;

    public void InitWeapon(WeaponType weaponType, StrengthScoreType strengthType, int score)
    {
        strengthScore.InitStrengthScore(strengthType, score);

        this.weaponType = weaponType;
    }

    private void Start()
    {
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
