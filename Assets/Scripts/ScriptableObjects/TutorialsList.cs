using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TutorialsList", menuName = "Game/TutorialsList")]
public class TutorialsList : ScriptableObject {

    public List<Tutorial> Tutorials; 
}
