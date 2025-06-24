using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using TcgEngine.Client;
using UnityEngine;

public class BoardRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform boardmap;

    // Update is called once per frame
    void Update()
    {
        //해야할것-나중에 게임 시작하면 설정되도록 변경하기
        if (!GameClient.Get().IsReady())
            return;
        Game data = GameClient.Get().GetGameData();
        Player player = GameClient.Get().GetPlayer();
        if (player.player_id == 0)
        {

        }
        else if (player.player_id == 1)
        {
            boardmap.localRotation = Quaternion.Euler(0f, 0f, 180f);
            //boardmap.localPosition = new Vector3(0, -2f, 0f);
        }
        return;
    }
}
