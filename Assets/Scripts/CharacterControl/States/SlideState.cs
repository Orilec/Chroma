using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlideState : BaseCharacterState
{
    
    public SlideState(PlayerController player, InputReader input) : base(player, input) {}
    
    public override void Update()
    {
        OnSlide();
        OnJump();
    }

    public override void FixedUpdate()
    {
        Slide();
        _playerController.PlayerMove();
    }

    public override void OnExit()
    {
        //start coroutine for deceleration
    }

    private void Slide()
    {
        //acceleration function, less control on move
    }
    
    void OnSlide()
    {
        //check if slide button is released, stop Slide Timer
    }

    void OnJump()
    {
        //start SlidingJump Timer
    }
}
