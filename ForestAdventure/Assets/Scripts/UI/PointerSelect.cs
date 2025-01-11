using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    
    [SerializeField] private RectTransform[] options;
    private RectTransform rectangle;
    private int currentPosition;

    private void Awake()
    {
        rectangle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            ExecuteOption();

    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        rectangle.position = new Vector3(rectangle.position.x, options[currentPosition].position.y, 0);

    }

    private void ExecuteOption()
    {
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
