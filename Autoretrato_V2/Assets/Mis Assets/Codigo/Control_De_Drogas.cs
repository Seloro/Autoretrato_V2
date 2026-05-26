using UnityEngine;
using System.Collections.Generic;

public class Control_De_Drogas : MonoBehaviour
{
    public float tiempo, sangrado;

    private List<GameObject> dorgas = new List<GameObject>();
    private float temp;

    void Start()
    {
        RevisarHijosDesactivados();

        Capsulas.Enviar += ModificarSangrado;
    }

    private void OnDestroy()
    {
        Capsulas.Enviar -= ModificarSangrado;
    }

    void Update()
    {
        temp += Time.deltaTime * sangrado;

        if (temp >= tiempo)
        {
            temp = 0f;
            ActivarHijoAleatorio();
        }

        RevisarHijosDesactivados();
    }

    public void RevisarHijosDesactivados()
    {
        foreach (Transform hijo in transform)
        {
            if (!hijo.gameObject.activeSelf)
            {
                dorgas.Add(hijo.gameObject);
            }
        }
    }

    private void ActivarHijoAleatorio()
    {
        if (dorgas.Count == 0) return;

        int indice = Random.Range(0, dorgas.Count);
        GameObject hijo = dorgas[indice];

        hijo.SetActive(true);
        dorgas.RemoveAt(indice);
    }

    void ModificarSangrado(float valor)
    {
        sangrado += valor;

        if (sangrado < 0)
            sangrado = 0;
    }
}
