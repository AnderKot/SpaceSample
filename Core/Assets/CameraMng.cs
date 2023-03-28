using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleSpace;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.PlayerInput;

public class CameraMng : MonoBehaviour
{
    public PlayerInput Imput;
    private ICraftMng CurrCraftMng;

    private ActionEvent Move;
    private ActionEvent Fire;
    private ActionEvent Rotare;
    private ActionEvent Brake;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCurrCraftControls();
    }

    public void SetCraft(ICraftMng craftMng)
    {

        CurrCraftMng = craftMng;
    }

    private void UpdateCurrCraftControls()
    {
        
        CurrCraftMng = GameObject.FindGameObjectWithTag("Player").GetComponent<ICraftMng>();
        transform.SetParent(CurrCraftMng.GetCameraRoot());
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        Move = Imput.actionEvents[Imput.actionEvents.IndexOf(Cevent => Cevent.actionName.Contains("Player/Move"))];
        Fire = Imput.actionEvents[Imput.actionEvents.IndexOf(Cevent => Cevent.actionName.Contains("Player/Fire"))];
        Rotare = Imput.actionEvents[Imput.actionEvents.IndexOf(Cevent => Cevent.actionName.Contains("Player/Rotare"))];
        Brake = Imput.actionEvents[Imput.actionEvents.IndexOf(Cevent => Cevent.actionName.Contains("Player/Brake"))];

        Move.RemoveAllListeners();
        Move.AddListener(CurrCraftMng.Move);
        Fire.RemoveAllListeners();
        Fire.AddListener(CurrCraftMng.Fire);
        Rotare.RemoveAllListeners();
        Rotare.AddListener(CurrCraftMng.Rotare);
        Brake.RemoveAllListeners();
        Brake.AddListener(CurrCraftMng.Brake);
    }
}
