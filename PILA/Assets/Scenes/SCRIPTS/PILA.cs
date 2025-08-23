using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class Arbolejecucion : MonoBehaviour
{
    [SerializeField] Button ButtonPUSH;
    [SerializeField] Button ButtonPOP;
    [SerializeField] Button ButtonPEEK;
    [SerializeField] Button ButtonCLEAR;
    [SerializeField] TMP_InputField InputFieldTMP;
    [SerializeField] TMP_Text TextInformacion; // texto inferior (mensajes)
    [SerializeField] TMP_Text TextDATOS;       // texto central (muestra la pila)

    Stack<string> pila = new Stack<string>();

    void Start()
    {
        if (ButtonPUSH != null) ButtonPUSH.onClick.AddListener(Push);
        if (ButtonPOP != null) ButtonPOP.onClick.AddListener(Pop);
        if (ButtonPEEK != null) ButtonPEEK.onClick.AddListener(Peek);
        if (ButtonCLEAR != null) ButtonCLEAR.onClick.AddListener(ClearStack);
        if (InputFieldTMP != null) InputFieldTMP.onSubmit.AddListener(OnSubmit);
        UpdateUI();
    }

    void OnDestroy()
    {
        if (ButtonPUSH != null) ButtonPUSH.onClick.RemoveListener(Push);
        if (ButtonPOP != null) ButtonPOP.onClick.RemoveListener(Pop);
        if (ButtonPEEK != null) ButtonPEEK.onClick.RemoveListener(Peek);
        if (ButtonCLEAR != null) ButtonCLEAR.onClick.RemoveListener(ClearStack);
        if (InputFieldTMP != null) InputFieldTMP.onSubmit.RemoveListener(OnSubmit);
    }

    // Operaciones de pila
    public void Push()
    {
        string s = InputFieldTMP != null ? InputFieldTMP.text.Trim() : "";
        if (string.IsNullOrEmpty(s))
        {
            if (TextInformacion != null) TextInformacion.text = "Escribe algo antes de hacer Push.";
            return;
        }
        pila.Push(s);
        if (TextInformacion != null) TextInformacion.text = "Push: '" + s + "'";
        if (InputFieldTMP != null) InputFieldTMP.text = "";
        UpdateUI();
    }

    public void Pop()
    {
        if (pila.Count == 0)
        {
            if (TextInformacion != null) TextInformacion.text = "La pila esta vacia. No hay nada que desapilar.";
            return;
        }
        string elem = pila.Pop();
        if (TextInformacion != null) TextInformacion.text = "Pop: '" + elem + "'";
        UpdateUI();
    }

    public void Peek()
    {
        if (pila.Count == 0)
        {
            if (TextInformacion != null) TextInformacion.text = "La pila esta vacia. No hay tope.";
            return;
        }
        if (TextInformacion != null) TextInformacion.text = "Tope: '" + pila.Peek() + "'";
    }

    public void ClearStack()
    {
        pila.Clear();
        if (TextInformacion != null) TextInformacion.text = "Pila vaciada.";
        UpdateUI();
    }

    // Actualiza la vista de la pila usando solo caracteres ASCII
    void UpdateUI()
    {
        if (TextDATOS == null) return;

        if (pila.Count == 0)
        {
            TextDATOS.text = "PILA (tope -> fondo)\n\n(la pila esta vacia)";
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine("PILA (tope -> fondo)");
        foreach (var item in pila) sb.AppendLine("- " + item);
        TextDATOS.text = sb.ToString();
    }

    // Responde al evento onSubmit del TMP_InputField (Enter)
    void OnSubmit(string s)
    {
        Push();
    }
}
