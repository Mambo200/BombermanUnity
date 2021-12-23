using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MyPlayerController : BasePlayerController
{
    [SerializeField]
    [Tooltip("Speed of character")]
    private float m_Speed = 5;
    protected CharacterController m_CharacterController;

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = GetInput();
        m_CharacterController.SimpleMove(movement * m_Speed);

        //Bomb
        if (Input.GetKeyDown(KeyCode.E))
            PlaceBomb();
    }

    /// <summary>
    /// Get input
    /// </summary>
    /// <returns></returns>
    protected Vector3 GetInput()
    {
        float LR = Input.GetAxisRaw("Horizontal");
        float UD = Input.GetAxisRaw("Vertical");

        return new Vector3(LR, 0, UD);
    }
}
