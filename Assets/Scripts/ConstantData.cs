using UnityEngine;

public class ConstantData
{   
    public const string TAG_FLOOR = "Floor";
    public const string TAG_DRAGGABLE = "Draggable";

    public const string ANIM_TRIGGER_GRAB = "isGrab";
    public const string ANIM_TRIGGER_GRAB_RELEASE = "isGrabRelease";
    public const string ANIM_BOOL_RUNNING = "isRunning";
    public const string ANIM_TRIGGER_GET_ITEM = "isGetItem";
    public const string ANIM_TRIGGER_WIN = "isWin";
    public const string ANIM_TRIGGER_LOSE = "isLose";
    public const string ANIM_TRIGGER_OPEN_CHEST = "isOpenChest";
    public const string ANIM_TRIGGER_CHANGE_SIZE = "isChangeSize";
    public const string ANIM_TRIGGER_SLASH_BONUS = "isSlashBonus";
    public const string ANIM_TRIGGER_BOSS_COMBO = "isBossCombo";

    public const string ANIM_TRIGGER_ATTACK = "isAttack";
    public const string ANIM_BLEND_ATTACK = "attackIndex";
    public const int ANIM_ATTACK_EMPTY_MIN = 0;
    public const int ANIM_ATTACK_EMPTY_MAX = 3;
    public const int ANIM_ATTACK_SWORD_MIN = 3;
    public const int ANIM_ATTACK_SWORD_MAX = 6;
    public const int ANIM_ATTACK_HAMMER_AXE_MIN = 6;
    public const int ANIM_ATTACK_HAMMER_AXE_MAX = 9;
    public const int ANIM_ATTACK_DAGGER_MIN = 9;
    public const int ANIM_ATTACK_DAGGER_MAX = 12;


    public const string ANIM_TRIGGER_DAMAGE = "isDamage";
    public const string ANIM_BLEND_DAMAGE = "damageIndex";
    public const float ANIM_DAMAGE_DOWM = 2;
    public const float ANIM_DAMAGE_UP = 3;
    public const float ANIM_DAMAGE_STRAIGHT = 1;

    public const string ANIM_TRAP_BEAR_IS_ACTIVE = "isActive";

    public const string ANIM_BLEND_BOSS_COMBO = "bossComboIndex";
    public const float ANIM_BOSS_COMBO_EMPTY = 0;
}
