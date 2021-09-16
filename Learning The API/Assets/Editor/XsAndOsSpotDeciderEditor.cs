using UnityEditor;

[CustomEditor(typeof(XsAndOsSpotDecider))]
public class XsAndOsSpotDeciderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Drawing the normal GUI elements for this script
        base.OnInspectorGUI();

        // Getting the script that this is a custom editor for
        XsAndOsSpotDecider xsAndOsSpotDecider = (XsAndOsSpotDecider)target;

        // Depending on the value of the spot tag enum, update the active object in this spot
        switch (xsAndOsSpotDecider.spotTag) {
            case XsAndOsSpotDecider.Tag.Blank:
                xsAndOsSpotDecider.SetBlank();
                break;
            case XsAndOsSpotDecider.Tag.X:
                xsAndOsSpotDecider.SetX();
                break;
            case XsAndOsSpotDecider.Tag.O:
                xsAndOsSpotDecider.SetO();
                break;
        }
    }
}
