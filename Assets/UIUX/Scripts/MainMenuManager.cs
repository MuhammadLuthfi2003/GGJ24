using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]private List<RectTransform> MenuObjects;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float duration = 1f;
    public static MainMenuManager instances;
    private void Awake()
    {
        instances = this;
    }
    private void Start()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => AudioManager.instance.Play("Button")); 
        }
    }
    private void Update()
    {

    }
    public void MoveGroupObjects(int index)
    {
        MoveGroupObjects(MenuObjects[index]);
    }
    public void MoveGroupObjects(RectTransform targetObject)
    {
        Vector3 screenOffset= new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 position = targetObject.position - screenOffset;
        Vector3 moveTranslation = Vector3.zero - position;

        foreach (RectTransform obj in MenuObjects)
        {
            //obj.position += moveTranslation;
            StartCoroutine(MoveToTarget(moveTranslation, obj));
        }
    }
    IEnumerator MoveToTarget(Vector3 targetPosition, RectTransform targetObject)
    {
        // Get the initial position of the object
        Vector3 startPosition = targetObject.position;
        Vector3 translatedPosition = startPosition + targetPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate between the start and target positions based on the elapsed time
            targetObject.position = Vector3.Lerp(startPosition, translatedPosition, elapsedTime);

            // Increment the elapsed time based on the move speed
            elapsedTime += Time.deltaTime * moveSpeed;

            // Yield execution until the next frame
            yield return null;
        }

        // Ensure the object reaches the exact target position
        transform.position = targetPosition;
    }
}
