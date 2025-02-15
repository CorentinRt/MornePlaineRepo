using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    #region Fields
    private static PlayerInteractions _instance;


    #endregion

    #region Properties
    public static PlayerInteractions Instance { get => _instance; set => _instance = value; }


    #endregion

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InteractGet();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }
    private void InteractGet()
    {
        IInteractible interactible = GetObjectUnderInteractionRaycast();

        if (interactible == null) return;

        if (!interactible.IsGettable()) return;

        interactible.InteractGet();
    }
    private void Interact()
    {
        IInteractible interactible = GetObjectUnderInteractionRaycast();

        if (interactible == null ) return;

        if (!interactible.IsInteractible())  return;

        interactible.Interact();
    }
    private IInteractible GetObjectUnderInteractionRaycast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            IInteractible interactible = hit.transform.gameObject.GetComponentInParent<IInteractible>();

            if (interactible == null) return null;

            return interactible;
        }

        return null;
    }
}
