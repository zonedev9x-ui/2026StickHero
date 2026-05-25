using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public StrengthScore strengthScore;

    public List<ItemWeapon> itemWeapons;

    public void InitWeapon(WeaponType weaponType, StrengthType strengthType, int score)
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

    public void OnEnableWeapon(bool isOn)
    {
        gameObject.SetActive(isOn);
    }
}
