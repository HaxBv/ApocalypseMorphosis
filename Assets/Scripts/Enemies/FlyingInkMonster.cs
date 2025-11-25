using UnityEngine;

public class FlyingInkMonster : EnemyBase
{


    //private void OnCollisionEnter2D(Collision2D collision)
   // {
      //  if
    //}
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
