using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public Animator     anime;
    public Rigidbody2D  playerRigidBody;
    public int          forceJump;


    public bool         slide;

    //verifica o chao
    public Transform    groundCheck;
    public bool         grounded;
    public LayerMask    whatIsGround;

    //slide
    public float slideTemp;
    public float timeTemp;

    //colisor
    public Transform colisor;

    //audio
    public AudioSource audio;
    public AudioClip soundJump;
    public AudioClip soundSlide;

    //pontuacao
    public UnityEngine.UI.Text txtPontos;
    public static int pontuacao;

    // Use this for initialization
    void Start () {
        //Debug.Log("Teste");
        pontuacao = 0;
        PlayerPrefs.SetInt("pontuacao", pontuacao);
        //PlayerPrefs.SetInt("recorde", pontuacao);
    }
	
	// Update is called once per frame
	void Update () {

        txtPontos.text = pontuacao.ToString();

        //se o botao de pulo for apertado e tiver pisando no chao, entao faca isso
		if (Input.GetKeyDown("space") && grounded == true)
        {
            //Debug.Log("pulo");
            playerRigidBody.AddForce(new Vector2(0, forceJump));
             audio.PlayOneShot(soundJump);

            if(slide == true)
            {
                colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.3f, colisor.position.z);
                slide = false;
            }

        }

		if (Input.GetKeyDown(KeyCode.LeftAlt) && grounded == true && slide == false)
        {
            audio.PlayOneShot(soundSlide);
            //Debug.Log("slide");
            slide = true;
            timeTemp = 0;
            colisor.position = new Vector3(colisor.position.x, colisor.position.y - 0.3f, colisor.position.z);
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
        
        if (slide == true)
        {
            timeTemp += Time.deltaTime;
            if (timeTemp >= slideTemp)
            {
                colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.3f, colisor.position.z);
                slide = false;
            }
        }


        anime.SetBool("jump", !grounded);
        anime.SetBool("slide", slide);

    }

    void OnTriggerEnter2D()
    {
        //Debug.Log("bateu");
        PlayerPrefs.SetInt("pontuacao", pontuacao);
        if (pontuacao > PlayerPrefs.GetInt("recorde"))
        {
            PlayerPrefs.SetInt("recorde", pontuacao);
        }
        //Application.LoadLevel("gameover");
		SceneManager.LoadScene("gameover");
    }



}
