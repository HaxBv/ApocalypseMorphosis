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