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
        Player = GameObject.FindGameObjectWithTag("Player");  // Refer�ncia ao jogador
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
        // Verifica se o inimigo est� sendo atingido por um ataque do jogador
        if (collision.gameObject.tag == "Ataque")
        {
            // Verifica se o ataque veio pela parte de tr�s do goblin
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

    // Verifica se o ataque veio pela parte de tr�s do goblin
    bool IsHitFromBehind()
    {
        // Calcula a dire��o do jogador em rela��o ao goblin
        float directionToPlayer = Player.transform.position.x - transform.position.x;

        // Verifica se o jogador est� atr�s do goblin (considerando a dire��o do goblin)
        if (directionToPlayer < 0 && ImagemEscudo.flipX)  // O jogador est� � esquerda e o goblin est� virado para a direita
        {
            return true;
        }
        else if (directionToPlayer > 0 && !ImagemEscudo.flipX)  // O jogador est� � direita e o goblin est� virado para a esquerda
        {
            return true;
        }

        return false;  // Caso contr�rio, o ataque n�o veio pela parte de tr�s
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
