using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class ColaPersonas : MonoBehaviour
{
    [Header("Botones")]
    [SerializeField] Button ButtonENQUEUE;
    [SerializeField] Button ButtonDEQUEUE;
    [SerializeField] Button ButtonPEEK;
    [SerializeField] Button ButtonCLEAR;

    [Header("Entradas")]
    [SerializeField] TMP_InputField InputNombre;
    [SerializeField] TMP_InputField InputCorreo;
    [SerializeField] TMP_InputField InputEdad;

    [Header("Textos")]
    [SerializeField] TMP_Text TextInformacion; // mensajes de acción
    [SerializeField] TMP_Text TextDATOS;       // muestra la cola completa

    // Cola de personas
    Queue<Persona> cola = new Queue<Persona>();

    void Start()
    {
        if (ButtonENQUEUE) ButtonENQUEUE.onClick.AddListener(EnqueuePersona);
        if (ButtonDEQUEUE) ButtonDEQUEUE.onClick.AddListener(DequeuePersona);
        if (ButtonPEEK) ButtonPEEK.onClick.AddListener(PeekPersona);
        if (ButtonCLEAR) ButtonCLEAR.onClick.AddListener(ClearQueue);

        UpdateUI();
    }

    void OnDestroy()
    {
        if (ButtonENQUEUE) ButtonENQUEUE.onClick.RemoveListener(EnqueuePersona);
        if (ButtonDEQUEUE) ButtonDEQUEUE.onClick.RemoveListener(DequeuePersona);
        if (ButtonPEEK) ButtonPEEK.onClick.RemoveListener(PeekPersona);
        if (ButtonCLEAR) ButtonCLEAR.onClick.RemoveListener(ClearQueue);
    }

    // -------- Operaciones de Cola --------
    public void EnqueuePersona()
    {
        string nombre = InputNombre ? InputNombre.text.Trim() : "";
        string correo = InputCorreo ? InputCorreo.text.Trim() : "";
        string edadTexto = InputEdad ? InputEdad.text.Trim() : "";

        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(edadTexto))
        {
            SetInfo("Completa todos los campos antes de insertar.");
            return;
        }

        if (!int.TryParse(edadTexto, out int edad))
        {
            SetInfo("La edad debe ser un número válido.");
            return;
        }

        Persona p = new Persona(nombre, correo, edad);
        cola.Enqueue(p);
        SetInfo($"Enqueue: {p.Nombre}, {p.Correo}, {p.Edad} años");

        // limpiar inputs
        if (InputNombre) InputNombre.text = "";
        if (InputCorreo) InputCorreo.text = "";
        if (InputEdad) InputEdad.text = "";

        UpdateUI();
    }

    public void DequeuePersona()
    {
        if (cola.Count == 0)
        {
            SetInfo("Cola vacía. No hay nada que atender.");
            return;
        }

        Persona p = cola.Dequeue();
        SetInfo($"Dequeue: {p.Nombre}, {p.Correo}, {p.Edad} años");
        UpdateUI();
    }

    public void PeekPersona()
    {
        if (cola.Count == 0)
        {
            SetInfo("Cola vacía. No hay primero en la fila.");
            return;
        }

        Persona p = cola.Peek();
        SetInfo($"Peek (primero): {p.Nombre}, {p.Correo}, {p.Edad} años");
    }

    public void ClearQueue()
    {
        cola.Clear();
        SetInfo("Cola vaciada.");
        UpdateUI();
    }

    // -------- UI --------
    void UpdateUI()
    {
        if (!TextDATOS) return;

        if (cola.Count == 0)
        {
            TextDATOS.text = "COLA (primero -> último)\n\n(la cola está vacía)";
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine("COLA (primero -> último)");
        foreach (var p in cola)
            sb.AppendLine($"- {p.Nombre} | {p.Correo} | {p.Edad} años");

        TextDATOS.text = sb.ToString();
    }

    void SetInfo(string m)
    {
        if (TextInformacion) TextInformacion.text = m;
        Debug.Log(m);
    }
}

// ----- Clase Persona -----
[System.Serializable]
public class Persona
{
    public string Nombre;
    public string Correo;
    public int Edad;

    public Persona(string nombre, string correo, int edad)
    {
        Nombre = nombre;
        Correo = correo;
        Edad = edad;
    }
}
