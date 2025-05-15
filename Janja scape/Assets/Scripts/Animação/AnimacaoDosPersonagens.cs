using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimacaoDosPersonagens : MonoBehaviour
{
    public GameObject player;
    public GameObject Jana;
    public GameObject playerDialogo;
    public GameObject JanaDialogo;
    public GameObject VerdadeOuFalso;


    [SerializeField]
    public string[] DialogosJanja;
    public string[] Tranzições;

    [SerializeField]
    public bool[] Resposta;

    public static bool respostaAtual;

    public Text[] BlocosDialogos;
    public float DelayEntreLetras = 0.1f;

    private bool janaEstaNoLocal = false;
    private float TrocaDeFalas = 2f;
    private bool HoraDaREsposta = false;
    private bool EntrarNoLoop = false;

    private int Acertos = 0;
    private List<int> HistoriasContadas = new List<int>();

    private int HistoriaAleatoria;
    private int TranziçõesAleatorias;
    private bool TarefaCOmprida;
        
    void Start()
    {
        player.GetComponent<Transform>();
        Jana.GetComponent<Transform>();
        playerDialogo.GetComponent<Transform>();

    }

    void Update()
    {
        if(EntrarNoLoop == false)
        {
            Animacao();
        }
        else
        {
            if (ColisãoFake.Sobreviveu == 2 || ColisãoTrue.Sobreviveu == 2)
            {
                
                if(Acertos < 5)
                {
                    Loop();
                }
                else
                {
                    SceneManager.LoadScene(2);
                }
                
            }
        }
        

        HistoriaAleatoria = Random.Range(0, DialogosJanja.Length);
        TranziçõesAleatorias = Random.Range(0, Tranzições.Length);
    }
    void Animacao()
    {

        //player animation
        if (player.transform.position.x < -8)
        {
            player.transform.Translate(Vector3.right * 0.02f);
        }
        else
        {
            if (janaEstaNoLocal == false) playerDialogo.gameObject.SetActive(true);
            if (playerDialogo.transform.position.x < -4 && janaEstaNoLocal == false)
            {
                playerDialogo.transform.Translate(Vector3.right * 0.03f);

            }
            else
            {
                if (janaEstaNoLocal == false)
                {
                    BlocosDialogos[0].text = "Ela está chegando";
                }

            }
        }

        if (player.transform.eulerAngles.z > 25)
        {
            player.transform.Rotate(0, 0, 1);
        }
        if (player.transform.eulerAngles.z > -25)
        {
            player.transform.Rotate(0, 0, -1);
        }

        //jana animation
        if (Jana.transform.position.y < -1.21)
        {
            Jana.transform.Translate(Vector3.up * 0.005f);
            JanaDialogo.gameObject.SetActive(true);

        }
        else
        {
            janaEstaNoLocal = true;
        }
        if (janaEstaNoLocal == true && JanaDialogo.transform.position.x > 4)
        {
            playerDialogo.gameObject.SetActive(false);
            BlocosDialogos[0].text = "";
            JanaDialogo.transform.Translate(Vector3.left * 0.03f);
            TarefaCOmprida = true;
        }
        else if (TarefaCOmprida == true)
        {
            BlocosDialogos[1].text = Tranzições[TranziçõesAleatorias];
            Invoke("TrocarFalasJana", TrocaDeFalas);
            Invoke("TrocarFalasPLayer", 6f);
            TarefaCOmprida = false;
        }

        if (VerdadeOuFalso.transform.position.x < -4 && HoraDaREsposta == true)
        {
            VerdadeOuFalso.transform.Translate(Vector3.right * 0.03f);
        }
        else if (ColisãoFake.Sobreviveu == 2 || ColisãoTrue.Sobreviveu == 2)
        {
            Acertos++;
            ColisãoTrue.Sobreviveu = 0;
            ColisãoFake.Sobreviveu = 0;
            EntrarNoLoop = true;
        }


    }
    void TrocarFalasJana()
    {

      BlocosDialogos[1].text = "";
        if (HistoriasContadas.Contains(HistoriaAleatoria))
        {
            while (HistoriasContadas.Contains(HistoriaAleatoria))
            {
                HistoriaAleatoria = Random.Range(0, DialogosJanja.Length);
            }
        }
        BlocosDialogos[1].text = DialogosJanja[HistoriaAleatoria];
      HistoriaAleatoria = BlocosDialogos[1].text.Length;
      respostaAtual = Resposta[HistoriaAleatoria];
      HistoriasContadas.Add(HistoriaAleatoria);
    }
    void TrocarFalasPLayer()
    {
        JanaDialogo.gameObject.SetActive(false);
        BlocosDialogos[1].text = "";
        VerdadeOuFalso.SetActive(true);
        HoraDaREsposta = true;

    }

    void Loop()
    {
        VerdadeOuFalso.SetActive(false);
        JanaDialogo.gameObject.SetActive(true);
        BlocosDialogos[1].text = "";
        BlocosDialogos[1].text = Tranzições[TranziçõesAleatorias];
        Invoke("TrocasDeFalaJanaLoop", 4f);
        Invoke("TrocasDeFalaVFLoop", 6f);
        if (ColisãoFake.Sobreviveu == 2 || ColisãoTrue.Sobreviveu == 2)
        {
            Acertos++;
            ColisãoTrue.Sobreviveu = 0;
            ColisãoFake.Sobreviveu = 0;
        }
    }
    void TrocasDeFalaVFLoop()
    {
        JanaDialogo.gameObject.SetActive(false);
        BlocosDialogos[1].text = "";
        VerdadeOuFalso.SetActive(true);
    }
    void TrocasDeFalaJanaLoop()
    {
        BlocosDialogos[1].text = "";
        if (HistoriasContadas.Contains(HistoriaAleatoria))
        {
            while (HistoriasContadas.Contains(HistoriaAleatoria))
            {
                HistoriaAleatoria = Random.Range(0, DialogosJanja.Length);
            }
        }
        BlocosDialogos[1].text = DialogosJanja[HistoriaAleatoria];
        respostaAtual = Resposta[HistoriaAleatoria];
        HistoriasContadas.Add(HistoriaAleatoria);
    }

}

