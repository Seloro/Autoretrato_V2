using TMPro;
using UnityEngine;

public class Capsulas : MonoBehaviour
{
    public Color colo1, color2;
    public string referencia1, referencia2, formula;
    public TextMeshProUGUI nombre;
    public float efecto, velocidad, frecuencia, amplitud;

    Material mat;
    float posicionInicialX, direccion, velocidadReal, frecuenciaReal;
    bool permitido;

    public delegate void EnviarEfecto(float valor);
    static public event EnviarEfecto Enviar;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        mat.SetColor(referencia1, colo1);
        mat.SetColor(referencia2, color2);

        nombre.text = formula;

        Posicionamiento();
        permitido = true;
    }

    private void OnEnable()
    {
        Posicionamiento();
        permitido = true;
    }

    private void OnDisable()
    {
        if (permitido)
            Enviar.Invoke(efecto);
    }

    void Update()
    {
        Movimiento();

        if (transform.position.y < -6)
        {
            permitido = false;
            gameObject.SetActive(false);
        }
    }

    void Posicionamiento()
    {
        transform.position = new Vector3(Random.Range(-8, 9), 7, 1.5f);
        posicionInicialX = transform.position.x;
        direccion = Random.Range(0, 2) * 2 - 1;
        velocidadReal = velocidad * Random.Range(0.75f, 1.25f);
        frecuenciaReal = frecuencia * Random.Range(0.75f, 1.25f);
    }

    void Movimiento()
    {
        transform.position += Vector3.down * velocidadReal * Time.deltaTime;

        float x = (Mathf.Sin(Time.time * frecuenciaReal) * amplitud) * direccion;
        float z = (Mathf.Cos(Time.time * frecuenciaReal) * amplitud) * direccion;

        transform.position = new Vector3(posicionInicialX + x, transform.position.y, 1.5f + z);
    }
}
