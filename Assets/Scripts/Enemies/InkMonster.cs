using UnityEngine;

public class InkMonster : EnemyBase
{
    public override void TakeDamage(int dmg)
    {
        // Aquí puedes meter partículas, animaciones, color rojo, etc
        Debug.Log("InkMonster recibió daño!");

        base.TakeDamage(dmg); // Usa la lógica del EnemyBase
    }

    protected override void Die()
    {
        // Efecto de muerte específico del InkMonster
        Debug.Log("InkMonster murió con efecto especial!");

        base.Die();
    }
}