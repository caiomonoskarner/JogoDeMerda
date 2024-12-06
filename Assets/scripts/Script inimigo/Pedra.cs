using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D rb;
    public float force;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");

        // Dire��o da pedra em dire��o ao jogador
        Vector3 direction = Player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // Rotaciona a pedra para que ela olhe para a dire��o correta
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            Destroy(gameObject);  // Destr�i a pedra ap�s 5 segundos
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Quando a pedra colide com o jogador
        if (other.gameObject.CompareTag("Player"))
        {
            // Pega o script do personagem que gerencia a vida
            Personagem personagem = other.gameObject.GetComponent<Personagem>();
            if (personagem != null)
            {
                // Chama o m�todo para perder vida no personagem
                personagem.vida--;  // Reduz a vida do jogador
                personagem.PerderVida();  // Atualiza a barra de vida
                personagem.VerificarMorte();  // Verifica se o personagem morreu
            }
            Destroy(gameObject);  // Destr�i a pedra ap�s o impacto
        }
    }
}
