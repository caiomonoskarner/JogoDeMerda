using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimacao : MonoBehaviour
{
    public Animator Goblinanimacao;
    public void PlayAnimation(string animationName)
    {
        Goblinanimacao.Play(animationName);
    }
}
