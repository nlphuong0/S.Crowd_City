using System;
using Sirenix.OdinInspector;

[Serializable]
public class DataCharacterBase
{
    public int hp;
    public float damage;
    public float speed;

    public DataCharacterBase(int hp, float damage, float speed)
    {
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
    }


}