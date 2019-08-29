
using UnityEngine;

public class LevelUpdate : MonoBehaviour
{

   public int LevelCompletionScore;

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        Player.Instance.ColorCube = LevelCompletionScore;
        Debug.Log(Player.Instance.ColorCube + " Available Cubes");
    }
}
