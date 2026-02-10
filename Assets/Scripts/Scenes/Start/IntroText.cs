using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroText : MonoBehaviour
{
    private string[] lines = new string[]
    {
        "Eres una eminencia en la investigación y has descubierto el cadáver de Litto, un cantante con abundante riqueza.",
        "Una pirámide atravesó su tórax.",
        "Son las 23:05, el cuerpo aún no tiene rigor mortis.",
        "Ten cuidado con la niebla, ¡es tóxica y está en todas partes! En algunos lugares es más intensa que en otros.",
        "Consigue filtros para tu máscara y sobrevive. Averigua qué ha ocurrido exactamente.",
        "<i>Muévete con <b>WASD</b>. Presiona <b>E</b> para interactuar. Algunas opciones deben seleccionarse con el ratón. ¡Buena suerte!</i>"
    };

    private int currentLine = 0;
    private bool introActive = true;

    void Start()
    {
        ShowLine(currentLine);
    }

    void Update()
    {
        if (introActive && Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentLine++;
            if (currentLine < lines.Length)
            {
                ShowLine(currentLine);
            }
            else
            {
                // Última línea terminada: cerrar UI
                MainUIController.ConversationManager.EndConversation();
                introActive = false;

                // Habilitar el cambio de escena porque la intro ha terminado
                ChangeScene.CanChangeScene = true;
            }
        }
    }

    private void ShowLine(int index)
    {
        MainUIController.ConversationManager.SetConversationText(null, lines[index], false);
    }
}
