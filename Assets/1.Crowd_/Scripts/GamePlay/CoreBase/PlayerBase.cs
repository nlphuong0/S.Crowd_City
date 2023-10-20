using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class PlayerBase : CharacterBase
{
    #region Variables

    [TitleGroup("INFO CHARACTER")] [SerializeField]
    protected int _weaponlengh;

    [TitleGroup("INFO CHARACTER")] [SerializeField]
    protected float _roationWeapon;

    protected PlayerInfo _dataPlayer;

    protected WeaponController _weaponController;

    #endregion

    #region Properties

    public PlayerInfo PlayerInfo
    {
        get => _dataPlayer;
        set => _dataPlayer = value;
    }

    public int WeaponLengh
    {
        get => _weaponlengh;
        set => _weaponlengh = value;
    }

    public float RotationWeapon
    {
        get => _roationWeapon;
        set => _roationWeapon = value;
    }

    public WeaponController WeaponController
    {
        get => _weaponController;
        set => _weaponController = value;
    }

    #endregion

    public override void Init()
    {
        base.Init();
        SetInfo();
    }

    public virtual void SetInfo()
    {
        HP = PlayerInfo.hp;
        maxHp = PlayerInfo.hp;
        DAMAGE = PlayerInfo.damage;
        SPEED = PlayerInfo.speed;
        WeaponLengh = PlayerInfo.weaponLengh;
        RotationWeapon = PlayerInfo.rotationWeapon;
    }
}