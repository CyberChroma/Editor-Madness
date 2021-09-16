using UnityEngine;
using UnityEditor;

// Class for information to be shared between the editor window and the number guess property drawers
public static class SharedReference
{
    // The current 'level' we are on
    public static int page = 1;

    // A flag for the property drawer to reset it's information
    public static bool reset = false;
}

[ExecuteInEditMode]
public class EditorMadnessWindow : EditorWindow
{
    // Main
    // Reference to the long pieces of text to be read
    static ScriptableObjectAccess scriptableObjectAccess;
    // Reference to the parent gameobject of all the levels
    static Transform levelsParent;

    // Information on the different styles of text
    static GUIStyle styleBoldCentered;
    static GUIStyle styleCentered;


    // Level 1
    // Reference to the welcome text
    static Transform welcomeText;


    // Level 2
    // Default values of the math questions
    int mathNum1 = -1;
    int mathNum2 = -1;
    int mathNum3 = -1;
    
    // Reference to the parent of the checkmarks
    static Transform checkmarksParent;


    // Level 3
    // Default number of the slide
    int sliderNum = 0;
    // Reference to the cube that the slider controls
    static Transform sliderCube;


    // Level 4
    // The default values of the empty spots of the tic tac toe board
    string spot1 = "   ";
    string spot2 = "   ";
    string spot3 = "   ";

    // References to all 3 empty spots of the tic tac toe board
    XsAndOsSpotDecider spot1Parent;
    XsAndOsSpotDecider spot2Parent;
    XsAndOsSpotDecider spot3Parent;


    // Level 5
    // Reference to the number guess enum
    public NumberGuess numberGuess;


    // Level 6
    // Default values of all the toggles
    bool toggle1 = true;
    bool toggle2 = false;
    bool toggle3 = false;
    bool toggle4 = true;
    bool toggle5 = true;
    bool toggle6 = false;
    bool toggle7 = false;
    bool toggle8 = true;

    // Reference to the parent of the toggle lights
    static Transform toggleLightsParent;


    // Level 7
    // Default value of the copy text field
    string copiedText = "";

    // Reference to the completion image
    static Transform helloWorldParent;


    // Level 8
    // Default colours of all the buttons
    Color buttonColor1 = Color.red;
    Color buttonColor2 = Color.blue;
    Color buttonColor3 = Color.green;
    Color buttonColor4 = Color.blue;
    Color buttonColor5 = Color.red;
    Color buttonColor6 = Color.blue;
    Color buttonColor7 = Color.green;
    Color buttonColor8 = Color.red;
    Color buttonColor9 = Color.green;
    Color buttonColor10 = Color.blue;
    Color buttonColor11 = Color.green;
    Color buttonColor12 = Color.red;
    Color buttonColor13 = Color.red;
    Color buttonColor14 = Color.red;
    Color buttonColor15 = Color.green;
    Color buttonColor16 = Color.blue;
    Color buttonColor17 = Color.green;
    Color buttonColor18 = Color.red;
    Color buttonColor19 = Color.green;
    Color buttonColor20 = Color.blue;
    Color buttonColor21 = Color.red;
    Color buttonColor22 = Color.blue;
    Color buttonColor23 = Color.green;
    Color buttonColor24 = Color.blue;
    Color buttonColor25 = Color.red;

    // Reference to the parent of all the coloured cubes
    static Transform buttonColorCubesParent;


    // Level 9
    // Default transform values of the monkey
    float posX = 3;
    float posY = 4;
    float posZ = 2;
    float rotX = -20;
    float rotY = -15;
    float rotZ = 30;
    float scaX = -1.5f;
    float scaY = -3;
    float scaZ = -1;

    // References to the monkey to move and the outline of where it need to go
    static Transform movingMonkey;
    static Transform movingMonkeyOutline;


    // Level 10
    // Default values for the starting position of the maze cube
    float mazePlayerXPos = 2;
    float mazePlayerYPos = 2;

    // References to the player cube, goal cube, and parent of the maze walls
    static Transform player;
    static Transform goal;
    static Transform wallsParent;


    // Ending
    // Reference to the ending UI elements
    static Transform endingPanel;


    // Add menu to the Window menu
    [MenuItem("Custom Editor/Editor Madness")]
    static void Init()
    {
        // Open a new editor window
        EditorMadnessWindow window = (EditorMadnessWindow)GetWindow(typeof(EditorMadnessWindow));
        window.Show();
    }

