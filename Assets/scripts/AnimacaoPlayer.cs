using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPlayer : MonoBehaviour
{
    public Animator PlayerAnimator;
    public void PlayAnimation(string animationName)
    {
        PlayerAnimator.Play(animationName);
    }
}
