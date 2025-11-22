using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damage);
}
public interface IInteractuable
{
    public void Interact(GameObject observer);
}

public interface IAtacar
{
    void Atacar();
    void AttackCooldown();
}

public interface IAbilities
{
    void Passive();
    void Ability1();
    void Ability2();
    void Definitiva();
    void OutOfControl();
}

public interface IRechargeAbility
{
    void Recharge();
}

