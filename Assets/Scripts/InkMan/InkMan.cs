using UnityEngine;
using UnityEngine.Rendering;

public class InkMan : PlayerInputs, IDamagable
{
    private float recargaEnergia = 10f;

    private float attackCooldown = 0;

    public float RangeAttack;

    public GameObject Ataque;
    void Start()
    {
       
    }

    void Update()
    {
        Passive();
        MovementMechanic();
        AttackCooldown();
    }
    public override void Passive()
    {
        GameManager.Instance.EnergiaActual += recargaEnergia * Time.deltaTime;
    }
    
    public override void Ability1()
    {
        Debug.Log("Ability1");
    }


    public override void Ability2()
    {
        Debug.Log("Ability2");

    }

    public override void Definitiva()
    {
        Debug.Log("Ultimate");


    }

    public override void AttackCooldown()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }
    public override void Atacar()
    {
        if (attackCooldown > 0f)
            return;

        float direccion = Mathf.Sign(transform.localScale.x);
        Vector3 posicionFrente = transform.position + new Vector3(RangeAttack * direccion, 0, 0);

        Instantiate(Ataque, posicionFrente, Quaternion.identity);
        Debug.Log("Ataco");


        attackCooldown = 1f / stats.currentSpeedAttack;
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

   
}