    void OnEnable()
    {
        // For each level, find the references for all the necessary objects

        // Main
        scriptableObjectAccess = FindObjectOfType<ScriptableObjectAccess>();
        levelsParent = GameObject.Find("Levels").transform;

        // Level 1
        welcomeText = levelsParent.Find("Level 1").Find("Canvas").Find("Welcome Text Dropshadow");

        // Level 2
        checkmarksParent = levelsParent.Find("Level 2").Find("Checkmarks");

        // Level 3
        sliderCube = levelsParent.Find("Level 3").Find("Slider Cube");

        // Level 4
        spot1Parent = levelsParent.Find("Level 4").Find("Top Center").GetComponent<XsAndOsSpotDecider>();
        spot2Parent = levelsParent.Find("Level 4").Find("Bottom Left").GetComponent<XsAndOsSpotDecider>();
        spot3Parent = levelsParent.Find("Level 4").Find("Bottom Center").GetComponent<XsAndOsSpotDecider>();

        // Level 6
        toggleLightsParent = levelsParent.Find("Level 6").Find("Toggle Lights");

        // Level 7
        helloWorldParent = levelsParent.Find("Level 7").Find("Hello World Sprites");

        // Level 8
        buttonColorCubesParent = levelsParent.Find("Level 8").Find("Button Color Cubes");

        // Level 9
        movingMonkey = levelsParent.Find("Level 9").Find("Suzanne");
        movingMonkeyOutline = levelsParent.Find("Level 9").Find("Wireframe Suzanne");

        // Level 10
        player = levelsParent.Find("Level 10").Find("Player");
        goal = levelsParent.Find("Level 10").Find("Goal");
        wallsParent = levelsParent.Find("Level 10").Find("Walls");

        // Ending
        endingPanel = levelsParent.Find("Level 11").Find("Canvas").Find("Ending Panel");

        Reset();
    }

