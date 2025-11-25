using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimations : MonoBehaviour
{
    public Animator controller;
    public SpriteRenderer sprite;

    public bool IsMoving;

    private void Awake()
    {
        controller = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        /*PlayerController.Instance.
            InputManager.OnAttackPerformed
            += SetAttackAnimation;
         */
        PlayerController.Instance.
            InputManager.OnAbility1Trigger
            += SetSkill1Animation;

        /* PlayerController.Instance.
             InputManager.OnAbility2Trigger
             += SetSkill2Animation;

         PlayerController.Instance.
             InputManager.OnDefinitivaTrigger
             += SetDefinitivaAnimation;
         */
    }

    /*private void SetAttackAnimation(PlayerInputs inputs)
    {
        throw new NotImplementedException();
    }
    */
    private void SetSkill1Animation()
    {
        controller.SetTrigger("OnSkill1");
        print("Skill1");
    }
    /* private void SetSkill2Animation(PlayerInputs inputs)
     {
         throw new NotImplementedException();
     }

     private void SetDefinitivaAnimation(PlayerInputs inputs)
     {
         throw new NotImplementedException();
     }*/

}