using UnityEngine;
using UnityEngine.UI;

public class Control_Jugador : MonoBehaviour
{
    public ParticleSystem sangre;
    public Image bolsaDeSangre;
    public float sangrado, recuperacion, vida;

    public delegate void EnviarCondisionPasiente(int valor);
    static public event EnviarCondisionPasiente Enviar;

    void Start()
    {
        Capsulas.Enviar += ModificarSangrado;
    }

    private void OnDestroy()
    {
        Capsulas.Enviar -= ModificarSangrado;
    }

    void Update()
    {
        if (vida > 0)
        {
            DescativarObjetosSeleccionados();
            ManejarParticulasYVida();
        }
    }

    void DescativarObjetosSeleccionados()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
                hit.collider.gameObject.SetActive(false);
        }
    }

    void ModificarSangrado(float valor)
    {
        sangrado += valor;

        if (sangrado <= 0)
            Enviar.Invoke(0);
    }

    void ManejarParticulasYVida()
    {
        vida += Time.deltaTime * recuperacion;
        vida -= Time.deltaTime * sangrado;

        bolsaDeSangre.fillAmount = vida / 100;

        var emicion = sangre.emission;

        emicion.rateOverTime = sangrado;

        if (vida <= 0)
            Enviar.Invoke(1);
    }
}
