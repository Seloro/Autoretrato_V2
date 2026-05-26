using UnityEngine;

public class Gestor_De_Manchas : MonoBehaviour
{
    public float sangrado, tiempo;

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
                break;
            }
        }
    }

    void AumentarSangrado(float valor)
    {
        sangrado += valor;

        if (sangrado < 0)
            sangrado = 0;
    }
}
