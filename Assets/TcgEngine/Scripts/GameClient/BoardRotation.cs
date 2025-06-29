using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using TcgEngine.Client;
using UnityEngine;

public class BoardRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform CameraParent;
    bool rotateConfirmed = false;
    // Update is called once per frame
    void Update()
    {
        if (rotateConfirmed == true)
            return;
        //해야할것-나중에 게임 시작하면 설정되도록 변경하기
        if (!GameClient.Get().IsReady())
            return;
        Game data = GameClient.Get().GetGameData();
        Player player = GameClient.Get().GetPlayer();
        if (player.player_id == 0)
        {
            rotateConfirmed = true;
        }
        else if (player.player_id == 1)
        {
            //CameraParent.eulerAngles = new Vector3(0f, 180f, 0f);
            CameraParent.localRotation = Quaternion.Euler(0, 180, 0);
            //CameraParent.position = new Vector3(0, -2f, 0f);
            rotateConfirmed = true;
        }
        return;
        
    }
}
