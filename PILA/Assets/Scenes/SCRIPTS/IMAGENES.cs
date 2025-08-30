using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageStackManager : MonoBehaviour
{
    public GameObject imagePrefab;      // Prefab con un Image
    public Transform contentPanel;      // Panel con Vertical Layout Group
    public InputField inputField;       // Donde escribes el nombre de la imagen
    public Text peekText;               // Muestra el �ltimo agregado

    private Stack<GameObject> imageStack = new Stack<GameObject>();

    public void PushImage()
    {
        string imageName = inputField.text.Trim();
        if (string.IsNullOrEmpty(imageName))
        {
            Debug.LogWarning("Por favor escribe el nombre de la imagen.");
            return;
        }

        // Cargar imagen desde Resources
        Sprite sprite = Resources.Load<Sprite>("Images/" + imageName);
        if (sprite == null)
        {
            Debug.LogWarning("Imagen no encontrada en Resources/Images/");
            return;
        }

        // Instanciar el prefab
        GameObject newImage = Instantiate(imagePrefab, contentPanel);
        newImage.GetComponent<Image>().sprite = sprite;

        // Agregar a la pila
        imageStack.Push(newImage);

        // Actualizar Peek
        peekText.text = "�ltima imagen: " + imageName;

        inputField.text = ""; // Limpiar input
    }

    public void PopImage()
    {
        if (imageStack.Count > 0)
        {
            GameObject topImage = imageStack.Pop();
            Destroy(topImage);

            // Actualizar Peek
            peekText.text = imageStack.Count > 0 ? "�ltima imagen: " + imageStack.Peek().name : "Pila vac�a";
        }
        else
        {
            peekText.text = "Pila vac�a";
        }
    }

    public void PeekImage()
    {
        if (imageStack.Count > 0)
        {
            peekText.text = "�ltima imagen: " + imageStack.Peek().name;
        }
        else
        {
            peekText.text = "Pila vac�a";
        }
    }

    public void ClearAllImages()
    {
        foreach (GameObject img in imageStack)
        {
            Destroy(img);
        }
        imageStack.Clear();
        peekText.text = "Pila�vac�a";
    }
}
