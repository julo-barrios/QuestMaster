using UnityEngine;

public class QuestNotificationClickedListener : MonoBehaviour
{
    
    void OnMouseDown()
    {
        Debug.Log("notification clicked");
        this.gameObject.SetActive(false);
    }
}
