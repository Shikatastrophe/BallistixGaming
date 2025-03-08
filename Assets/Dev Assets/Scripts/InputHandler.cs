using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static event Action<int, float, float> OnMove;
    //public static event Action<int> OnAbility;

    float horizontal, vertical;
    int id;
    PlayerInput input;


    void Start()
    {
        input = GetComponent<PlayerInput>();
        id = (int)input.user.index;
    }
    void Update()
    {
        OnMove?.Invoke(id, horizontal, vertical);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    public void Ability(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            //OnAbility.Invoke(id);
            Debug.Log("No ability :c");
        }
    }
}
