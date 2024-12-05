using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Personagem : MonoBehaviour
{
    public float velocidade;
    private Rigidbody2D Corpo;
    public int vida = 3;
    private float meuTempoDano = 0;
    private bool Pode_Dano = true;
    public SpriteRenderer ImagemPersonagem;
    public int qtd_pulo = 0;
    private Image BarraDano;
    public int moedas = 0;
    public Text MoedaTexto;
    private GerenciadorJogo GJ;
    private Animator Animacao;
    public int chances = 3;
    public Text ChanceText;
    public Vector3 posInicial;
    public GameObject Ataque;
    public AnimacaoPlayer PlayerAnim;
    void Start()
    {
        posInicial = new Vector3(-2, 1.8f, 0);
        transform.position = posInicial;
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorJogo>();
        Animacao = GetComponent<Animator>();
        Corpo = GetComponent<Rigidbody2D>();
        BarraDano = GameObject.FindGameObjectWithTag("VidaJogador").GetComponent<Image>();
        MoedaTexto = GameObject.FindGameObjectWithTag("TextoPraMoeda").GetComponent<Text>();
        ChanceText = GameObject.FindGameObjectWithTag("Chance").GetComponent<Text>();
    }

    void Update()
    {
        if (GJ.EstadoDoJogo() == true)
        {
            Mover();
            Dano();
            Pular();
            Atacar();
        }
    }
    void Mover()
    {
        velocidade = Input.GetAxis("Horizontal") * 5;
        Corpo.velocity = new Vector2(velocidade, Corpo.velocity.y);
        if (velocidade != 0)
        {
            Animacao.SetBool("Andando", true);
        }
        else
        {
            Animacao.SetBool("Andando", false);
        }
        if (Corpo.velocity.y > 1)
        {
            Animacao.SetBool("ForaDoChao", true);
        }
        if (Corpo.velocity.y < -2)
        {
            Animacao.SetBool("ForaDoChao", true);
        }
        Virar();
    }

    void Virar()
    {
        if (velocidade > 0)
        {
            ImagemPersonagem.flipX = false;
        }
        else if (velocidade < 0)
        {
            ImagemPersonagem.flipX = true;
        }
    }
    void Pular()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            qtd_pulo++;
            if (qtd_pulo <= 1)
            {
                acaoPulo();
            }
        }
    }
    void acaoPulo()
    {
        Corpo.AddForce(transform.up * 300f);
    }
    void OnTriggerStay2D(Collider2D gatilho)
    {
        if (gatilho.gameObject.tag == "Pisavel")
        {
            Animacao.SetBool("ForaDoChao", false);
        }
    }
    void OnTriggerEnter2D(Collider2D gatilho)
    {
        if(gatilho.gameObject.tag == "Pisavel")
        {
            qtd_pulo = 0;
        }
        if (gatilho.gameObject.tag == "Moeda")
        {
            Destroy(gatilho.gameObject);
            moedas++;
            MoedaTexto.text = moedas.ToString();
        }
        if (gatilho.gameObject.tag == "MorteHora")
        {
            if (Pode_Dano == true)
            {
                Pode_Dano = false;
                vida = vida - 20;
                PerderVida();
                Morrer();
            }
        }
        if (gatilho.gameObject.tag == "Checkpoint")
        {
            posInicial = gatilho.gameObject.transform.position;
            Destroy(gatilho.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.tag == "Inimigo")
        {
            if (Pode_Dano == true)
            {
                vida--;
                PerderVida();
                Pode_Dano = false;
                ImagemPersonagem.color = UnityEngine.Color.red;
                if (vida <= 0)
                {
                    Morrer();
                }
            }
        }
    }
    void Dano()
    {
        if (Pode_Dano == false)
        {
            TemporizadorDano();
        }
    }
    void TemporizadorDano()
    {
        meuTempoDano += Time.deltaTime;
        if (meuTempoDano > 0.5f)
        {
            Pode_Dano = true;
            meuTempoDano = 0;
            ImagemPersonagem.color = UnityEngine.Color.white;
        }
    }
    void PerderVida()
    {
        int vida_parabarra = vida * 5;
        BarraDano.rectTransform.sizeDelta = new Vector2(vida_parabarra, 10);
    }
    void Morrer()
    {
        chances--;
        ChanceText.text = "Vidas: " + chances.ToString();
        if (chances <= 0)
        {
            Reiniciar();
        }
        else
        {
            Inicializar();
        }
    }
    void Inicializar()
    {
        transform.position = posInicial;
        vida = 10;
        int vida_parabarra = vida * 5;
        BarraDano.rectTransform.sizeDelta = new Vector2(vida_parabarra, 10);
    }
    void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }

    private float tempoAtaque = 0.5f; // Duração da animação de ataque

    void Atacar()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerAnim.PlayAnimation("Ataque");
            AtivaEspada();
            Invoke("DesativaEspadaEPararAnimacao", tempoAtaque);
        }
    }
    void DesativaEspadaEPararAnimacao()
    {
        Ataque.SetActive(false);
        PlayerAnim.PlayAnimation("Correndo");
    }
    public void AtivaEspada()
    {
        Ataque.SetActive(true);
    }
}