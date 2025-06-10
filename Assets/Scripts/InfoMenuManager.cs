using TMPro;
using UnityEngine;

public class InfoMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TextMeshProUGUI counterText;

    private string text1, text2, text3, text4, text5;
    private string[] texts = new string[5];
    private uint iterator = 0;

    private void Awake()
    {
        text1 = "This Prototype will let you test a Combat System.\n\n" +
            "You are the entity that spawns at the left, and the enemy is the one that spawns in the right.\n\n" +
            "You can reset your position and the enemie's by pressing the R key (keyboard) or the Select button (controller).\n\n" +
            "After playing this Prototype, please respond the provided feedback form so I can fix any issue.";
       
        text2 = "There exist 2 different resources in the game: Potential and Charge.\n\n" +
            "As the Player you will use the first, and the Enemy will use the second.";
        
        text3 = "You have 2 types of attacks: Light and Heavy, each of them bound to a different button" +
            "(Head to the controls tab to see the controls).\n\n" +
            "Light Attacks give Potential and deal a low amount of Charge, and Heavy Attacks consume it and deal a higher amount of Charge." +
            "Heavy Attacks can also be hold to deal more Charge damage.\n\n" +
            "You can also attack on the air.";
        
        text4 = "You will see the enemy attacks in a slow interval.\n\n" +
            "You can press the Parry button to stagger the enemy and deal Charge damage to them";
        
        text5 = "When you accumulate enough Charge on the enemy, they are stunned.\n\n" +
            "While stunned, you can CC the enemy with Heavy Attacks, letting you Knock them up for an air combo.\n\n" +
            "Some attacks also vaccum enemies when stunned, letting you control where they go.";

        texts[0] = text1;
        texts[1] = text2;
        texts[2] = text3;
        texts[3] = text4;
        texts[4] = text5;
    }

    private void Update()
    {
        textMesh.text = texts[iterator];
        counterText.text = (iterator + 1) + "/5";
    }

    public void NextText()
    {
        if (iterator == texts.Length - 1)
        {
            iterator = 0;
        }
        else iterator++;
    }

    public void PreviousText()
    {
        if (iterator == 0)
        {
            iterator = 4;
            return;
        }
        else iterator--;
    }
}
