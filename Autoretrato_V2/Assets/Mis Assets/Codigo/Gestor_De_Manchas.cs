using UnityEngine;

public class Gestor_De_Manchas : MonoBehaviour
{
    public float sangrado, tiempo;
    public float maxActivaciones;

    private int activacionesRealizadas;
    private float temp;

    private void Start()
    {
        Capsulas.Enviar += AumentarSangrado;
    }

    private void OnDestroy()
    {
        Capsulas.Enviar -= AumentarSangrado;
    }

    void Update()
    {
        temp += Time.deltaTime * sangrado;

        if (temp >= tiempo)
        {
            temp = 0f;
            ActivarHijoDesactivado();
        }
    }

    void ActivarHijoDesactivado()
    {
        foreach (Transform hijo in transform)
        {
            if (!hijo.gameObject.activeSelf)
            {
                hijo.gameObject.SetActive(true);
                activacionesRealizadas++;
                break;
            }
        }
    }

    void AumentarSangrado(float valor)
    {
        sangrado += valor;
        maxActivaciones += valor;

        if (maxActivaciones > 10)
            maxActivaciones = 10;
    }
}
