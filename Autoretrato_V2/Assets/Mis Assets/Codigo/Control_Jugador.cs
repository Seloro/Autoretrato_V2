using UnityEngine;
using UnityEngine.UI;

public class Control_Jugador : MonoBehaviour
{
    public ParticleSystem sangre;
    public Image bolsaDeSangre;
    public float sangrado, vida;

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
        DescativarObjetosSeleccionados();
        ManejarParticulasYVida();
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
        sangrado -= valor;
    }

    void ManejarParticulasYVida()
    {
        vida -= Time.deltaTime * sangrado;

        bolsaDeSangre.fillAmount = vida / 100;

        var emicion = sangre.emission;

        emicion.rateOverTime = sangrado;
    }
}
