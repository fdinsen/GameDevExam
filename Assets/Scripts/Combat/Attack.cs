
public class Attack
{
    private readonly int _damage;
    private readonly bool _critical;
    private readonly int _stunTime;

    public Attack(int damage, bool critical)
    {
        _damage = damage;
        _critical = critical;
        _stunTime = 0;
    }

    public Attack(int damage, bool critical, int stunTime)
    {
        _damage = damage;
        _critical = critical;
        _stunTime = stunTime;
    }

    public int Damage
    {
        get { return _damage; }
    }

    public bool IsCritical
    {
        get { return _critical; }
    }

    public int StunTime
    {
        get { return _stunTime; }
    }
}
