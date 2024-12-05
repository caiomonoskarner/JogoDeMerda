using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorJogo : MonoBehaviour
{

    public bool GameLigado = true;


    // Start is called before the first frame update
    void Start()
    {
        GameLigado = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool EstadoDoJogo()
    {
        return GameLigado;
    }
    public void LigarJogo()
    {
        GameLigado = true;
        Time.timeScale = 1;
    }
}
