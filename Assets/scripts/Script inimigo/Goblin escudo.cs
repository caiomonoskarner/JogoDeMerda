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

    // Start is called before the first frame update
    void Start()
    {
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorJogo>();
        ImagemEscudo = GetComponent<SpriteRenderer>();
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
        if(collision.gameObject.tag == "Ataque")
        {
            life--;
            if (life <= 0)
            {
                Destroy(this.gameObject);
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.tag == "Ataque")
        {
            if (Pode_Dano == true)
            {
                life--;
                Pode_Dano = false;
                GoblinEscudo.color = UnityEngine.Color.red;
                if (life <= 0)
                {
                    Destroy(this.gameObject);
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
            GoblinEscudo.color = UnityEngine.Color.white;
        }
    }
}   