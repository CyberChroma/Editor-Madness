using UnityEngine;

[CreateAssetMenu(fileName = "Long Text Storage")]
public class LongTextStorage : ScriptableObject
{
    // Defining several strings used to organize all of the long pieces of text used by the editor window in one place
    [TextArea(3, 10)]
    public string introText = "";

    [TextArea(3, 10)]
    public string level1Text = "";

    [TextArea(3, 10)]
    public string level2Text = "";

    [TextArea(3, 10)]
    public string level3Text = "";

    [TextArea(3, 10)]
    public string level4Text = "";

    [TextArea(3, 10)]
    public string level5Text = "";

    [TextArea(3, 10)]
    public string level6Text = "";

    [TextArea(3, 10)]
    public string level7Text = "";

    [TextArea(3, 10)]
    public string level8Text = "";

    [TextArea(3, 10)]
    public string level9Text = "";

    [TextArea(3, 10)]
    public string level10Text = "";

    [TextArea(3, 10)]
    public string endingText = "";
}
