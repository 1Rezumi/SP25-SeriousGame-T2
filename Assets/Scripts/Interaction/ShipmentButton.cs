using UnityEngine;
using UnityEngine.UI;

public class ShipmentButton : MonoBehaviour
{
    public int shipmentIndex;
    public Button button;

    private ShipmentManager shipmentManager;

    private void Start()
    {
        shipmentManager = FindObjectOfType<ShipmentManager>();

        if (shipmentManager == null)
        {
            Debug.LogError("ShipmentManager not found in scene.");
            return;
        }

        button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        if (shipmentManager != null)
        {
            shipmentManager.TrySubmitShipment(shipmentIndex);
        }
    }
}
