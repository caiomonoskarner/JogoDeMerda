using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviour
{

    public bool GameLigado = true;
    public GameObject TelaGameOver;

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
    public void MortePlayer()
    {
        TelaGameOver.SetActive(true);
        GameLigado = false;
        Time.timeScale = 0;
    }
    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }
}
