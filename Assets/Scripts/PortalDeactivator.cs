using UnityEngine;

public class PortalDeactivator : MonoBehaviour
{
    [SerializeField] GameObject prehistoricPortal, medievalPortal, futurePortal;
    
    void Awake()
    {
        if(GlobalSave.Instance.portalOneActive == true)
        {
            prehistoricPortal.SetActive(true);
        }
        else
        {
            prehistoricPortal.SetActive(false);
        }
        
        if(GlobalSave.Instance.portalTwoActive == true)
        {
            medievalPortal.SetActive(true);
        }
        else
        {
            medievalPortal.SetActive(false);
        }

        if(GlobalSave.Instance.portalThreeActive == true)
        {
            futurePortal.SetActive(true);
        }
        else
        {
            futurePortal.SetActive(false);
        }
    }
}
