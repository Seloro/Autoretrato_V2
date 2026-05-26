using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gestor_de_dialogo : MonoBehaviour
{
    public GameObject[] formas;
    public GameObject globoJugador, globoNPC;
    public TextMeshProUGUI textoJugador, textoNPC;
    public List<Contenedor_De_Dialogos> dialogosJugador;
    public List<Contenedor_De_Dialogos> dialogosNPC;
    public Transform enPantalla, fueraDePantalla;
    public float intervaloDeEspera, velocidad;
    public int maximoDeEventos, eventoInicio, eventoFin;

    int IndiceDeEvento, indiceDeDialogo, siguienteObjetivo;
    float tiempo, temp;
    bool fin;

    void Start()
    {
        Time.timeScale = 0;
        IndiceDeEvento = eventoInicio;
        tiempo = intervaloDeEspera * Random.Range(0.75f, 1.25f);

        Control_Jugador.Enviar += EstablecerFinal;
    }

    private void OnDestroy()
    {
        Control_Jugador.Enviar -= EstablecerFinal;
    }

    void Update()
    {
        Movimiento();

        if (!fin)
            Manager();
        else
            EventoFinal();

        GestorDeDialogo();
        EventoInicial();
    }

    void Movimiento()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, Time.deltaTime * velocidad);
    }

    void Manager()
    {
        if (transform.position == enPantalla.position)
        {
            temp += Time.deltaTime;

            globoJugador.SetActive(true);
            globoNPC.SetActive(true);

            siguienteObjetivo = 0;
        }
        else if (transform.position == fueraDePantalla.position)
        {
            temp += Time.deltaTime;

            formas[IndiceDeEvento].SetActive(false);

            siguienteObjetivo = 1;
        }

        if (temp >= tiempo)
        {
            temp = 0;
            tiempo = intervaloDeEspera * Random.Range(0.75f, 1.25f);

            if (siguienteObjetivo == 1)
            {
                transform.SetParent(enPantalla);
                IndiceDeEvento = Random.Range(0, maximoDeEventos);
                indiceDeDialogo = Random.Range(0, 4);
            }
            else
            {
                transform.SetParent(fueraDePantalla);
                globoJugador.SetActive(false);
                globoNPC.SetActive(false);
            }
        }
    }

    void GestorDeDialogo()
    {
        formas[IndiceDeEvento].SetActive(true);
        textoJugador.text = dialogosJugador[IndiceDeEvento].dialogos[indiceDeDialogo];
        textoNPC.text = dialogosNPC[IndiceDeEvento].dialogos[indiceDeDialogo];
    }

    void EventoInicial()
    {
        if (IndiceDeEvento == eventoInicio && Input.GetMouseButtonDown(0))
        {
            transform.SetParent(fueraDePantalla);
            globoJugador.SetActive(false);
            globoNPC.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void EstablecerFinal(int valor)
    {
        transform.SetParent(fueraDePantalla);
        globoJugador.SetActive(false);
        globoNPC.SetActive(false);
        indiceDeDialogo = valor;
        fin = true;
    }

    void EventoFinal()
    {
        if (transform.position == enPantalla.position)
        {
            Time.timeScale = 0;
            globoJugador.SetActive(true);
            globoNPC.SetActive(true);
        }
        else if (transform.position == fueraDePantalla.position)
        {
            transform.SetParent(enPantalla);
            formas[IndiceDeEvento].SetActive(false);
            IndiceDeEvento = eventoFin;
            formas[IndiceDeEvento].SetActive(false);
        }
    }
}

[System.Serializable]
public class Contenedor_De_Dialogos
{
    public List<string> dialogos = new List<string>();
}