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
    void Atacar(GameObject Target);
}

public interface IAllAbilities
{
    void Pasive();
    void Ability1();
    void Ability2();
    void Definitiva();
    void OutOfControl();
}
