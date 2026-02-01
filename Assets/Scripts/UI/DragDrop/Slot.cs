using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    // DROP HANDLER---------------------------------------------------
    public void OnDrop(PointerEventData eventData)
    {
        print("OnDrop en Slot");
        GameObject dropped = eventData.pointerDrag; // Capturar el objeto sobre el que esta el puntero

        if (!dropped.TryGetComponent<DraggableObject>(out var newDraggableObject))
        {
            return; // Si no es draggable, no hacemos nada
        }

        if (IsDragableObjectNOTABalrog())
        {
            newDraggableObject.SetNewParent(transform); // Cambiar el parent para que contenga al objeto
            return;
        }

        // Pasar el hijo anterior a las sombras (al parent del nuevo draggable)
        DraggableObject currentDraggableObject = transform.GetChild(0).GetComponent<DraggableObject>();

        currentDraggableObject.ReturnsToTheShadows(newDraggableObject.ParentAfterDrag);

        newDraggableObject.SetNewParent(transform); // Cambiar el parent para que contenga al nuevo objeto
    }



    // HERRAMIENTAS---------------------------------------------------
    public bool IsDragableObjectNOTABalrog()
    {
        if (transform.childCount == 0)
        {
            return true;
        }
        // print("YOU SHALL NOT PASS!!!!!!!!!!");
        return false;

//
//                                                .''.'''
//                .                             .'   :
//                \\                          .:    :
//                 \\                        _:    :       ..----.._
//                  \\                    .:::.....:::.. .'         ''.
//                   \\                 .'  #-. .-######'     #        '.
//                    \\                 '.##' / ' ################       :
//                     \\                  #####################         :
//                      \\               ..##.-.#### .''''###'.._        :
//                       \\             :--:########:            '.    .' :
//                        \\..__...--.. :--:#######.'   '.         '.     :
//                        :     :  : : '':'-:'':'::        .         '.  .'
//                        '---'''..: :    ':    '..'''.      '.        :'
//                           \\  :: : :     '      ''''''.     '.      .:
//                            \\ ::  : :     '            '.      '      :
//                             \\::   : :           ....' ..:       '     '.
//                              \\::  : :    .....####\\ .~~.:.             :
//                               \\':.:.:.:'#########.===. ~ |.'-.   . '''.. :
//                                \\    .'  ########## \ \ _.' '. ' -.       '''.
//                                :\\  :     ########   \ \      '.  '-.        :
//                               :  \\'    '   #### :    \ \      :.    '-.      :
//                              :  .'\\   :'  :     :     \ \       :      '-.    :
//                             : .'  .\\  '  :      :     :\ \       :        '.   :
//                             ::   :  \\'  :.      :     : \ \      :          '. :
//                             ::. :    \\  : :      :    ;  \ \     :           '.:
//                              : ':    '\\ :  :     :     :  \:\     :        ..'
//                                 :    ' \\ :        :     ;  \|      :   .'''
//                                 '.   '  \\:                         :.''
//                                  .:..... \\:       :            ..''
//                                 '._____|'.\\......'''''''.:..'''
//                                            \\
    }
}
