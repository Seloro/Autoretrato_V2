using UnityEngine;
using UnityEngine.UI;

public class Manchas_De_Sangre : MonoBehaviour
{
    public float velocidad;
    public RectTransform rectTransform;

    float velocidadFinal;
    Image imagen;
    Color sangre;

    private void Start()
    {
        imagen = GetComponent<Image>();

        sangre = imagen.color;
    }

    private void OnEnable()
    {
        sangre.a = 1;

        velocidadFinal = velocidad * Random.Range(0.75f, 1.25f);

        ColocarEnPosicionAleatoria();
    }

    private void Update()
    {
        if (sangre.a > 0)
            sangre.a -= Time.deltaTime * velocidadFinal;
        else
            gameObject.SetActive(false);

        imagen.color = sangre;
    }

    void ColocarEnPosicionAleatoria()
    {
        rectTransform.localPosition = new Vector3(Random.Range(-800f, 800f), Random.Range(-400, 400), 0);
    }
}