    void Update()
    {
        // If in level 1, spin the welcome text
        if (SharedReference.page == 1) {
            welcomeText.Rotate(new Vector3(0, 0, 20 * Time.deltaTime));
        // If in the ending, rotate the ending text back and forth
        } else if (SharedReference.page == 11) {
            endingPanel.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sin(Time.time*2) * 7));
        }
    }

    void OnGUI()
    {
        // Set up the level select UI and run its functionality
        LevelSelect();

        // Call the different levels depending on the page number
        switch (SharedReference.page) {
            case 1:
                // Set up the level 1 UI and run its functionality
                Level1();
                break;

            case 2:
                // Set up the level 2 UI and run its functionality
                Level2();
                break;

            case 3:
                // Set up the level 3 UI and run its functionality
                Level3();
                break;

            case 4:
                // Set up the level 4 UI and run its functionality
                Level4();
                break;

            case 5:
                // Set up the level 5 UI and run its functionality
                Level5();
                break;

            case 6:
                // Set up the level 6 UI and run its functionality
                Level6();
                break;

            case 7:
                // Set up the level 7 UI and run its functionality
                Level7();
                break;

            case 8:
                // Set up the level 8 UI and run its functionality
                Level8();
                break;

            case 9:
                // Set up the level 9 UI and run its functionality
                Level9();
                break;

            case 10:
                // Set up the level 10 UI and run its functionality
                Level10();
                break;

            case 11:
                // Set up the ending UI and run its functionality
                Ending();
                break;
        }
    }

    private void LevelSelect()
    {
        // Setting up the GUI styles
        styleBoldCentered = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
        styleCentered = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, wordWrap = true };

        // Drawing the title text
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("EDITOR MADNESS", styleBoldCentered);

        DrawLine(Color.grey);

        EditorGUILayout.BeginHorizontal();
        // Drawing a button that, if pressed, goes back to the previous level
        if (GUILayout.Button("Previous")) {
            SharedReference.page--;
            // Makes sure we can't go before level 1
            if (SharedReference.page < 1) {
                SharedReference.page = 1;
            }
        }

        // Draw the level number, or 'the end' if we are on the last level
        EditorGUIUtility.labelWidth = 0.1f;
        if (SharedReference.page == 11) {
            EditorGUILayout.LabelField("The End!", styleBoldCentered);
        }
        else {
            EditorGUILayout.LabelField("Level " + SharedReference.page, styleCentered);
        }
        EditorGUIUtility.labelWidth = 4;

        // Drawing a button that, if pressed, goes back to the next level
        if (GUILayout.Button("Next")) {
            // Makes sure we can't go after level 11
            SharedReference.page++;
            if (SharedReference.page > 11) {
                SharedReference.page = 11;
            }
        }
        EditorGUILayout.EndHorizontal();

        DrawLine(Color.grey);

        // Based on the page number, activate the corresponding level object, and deactivate the rest
        foreach (Transform levelObject in levelsParent) {
            if (("Level " + SharedReference.page) == levelObject.name) {
                levelObject.gameObject.SetActive(true);
            }
            else {
                levelObject.gameObject.SetActive(false);
            }
        }
    }

    private void Level1()
    {
        // Drawing the intro and level 1 text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.introText, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level1Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Drawing the continue button to go to the next level
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        if (GUILayout.Button("CAN YOU CLICK ME??", GUILayout.Height(50))) {
            SharedReference.page = 2;
        }
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
    }

    private void Level2()
    {
        // Draw the level explanation text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level2Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the math question, save the result
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("2 + 2 = ", styleCentered);
        mathNum1 = EditorGUILayout.IntField(mathNum1);
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the math question, save the result
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("7 - 10 = ", styleCentered);
        mathNum2 = EditorGUILayout.IntField(mathNum2);
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the math question, save the result
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("1920 * 3456 * 54321 * 0 = ", styleCentered);
        mathNum3 = EditorGUILayout.IntField(mathNum3);
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();

        // If the first math question was answered correctly, activate its corresponding checkmark
        if (mathNum1 == 4) {
            checkmarksParent.Find("1").gameObject.SetActive(true);
        }
        else {
            checkmarksParent.Find("1").gameObject.SetActive(false);
        }

        // If the second math question was answered correctly, activate its corresponding checkmark
        if (mathNum2 == -3) {
            checkmarksParent.Find("2").gameObject.SetActive(true);
        }
        else {
            checkmarksParent.Find("2").gameObject.SetActive(false);
        }

        // If the third math question was answered correctly, activate its corresponding checkmark
        if (mathNum3 == 0) {
            checkmarksParent.Find("3").gameObject.SetActive(true);
        }
        else {
            checkmarksParent.Find("3").gameObject.SetActive(false);
        }

        // If all the math questions were answered correctly,
        if (mathNum1 == 4 && mathNum2 == -3 && mathNum3 == 0) {
            // Drawing the continue button to go to the next level
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Continue", GUILayout.Height(30))) {
                SharedReference.page = 3;
            }
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
        }
    }

    private void Level3()
    {
        // Draw the level explanation text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level3Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the slider, save the result
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        sliderNum = Mathf.RoundToInt(EditorGUILayout.Slider(sliderNum, -8, 8));
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();

        // Move the cube based on the slider's position
        sliderCube.position = new Vector3(sliderNum, 0, 0);

        // If the slider has moved to the correct position,
        if (sliderNum == 5) {
            // Drawing the continue button to go to the next level
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Continue", GUILayout.Height(30))) {
                SharedReference.page = 4;
            }
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
        }
    }

    private void Level4()
    {
        // Draw the level explanation text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level4Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the first row of the tic tac toe board in the GUI
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        GUILayout.Button("O");
        EditorGUILayout.Space();
        // If the user click the top middle button,
        if (GUILayout.Button(spot1)) {
            // Set the button texts so only spot 1 is an X
            spot2 = "   ";
            spot3 = "   ";
            spot1 = "X";

            // Set the spot tag enum so spot 1 is an X
            spot1Parent.spotTag = XsAndOsSpotDecider.Tag.X;

            // Set the spot objects so spot 1 is an X
            spot1Parent.SetX();
        }
        EditorGUILayout.Space();
        GUILayout.Button("X");
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the second row of the tic tac toe board in the GUI
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        GUILayout.Button("O");
        EditorGUILayout.Space();
        GUILayout.Button("X");
        EditorGUILayout.Space();
        GUILayout.Button("O");
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the third row of the tic tac toe board in the GUI
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        // If the user click the bottom left button,
        if (GUILayout.Button(spot2)) {
            // Set the button texts so only spot 2 is an X
            spot1 = "   ";
            spot3 = "   ";
            spot2 = "X";

            // Set the spot tag enum so spot 2 is an X
            spot2Parent.spotTag = XsAndOsSpotDecider.Tag.X;

            // Set the spot objects so spot 2 is an X
            spot2Parent.SetX();
        }
        EditorGUILayout.Space();
        // If the user click the bottom middle button,
        if (GUILayout.Button(spot3)) {
            // Set the button texts so only spot 3 is an X
            spot1 = "   ";
            spot2 = "   ";
            spot3 = "X";

            // Set the spot tag enum so spot 3 is an X
            spot3Parent.spotTag = XsAndOsSpotDecider.Tag.X;

            // Set the spot objects so spot 3 is an X
            spot3Parent.SetX();
        }
        EditorGUILayout.Space();
        GUILayout.Button("X");
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();

        // If the spot text in any of the spots is blank, disable the x objects
        if (spot1 == "   ") {
            spot1Parent.spotTag = XsAndOsSpotDecider.Tag.Blank;
            spot1Parent.SetBlank();
        }
        if (spot2 == "   ") {
            spot2Parent.spotTag = XsAndOsSpotDecider.Tag.Blank;
            spot2Parent.SetBlank();
        }
        if (spot3 == "   ") {
            spot3Parent.spotTag = XsAndOsSpotDecider.Tag.Blank;
            spot3Parent.SetBlank();
        }

        if (spot2 == "X") {
            // Drawing the continue button to go to the next level
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Continue", GUILayout.Height(30))) {
                SharedReference.page = 5;
            }
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
        }
    }

    private void Level5()
    {
        // Draw the level explanation text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level5Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the number guess enum (the rest of the functionality is handled in number guess property drawer)
        EditorGUILayout.PropertyField(new SerializedObject(this).FindProperty("numberGuess"));
        // Continue button handled in number guess property drawer
    }

    private void Level6()
    {
        // Draw the level explanation text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level6Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Drawing all the toggles, and saving their values
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        toggle1 = EditorGUILayout.Toggle(toggle1);
        toggle2 = EditorGUILayout.Toggle(toggle2);
        toggle3 = EditorGUILayout.Toggle(toggle3);
        toggle4 = EditorGUILayout.Toggle(toggle4);
        toggle5 = EditorGUILayout.Toggle(toggle5);
        toggle6 = EditorGUILayout.Toggle(toggle6);
        toggle7 = EditorGUILayout.Toggle(toggle7);
        toggle8 = EditorGUILayout.Toggle(toggle8);
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        // For each toggle light, if the corresponding toggle box is checked, activate the light
        if (toggle1) {
            toggleLightsParent.Find("1").gameObject.SetActive(true);
        }
        else {
            toggleLightsParent.Find("1").gameObject.SetActive(false);
        }

        if (toggle2) {
            toggleLightsParent.Find("2").gameObject.SetActive(true);
        }
        else {
            toggleLightsParent.Find("2").gameObject.SetActive(false);
        }

        if (toggle3) {
            toggleLightsParent.Find("3").gameObject.SetActive(true);
        }
        else {
            toggleLightsParent.Find("3").gameObject.SetActive(false);
        }

        if (toggle4) {
            toggleLightsParent.Find("4").gameObject.SetActive(true);
        }
        else {
            toggleLightsParent.Find("4").gameObject.SetActive(false);
        }

        if (toggle5) {
            toggleLightsParent.Find("5").gameObject.SetActive(true);
        }
        else {
            toggleLightsParent.Find("5").gameObject.SetActive(false);
        }

        if (toggle6) {
            toggleLightsParent.Find("6").gameObject.SetActive(true);
        }
        else {
            toggleLightsParent.Find("6").gameObject.SetActive(false);
        }

        if (toggle7) {
            toggleLightsParent.Find("7").gameObject.SetActive(true);
        }
        else {
            toggleLightsParent.Find("7").gameObject.SetActive(false);
        }

        if (toggle8) {
            toggleLightsParent.Find("8").gameObject.SetActive(true);
        }
        else {
            toggleLightsParent.Find("8").gameObject.SetActive(false);
        }

        // If the right combination of toggles is set,
        if (!toggle1 && toggle2 && toggle3 && !toggle4 && toggle5 && !toggle6 && toggle7 && toggle8) {
            // Drawing the continue button to go to the next level
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Continue", GUILayout.Height(30))) {
                SharedReference.page = 7;
            }
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
        }
    }

    private void Level7()
    {
        // Draw the level explanation text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level7Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the copy text field, and save its value
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        copiedText = EditorGUILayout.TextField(copiedText);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();

        // If the text written matches the prompt
        if (copiedText.ToLower() == "hello world!") {
            // Activate the hello world image
            helloWorldParent.Find("Hello World").gameObject.SetActive(true);
            // Drawing the continue button to go to the next level
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Continue", GUILayout.Height(30))) {
                SharedReference.page = 8;
            }
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
        }
        else {
            // Deactivate the hello world image
            helloWorldParent.Find("Hello World").gameObject.SetActive(false);
        }
    }

    private void Level8()
    {
        // Draw the level explanation text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level8Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Variable for the size of the colour buttons
        int buttonSize = 50;

        // Draw each of the 25 buttons, and for each, when they are clicked, cycle through the colours "red, green, blue" by changing the button colour and the material colour of the corresponding cube
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        GUI.backgroundColor = buttonColor1;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("1").GetComponent<MeshRenderer>();
            if (buttonColor1 == Color.red) {
                buttonColor1 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor1 == Color.green) {
                buttonColor1 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor1 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor2;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("2").GetComponent<MeshRenderer>();
            if (buttonColor2 == Color.red) {
                buttonColor2 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor2 == Color.green) {
                buttonColor2 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor2 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor3;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("3").GetComponent<MeshRenderer>();
            if (buttonColor3 == Color.red) {
                buttonColor3 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor3 == Color.green) {
                buttonColor3 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor3 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor4;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("4").GetComponent<MeshRenderer>();
            if (buttonColor4 == Color.red) {
                buttonColor4 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor4 == Color.green) {
                buttonColor4 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor4 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor5;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("5").GetComponent<MeshRenderer>();
            if (buttonColor5 == Color.red) {
                buttonColor5 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor5 == Color.green) {
                buttonColor5 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor5 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        GUI.backgroundColor = buttonColor6;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("6").GetComponent<MeshRenderer>();
            if (buttonColor6 == Color.red) {
                buttonColor6 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor6 == Color.green) {
                buttonColor6 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor6 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor7;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("7").GetComponent<MeshRenderer>();
            if (buttonColor7 == Color.red) {
                buttonColor7 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor7 == Color.green) {
                buttonColor7 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor7 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor8;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("8").GetComponent<MeshRenderer>();
            if (buttonColor8 == Color.red) {
                buttonColor8 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor8 == Color.green) {
                buttonColor8 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor8 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor9;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("9").GetComponent<MeshRenderer>();
            if (buttonColor9 == Color.red) {
                buttonColor9 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor9 == Color.green) {
                buttonColor9 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor9 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor10;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("10").GetComponent<MeshRenderer>();
            if (buttonColor10 == Color.red) {
                buttonColor10 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor10 == Color.green) {
                buttonColor10 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor10 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        GUI.backgroundColor = buttonColor11;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("11").GetComponent<MeshRenderer>();
            if (buttonColor11 == Color.red) {
                buttonColor11 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor11 == Color.green) {
                buttonColor11 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor11 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor12;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("12").GetComponent<MeshRenderer>();
            if (buttonColor12 == Color.red) {
                buttonColor12 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor12 == Color.green) {
                buttonColor12 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor12 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor13;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("13").GetComponent<MeshRenderer>();
            if (buttonColor13 == Color.red) {
                buttonColor13 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor13 == Color.green) {
                buttonColor13 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor13 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor14;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("14").GetComponent<MeshRenderer>();
            if (buttonColor14 == Color.red) {
                buttonColor14 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor14 == Color.green) {
                buttonColor14 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor14 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor15;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("15").GetComponent<MeshRenderer>();
            if (buttonColor15 == Color.red) {
                buttonColor15 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor15 == Color.green) {
                buttonColor15 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor15 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        GUI.backgroundColor = buttonColor16;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("16").GetComponent<MeshRenderer>();
            if (buttonColor16 == Color.red) {
                buttonColor16 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor16 == Color.green) {
                buttonColor16 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor16 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor17;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("17").GetComponent<MeshRenderer>();

            if (buttonColor17 == Color.red) {
                buttonColor17 = Color.green;
            }
            else if (buttonColor17 == Color.green) {
                buttonColor17 = Color.blue;
            }
            else {
                buttonColor17 = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor18;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("18").GetComponent<MeshRenderer>();
            if (buttonColor18 == Color.red) {
                buttonColor18 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor18 == Color.green) {
                buttonColor18 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor18 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor19;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("19").GetComponent<MeshRenderer>();

            if (buttonColor19 == Color.red) {
                buttonColor19 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor19 == Color.green) {
                buttonColor19 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor19 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor20;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("20").GetComponent<MeshRenderer>();
            if (buttonColor20 == Color.red) {
                buttonColor20 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor20 == Color.green) {
                buttonColor20 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor20 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        GUI.backgroundColor = buttonColor21;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("21").GetComponent<MeshRenderer>();
            if (buttonColor21 == Color.red) {
                buttonColor21 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor21 == Color.green) {
                buttonColor21 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor21 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor22;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("22").GetComponent<MeshRenderer>();
            if (buttonColor22 == Color.red) {
                buttonColor22 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor22 == Color.green) {
                buttonColor22 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor22 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor23;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("23").GetComponent<MeshRenderer>();
            if (buttonColor23 == Color.red) {
                buttonColor23 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor23 == Color.green) {
                buttonColor23 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor23 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor24;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("24").GetComponent<MeshRenderer>();
            if (buttonColor24 == Color.red) {
                buttonColor24 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor24 == Color.green) {
                buttonColor24 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor24 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }

        GUI.backgroundColor = buttonColor25;
        if (GUILayout.Button("", GUILayout.Width(buttonSize), GUILayout.Height(buttonSize))) {
            MeshRenderer meshRenderer = buttonColorCubesParent.Find("25").GetComponent<MeshRenderer>();
            if (buttonColor25 == Color.red) {
                buttonColor25 = Color.green;
                meshRenderer.sharedMaterial.color = Color.green;
            }
            else if (buttonColor25 == Color.green) {
                buttonColor25 = Color.blue;
                meshRenderer.sharedMaterial.color = Color.blue;
            }
            else {
                buttonColor25 = Color.red;
                meshRenderer.sharedMaterial.color = Color.red;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        // If all the buttons are coloured green,
        GUI.backgroundColor = Color.white;
        if (buttonColor1 == Color.green && buttonColor2 == Color.green && buttonColor3 == Color.green && buttonColor4 == Color.green && buttonColor5 == Color.green && buttonColor6 == Color.green && buttonColor7 == Color.green && buttonColor8 == Color.green && buttonColor9 == Color.green && buttonColor10 == Color.green && buttonColor11 == Color.green && buttonColor12 == Color.green && buttonColor13 == Color.green && buttonColor14 == Color.green && buttonColor15 == Color.green && buttonColor16 == Color.green && buttonColor17 == Color.green && buttonColor18 == Color.green && buttonColor19 == Color.green && buttonColor20 == Color.green && buttonColor21 == Color.green && buttonColor22 == Color.green && buttonColor23 == Color.green && buttonColor24 == Color.green && buttonColor25 == Color.green) {
            // Drawing the continue button to go to the next level
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Continue", GUILayout.Height(30))) {
                SharedReference.page = 9;
            }
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
        }
    }

    private void Level9()
    {
        // Draw the level explanation text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level9Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Draw the position values, and save the results
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Position");
        EditorGUILayout.LabelField("X", GUILayout.Width(10));
        posX = EditorGUILayout.FloatField(posX);
        EditorGUILayout.LabelField("Y", GUILayout.Width(10));
        posY = EditorGUILayout.FloatField(posY);
        EditorGUILayout.LabelField("Z", GUILayout.Width(10));
        posZ = EditorGUILayout.FloatField(posZ);
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        // Draw the rotation values, and save the results
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Rotation");
        EditorGUILayout.LabelField("X", GUILayout.Width(10));
        rotX = EditorGUILayout.FloatField(rotX);
        EditorGUILayout.LabelField("Y", GUILayout.Width(10));
        rotY = EditorGUILayout.FloatField(rotY);
        EditorGUILayout.LabelField("Z", GUILayout.Width(10));
        rotZ = EditorGUILayout.FloatField(rotZ);
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        // Draw the rotation values, and save the results
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Scale");
        EditorGUILayout.LabelField("X", GUILayout.Width(10));
        scaX = EditorGUILayout.FloatField(scaX);
        EditorGUILayout.LabelField("Y", GUILayout.Width(10));
        scaY = EditorGUILayout.FloatField(scaY);
        EditorGUILayout.LabelField("Z", GUILayout.Width(10));
        scaZ = EditorGUILayout.FloatField(scaZ);
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        // Round the positon values to the nearest 0.5
        posX = Mathf.Round(posX * 2) / 2;
        posY = Mathf.Round(posY * 2) / 2;
        posZ = Mathf.Round(posZ * 2) / 2;

        // Round the rotation values to the nearest 10
        rotX = Mathf.Round(rotX / 10) * 10;
        rotY = Mathf.Round(rotY / 10) * 10;
        rotZ = Mathf.Round(rotZ / 10) * 10;

        // Round the positon values to the nearest 0.5
        scaX = Mathf.Round(scaX * 2) / 2;
        scaY = Mathf.Round(scaY * 2) / 2;
        scaZ = Mathf.Round(scaZ * 2) / 2;

        // Set the active gameobject in the inspector to be the outline monkey
        Selection.activeGameObject = movingMonkeyOutline.gameObject;

        // Set the monkey's transform values to match the recorded values
        movingMonkey.position = new Vector3(posX, posY, posZ);
        movingMonkey.rotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));
        movingMonkey.localScale = new Vector3(scaX, scaY, scaZ);

        // If the monkey's transform matches the outline's,
        if (movingMonkey.position == movingMonkeyOutline.position && movingMonkey.rotation == movingMonkeyOutline.rotation && movingMonkey.localScale == movingMonkeyOutline.localScale) {
            // Drawing the continue button to go to the next level
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Continue", GUILayout.Height(30))) {
                SharedReference.page = 10;
            }
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
        }
    }

    private void Level10()
    {
        // Draw the level explanation text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.level10Text, styleCentered);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // For the maze game, each wall is named based on its position in a grid-like manner, 1 1 being the top left, and 13 25 being the bottom right
        // Collision detection is done by, when the player goes to move, searching for a wall object matching the position it wants to move to
        // If a wall with that name exists, the player can't move. If no wall with that name exists, then no wall exists there, thus the player can move there
        // For example, if the player cube is at positon 5 3, and it wants to move down, that would put it in position 6 3,
        // Thus it would look for a wall object with the name "6 3". If it finds it, it doesn't move. If it doesn't find it, it moves

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        // Draw the up button, and if clicked, look above the player for wall, and set the player to move
        if (GUILayout.Button("Up", GUILayout.Width(50), GUILayout.Height(50))) {
            if (wallsParent.Find((mazePlayerYPos - 1).ToString() + " " + mazePlayerXPos) == null) {
                mazePlayerYPos--;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        // Draw the left button, and if clicked, look above the player for wall, and set the player to move
        if (GUILayout.Button("Left", GUILayout.Width(50), GUILayout.Height(50))) {
            if (wallsParent.Find(mazePlayerYPos + " " + (mazePlayerXPos - 1).ToString()) == null) {
                mazePlayerXPos--;
            }
        }
        EditorGUILayout.Space();

        // Draw the right button, and if clicked, look above the player for wall, and set the player to move
        if (GUILayout.Button("Right", GUILayout.Width(50), GUILayout.Height(50))) {
            if (wallsParent.Find(mazePlayerYPos + " " + (mazePlayerXPos + 1).ToString()) == null) {
                mazePlayerXPos++;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        // Draw the down button, and if clicked, look above the player for wall, and set the player to move
        if (GUILayout.Button("Down", GUILayout.Width(50), GUILayout.Height(50))) {
            if (wallsParent.Find((mazePlayerYPos + 1).ToString() + " " + mazePlayerXPos) == null) {
                mazePlayerYPos++;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        // Map the grid positons to the world transform positions, and move the player
        player.position = new Vector3(0.75f * mazePlayerXPos - 9.75f, 0, -(0.75f * mazePlayerYPos - 5.25f));

        // If the player has reached the goal
        if (player.position == goal.position) {
            // Drawing the continue button to go to the next level
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Continue", GUILayout.Height(30))) {
                SharedReference.page = 11;
            }
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();
        }
    }

    private void Ending()
    {
        // Draw the ending text
        EditorGUILayout.LabelField(scriptableObjectAccess.longTextStorage.endingText, styleCentered);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        // Drawing the reset button, and if clicked, restarts the game
        if (GUILayout.Button("RESET", GUILayout.Height(30))) {
            Reset();
        }
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
    }

    void DrawLine(Color color)
    {
        EditorGUILayout.Space();
        Rect lineRect = EditorGUILayout.BeginHorizontal();
        // Setting the colour of the line
        Handles.color = color;
        // Draws a thin horizontal line across the editor window
        Handles.DrawLine(new Vector2(lineRect.x - 15, lineRect.y), new Vector2(lineRect.width + 15, lineRect.y));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    void Reset()
    {
        // For all the level variables, sets them back to their default values

        // Main
        SharedReference.page = 1;

        // Level 1
        welcomeText.rotation = Quaternion.identity;

        // Level 2
        mathNum1 = -1;
        mathNum2 = -1;
        mathNum3 = -1;

        // Level 3
        sliderNum = 0;

        // Level 4
        spot1 = "   ";
        spot2 = "   ";
        spot3 = "   ";

        // Level 5
        SharedReference.reset = true;

        // Level 6
        toggle1 = true;
        toggle2 = false;
        toggle3 = false;
        toggle4 = true;
        toggle5 = true;
        toggle6 = false;
        toggle7 = false;
        toggle8 = true;

        // Level 7
        copiedText = "";

        // Level 8
        buttonColor1 = Color.red;
        buttonColor2 = Color.blue;
        buttonColor3 = Color.green;
        buttonColor4 = Color.blue;
        buttonColor5 = Color.red;
        buttonColor6 = Color.blue;
        buttonColor7 = Color.green;
        buttonColor8 = Color.red;
        buttonColor9 = Color.green;
        buttonColor10 = Color.blue;
        buttonColor11 = Color.green;
        buttonColor12 = Color.red;
        buttonColor13 = Color.red;
        buttonColor14 = Color.red;
        buttonColor15 = Color.green;
        buttonColor16 = Color.blue;
        buttonColor17 = Color.green;
        buttonColor18 = Color.red;
        buttonColor19 = Color.green;
        buttonColor20 = Color.blue;
        buttonColor21 = Color.red;
        buttonColor22 = Color.blue;
        buttonColor23 = Color.green;
        buttonColor24 = Color.blue;
        buttonColor25 = Color.red;

        buttonColorCubesParent = GameObject.Find("Levels").transform.Find("Level 8").Find("Button Color Cubes").transform;
        buttonColorCubesParent.Find("1").GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        buttonColorCubesParent.Find("2").GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
        buttonColorCubesParent.Find("3").GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        buttonColorCubesParent.Find("4").GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
        buttonColorCubesParent.Find("5").GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        buttonColorCubesParent.Find("6").GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
        buttonColorCubesParent.Find("7").GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        buttonColorCubesParent.Find("8").GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        buttonColorCubesParent.Find("9").GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        buttonColorCubesParent.Find("10").GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
        buttonColorCubesParent.Find("11").GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        buttonColorCubesParent.Find("12").GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        buttonColorCubesParent.Find("13").GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        buttonColorCubesParent.Find("14").GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        buttonColorCubesParent.Find("15").GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        buttonColorCubesParent.Find("16").GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
        buttonColorCubesParent.Find("17").GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        buttonColorCubesParent.Find("18").GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        buttonColorCubesParent.Find("19").GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        buttonColorCubesParent.Find("20").GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
        buttonColorCubesParent.Find("21").GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        buttonColorCubesParent.Find("22").GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
        buttonColorCubesParent.Find("23").GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        buttonColorCubesParent.Find("24").GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
        buttonColorCubesParent.Find("25").GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;

        // Level 9
        posX = 3;
        posY = 4;
        posZ = 2;
        rotX = -20;
        rotY = -15;
        rotZ = 30;
        scaX = -1.5f;
        scaY = -3;
        scaZ = -1;

        // Level 10
        mazePlayerXPos = 2;
        mazePlayerYPos = 2;

        // Ending
        welcomeText.rotation = Quaternion.identity;
    }
}