using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisãoTrue : MonoBehaviour
{
    private bool essaREsposta = false;
    public static int Sobreviveu = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sobreviveu = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // Verifica se há toque na tela
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Apenas quando o toque começa
            if (touch.phase == TouchPhase.Began)
            {
                // Converte a posição do toque para um raio na cena
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Verifica se o raio colide com este objeto
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        if (essaREsposta != AnimacaoDosPersonagens.respostaAtual)
                        {
                            Sobreviveu = 1;
                            SceneManager.LoadScene(1);
                        }
                        else
                        {
                            Sobreviveu = 2;
                        }
                    }
                }
            }
        }
    }
    private void OnMouseDown()
    {
        if (essaREsposta != AnimacaoDosPersonagens.respostaAtual)
        {
            Sobreviveu = 1;
            SceneManager.LoadScene(1);

        }
        else
        {
            Sobreviveu = 2;
        }
    }
}
