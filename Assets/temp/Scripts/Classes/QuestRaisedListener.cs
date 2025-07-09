

using UnityEngine;

public class QuestRaisedListener : MonoBehaviour {

    [SerializeField] Component questNotification; 
    //
    public void Start(){

    }

    public void ShowQuestRaised(){
        Debug.Log("quest should be shown");
        questNotification.gameObject.SetActive(true);
    }

}