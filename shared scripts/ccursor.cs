using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ccursor : MonoBehaviour
{
    // Cursor related script. It is used to change the cursor and the color of the object on which the cursor is on, while entering, exiting, and on the object.
    //For example: Hovering over the letter A turns the letter A gray and the cursor changes from arrow view to hand view.


    public Texture2D cursorArrow;
    public Texture2D cursorHand;
    public Texture2D cursorForText;
    public static Color grayColor = new Color(0.1254902f, 0.1254902f, 0.1254902f, 1);
    Color blackColor = new Color(0.01568628f, 0.01568628f, 0.01568628f, 1);

    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.one, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        if (gameObject.name == "GeneralCanvas")
            Cursor.SetCursor(cursorArrow, Vector2.one, CursorMode.Auto);
        else if (gameObject.transform.parent.tag == "PausePanel" || gameObject.tag == "Slider")
        {
            Cursor.SetCursor(cursorArrow, Vector2.one, CursorMode.Auto);
        }
        else if (!EscapeKey.GameIsPaused)
        {
            Cursor.SetCursor(cursorArrow, Vector2.one, CursorMode.Auto);
            if (gameObject.tag == "Letter")
                gameObject.GetComponent<Text>().color = grayColor;
        }
    }

    private void OnMouseOver()
    {
        if (gameObject.name == "GeneralCanvas")
            Cursor.SetCursor(cursorArrow, Vector2.one, CursorMode.Auto);
        else if (gameObject.transform.parent.tag == "PausePanel" || gameObject.tag == "Slider")
        {
            Cursor.SetCursor(cursorHand, Vector2.one, CursorMode.Auto);
        }
        else if (!EscapeKey.GameIsPaused)
        {
            if (gameObject.name == "InputField")
                Cursor.SetCursor(cursorForText, Vector2.one, CursorMode.Auto);
            else
            {
                Cursor.SetCursor(cursorHand, Vector2.one, CursorMode.Auto);
                if (gameObject.tag == "Letter")
                    gameObject.GetComponent<Text>().color = blackColor;
            }
        }
    }

}
