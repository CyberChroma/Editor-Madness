using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(NumberGuess))]
public class NumberGuessPropertyDrawer : PropertyDrawer
{
    // An int to store the current number guess
    int savedGuess;

    // Information on the different styles of text
    GUIStyle styleCentered;
    GUIStyle styleBoldCentered;

    // Rects for the different GUI elements to be drawn
    Rect guessANumberRect;
    Rect wrongNumberRect;
    Rect enumRect;

    // Reference to the number guess enum
    SerializedProperty numberEnum;

    // Reference to the parent of the number text objects
    static Transform numberTextsParent;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Setting up the GUI styles
        styleCentered = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        styleBoldCentered = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };

        // Setting up the different Rects
        guessANumberRect = new Rect(Screen.width / 2 - position.width / 4, position.y, position.width / 4, position.height);
        enumRect = new Rect(Screen.width / 2, position.y, position.width / 4, position.height);

        // Making the guess a number label
        GUI.Label(guessANumberRect, "Guess a number!", styleCentered);

        // Getting the number guess enum, displaying it, and saving it's new value
        numberEnum = property.FindPropertyRelative("number");
        numberEnum.enumValueIndex = savedGuess;
        EditorGUI.PropertyField(enumRect, numberEnum, new GUIContent(""));
        savedGuess = numberEnum.enumValueIndex;

        // Getting the reference
        numberTextsParent = GameObject.Find("Number Texts Panel").transform;

        // Setting the correct text to be active, and the rest to be false
        foreach (Transform numberText in numberTextsParent) {
            if (numberText.name == savedGuess.ToString()) {
                numberText.gameObject.SetActive(true);
            }
            else {
                numberText.gameObject.SetActive(false);
            }
        }

        // If the user has 'guessed' the correct number (3), display the continue button
        if (savedGuess == 3) {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Continue", GUILayout.Height(30))) {
                SharedReference.page = 6;
            }
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
        }
        // If they have 'guessed' an incorrect number, display the wrong number text
        else if (savedGuess != 0) {
            wrongNumberRect = new Rect(Screen.width / 2 - position.width / 4, position.y * 1.25f, position.width / 2, position.height);
            GUI.Label(wrongNumberRect, "Wrong Number! Guess Again!", styleBoldCentered);
        }

        // If the editor madness window has called a reset, reset all necessary values
        if (SharedReference.reset) {
            savedGuess = 0;
            SharedReference.reset = false;
        }
    }
}
