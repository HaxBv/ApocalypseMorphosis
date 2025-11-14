using UnityEngine;
using UnityEngine.Rendering;

public class InkMan : PlayerInputs, IDamagable, IAtacar
{
    private float recargaEnergia = 10f;
    void Start()
    {
       
    }

    void Update()
    {
        Passive();
        MovementMechanic();
    }
    public override void Passive()
    {
        GameManager.Instance.EnergiaActual += recargaEnergia * Time.deltaTime;
    }
    
    public override void Ability1()
    {
        throw new System.NotImplementedException();
    }







    public override void Ability2()
    {
        throw new System.NotImplementedException();

    }

    public override void Definitiva()
    {
        throw new System.NotImplementedException();


    }

   


    public override void OutOfControl()
    {
        Debug.Log("Perdiste el control");
    }


   

    /*private void SetMoveAnimation(Vector2 vector)
    {
        print(vector);

        if (vector.x != 0)
            Controller.SetBool("isMoving", true);
        else
            Controller.SetBool("isMoving", false);

        if (vector.x < 0)
            Sprite.flipX = true;

        if (vector.x > 0)
            Sprite.flipX = false;
    }*/

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();

    }

    public void Atacar(GameObject Target)
    {
        throw new System.NotImplementedException();
    }
}
