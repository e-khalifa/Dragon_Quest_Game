using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [Header("Selection Options")]
    [SerializeField] private RectTransform[] options;

    [Header("SFX")]
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip selectedSound;


    private RectTransform rect;
    private int currentPosition;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //Interacting with options
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter))
            Interact();

    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;
        if (_change != 0)
            SoundManager.instance.PlaySound(buttonSound);

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y - 10, 0);
    }
    private void Interact()
    {
        SoundManager.instance.PlaySound(selectedSound);
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }

}
