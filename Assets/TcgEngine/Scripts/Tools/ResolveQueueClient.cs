using System;
using System.Collections;
using System.Collections.Generic;
using TcgEngine.Client;
using UnityEngine;
using UnityEngine.Events;


namespace TcgEngine
{
    public class ResolveQueueClient
    {
        private Pool<MscRefreshGameElement> common_elem_pool = new Pool<MscRefreshGameElement>();
        private Queue<MscRefreshGameElement> common_queue = new Queue<MscRefreshGameElement>();

        public GameClient client;
        public Game current_game_data;
        private bool is_resolving = false;
        private float resolve_delay = 0f;
        private bool skip_delay = false;

        public ResolveQueueClient(Game data)
        {
            current_game_data = data;
        }

        public virtual void Resolve()
        {
            if (common_queue.Count > 0)
            {
                MscRefreshGameElement elem = common_queue.Dequeue();
                if (elem.action_name == GameAction.CardMoved)
                {
                    MscMsgPlayCard msg = elem.sdata.Get<MscMsgPlayCard>();

                    current_game_data = msg.game_data;

                    Card card = client.GetGameData().GetCard(msg.card_uid);
                    client.onCardMoved?.Invoke(card, msg.slot);
                }
            }
        }



        public class MscRefreshGameElement
        {
            //public Game game_data;
            public ushort action_name;
            public SerializedData sdata;
        }
        public virtual void AddCallback(ushort action_name, SerializedData sdata)
        {
            if (sdata != null)
            {
                MscRefreshGameElement elem = new MscRefreshGameElement();
                common_elem_pool.Create(elem);
                elem.action_name = action_name;
                elem.sdata = sdata;
                common_queue.Enqueue(elem);
            }
        }
    }
}
