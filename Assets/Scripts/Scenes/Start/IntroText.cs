using System.Collections;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(IntroText1());
    }

    private IEnumerator IntroText1()
    {
        MainUIController.ConversationManager.waitingNoSkip = false;
        MainUIController.ConversationManager.SetConversationText(null, "Eres una eminencia en la investigación y has descubierto el cadáver de Litto, un cantante con abundante riqueza.", false);

        MainUIController.ConversationManager.waitingNoSkip = true;
        yield return new WaitForSeconds(5f);
        MainUIController.ConversationManager.waitingNoSkip = false;
        MainUIController.ConversationManager.SetConversationText(null, "Una pirámide atravesó su tórax.", false);

        MainUIController.ConversationManager.waitingNoSkip = true;
        yield return new WaitForSeconds(3f);
        MainUIController.ConversationManager.waitingNoSkip = false;
        MainUIController.ConversationManager.SetConversationText(null, "Son las 23:05, el cuerpo aún no tiene rigor mortis!", false);

        MainUIController.ConversationManager.waitingNoSkip = true;
        yield return new WaitForSeconds(3.5f);
        MainUIController.ConversationManager.waitingNoSkip = false;
        MainUIController.ConversationManager.SetConversationText(null, "Ten cuidado con la niebla, es tóxica y está en todas partes. En algunos lugares es más intensa que en otros", false);

        MainUIController.ConversationManager.waitingNoSkip = true;
        yield return new WaitForSeconds(4f);
        MainUIController.ConversationManager.waitingNoSkip = false;
        MainUIController.ConversationManager.SetConversationText(null, "Consigue filtros para tu máscara para poder sobrevivir. Averigua qué ha ocurrido exactamente.", false);

        MainUIController.ConversationManager.waitingNoSkip = true;
        yield return new WaitForSeconds(4f);
        MainUIController.ConversationManager.waitingNoSkip = false;
        MainUIController.ConversationManager.SetConversationText(null, "<i>Muévete con <b>WASD</b>. Presiona <b>E</b> para interactuar. Algunas opciones deben seleccionarse con el ratón. Buena suerte!</i>", false);
    }
}
