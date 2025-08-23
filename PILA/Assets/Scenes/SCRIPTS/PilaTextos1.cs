using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PilaTextos : MonoBehaviour
{

    public Stack<string> pilaString = new Stack<string>();

    public TMP_InputField inputAccion;

    public TMP_Text pilaText;

    public TMP_Text mensajeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //PushString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushString()
    {
        string dato = inputAccion.text;

        pilaString.Push(dato);
        mensajeText.text = "El dato apilado es " + dato;

        ActualizarTextoPila();
    }

    public void PopString()
    {
        if (pilaString.Count > 0)
        {
            string elemento = pilaString.Pop();
            mensajeText.text = "Este es el elemento desapilado " + elemento;
            ActualizarTextoPila();
        }
        else
        {
            mensajeText.text = "Pila Vacia no puedo extraer elementos ";
        }

    }

    public void PeekString()
    {
        mensajeText.text = "Este es el elemento del tope de la pila " + pilaString.Peek();
    }

    public void clear()
    {
        pilaString.Clear();

        mensajeText.text = "Pila Vacia";
    }

    public void ActualizarTextoPila()
    {
        string mostrar = "";

        foreach (var item in pilaString)
        {
            mostrar += item.ToString()+"\n";
        }
        pilaText.text = mostrar;
    }
}
