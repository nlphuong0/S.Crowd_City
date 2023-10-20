using Sirenix.OdinInspector;
using UnityEngine;

public abstract class CharacterBase : FsmControllerBase, IAttack
{
    #region Variables

    [TitleGroup("INFO CHARACTER")] [SerializeField]
    protected int id;

    [SerializeField] protected int hp;

    [SerializeField] protected int maxHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected CharacterBase _attacker;

    #endregion

    #region Properties

    public int ID
    {
        get => id;
        set => id = value;
    }

    public int HP
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                ChangeState(TypeState.Die);
            }
        }
    }

    public int MAXHP
    {
        get => maxHp;
    }

    public float DAMAGE
    {
        get => damage;
        set => damage = value;
    }

    public float SPEED
    {
        get => speed;
        set => speed = value;
    }
// hướng di chuyển
    public Vector3 Direction { get; set; }

    public Transform _Transform
    {
        get => transform;
    }

    public CharacterBase AttackerCharacter
    {
        get => _attacker;
        set => _attacker = value;
    }

    #endregion

// hàm này xử lý nhận damage
    public virtual void Attack(int damage)
    {
        HP -= damage;
    }

// hàm này xử lý thằng nào là thằng Attack
    public virtual void Attacker(CharacterBase attacker)
    {
        AttackerCharacter = attacker;
    }
}