using UnityEngine;

public class XsAndOsSpotDecider : MonoBehaviour
{
    // An enum used to represent what 'type' this spot in the tic tac toe board should be
    public enum Tag
    {
        Blank,
        X,
        O
    }

    public Tag spotTag;

    public void SetBlank ()
    {
        // Deactivate both the x and o
        transform.Find("X").gameObject.SetActive(false);
        transform.Find("O").gameObject.SetActive(false);
    }

    public void SetX ()
    {
        // Activate the the x and deactivate the o
        transform.Find("X").gameObject.SetActive(true);
        transform.Find("O").gameObject.SetActive(false);
    }

    public void SetO ()
    {
        // Activate the the o and deactivate the x
        transform.Find("X").gameObject.SetActive(false);
        transform.Find("O").gameObject.SetActive(true);
    }
}
