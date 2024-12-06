using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class GoblinLancador : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletpos;
    private float timer;
    private GameObject Player;
    public int life = 3;
    private bool Pode_Dano = true;
    private float meuTempoDano = 0;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector3 scale = transform.localScale;
        if (Player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        if (distance < 8)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
    }
    void shoot()
    {
        Instantiate(bullet, bulletpos.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ataque")
        {
            life--;
            if (life <= 0)
            {
                Destroy(this.gameObject);
            }
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
