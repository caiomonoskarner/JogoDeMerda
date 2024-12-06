using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblinescudo : MonoBehaviour
{
    private SpriteRenderer ImagemEscudo;
    public float Velocidade = 0.1f;
    public float DistInicial;
    public float DistFinal;
    public Rigidbody2D Corpo;
    private GerenciadorJogo GJ;
    public int life = 3;
    private bool Pode_Dano = true;
    private float meuTempoDano = 0;
    public SpriteRenderer GoblinEscudo;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorJogo>();
        ImagemEscudo = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");  // Referência ao jogador
    }

    // Update is called once per frame
    void Update()
    {
        if (GJ.EstadoDoJogo() == true)
        {
            Andar();
        }
    }

    void Andar()
    {
        transform.position = new Vector3(transform.position.x + Velocidade, transform.position.y, transform.position.z);

        if (transform.position.x > DistFinal)
        {
            Velocidade = Velocidade * -1;
            ImagemEscudo.flipX = false;
        }
        if (transform.position.x < DistInicial)
        {
            Velocidade = Velocidade * -1;
            ImagemEscudo.flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o inimigo está sendo atingido por um ataque do jogador
        if (collision.gameObject.tag == "Ataque")
        {
            // Verifica se o ataque veio pela parte de trás do goblin
            if (IsHitFromBehind())
            {
                life--;
                if (life <= 0)
                {
                    Destroy(this.gameObject);  // Destroi o goblin quando a vida chega a 0
                }
            }
        }
    }

    // Verifica se o ataque veio pela parte de trás do goblin
    bool IsHitFromBehind()
    {
        // Calcula a direção do jogador em relação ao goblin
        float directionToPlayer = Player.transform.position.x - transform.position.x;

        // Verifica se o jogador está atrás do goblin (considerando a direção do goblin)
        if (directionToPlayer < 0 && ImagemEscudo.flipX)  // O jogador está à esquerda e o goblin está virado para a direita
        {
            return true;
        }
        else if (directionToPlayer > 0 && !ImagemEscudo.flipX)  // O jogador está à direita e o goblin está virado para a esquerda
        {
            return true;
        }

        return false;  // Caso contrário, o ataque não veio pela parte de trás
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
        }
    }
}
