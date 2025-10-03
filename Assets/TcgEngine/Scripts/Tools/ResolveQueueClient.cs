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
        public virtual void Update(float delta)
        {
            if (resolve_delay > 0f)
            {
                resolve_delay -= delta;
                if (resolve_delay <= 0f)
                    ResolveAll();
            }
        }

        public virtual void AddCallback(SerializedData sdata, Action<SerializedData> callback)
        {
            if (sdata != null)
            {
                MscRefreshGameElement elem = new MscRefreshGameElement();
                common_elem_pool.Create(elem);
                elem.sdata = sdata;
                elem.callback = callback;
                common_queue.Enqueue(elem);
            }
        }

        public virtual void Resolve()
        {
            if (common_queue.Count > 0)
            {
                MscRefreshGameElement elem = common_queue.Dequeue();

                if (elem is MscRefreshGameElement)
                {
                    common_elem_pool.Dispose(elem);
                    elem.callback?.Invoke(elem.sdata);
                }
            }
        }
        public virtual void ResolveAll(float delay)
        {
            SetDelay(delay);
            ResolveAll();  //Resolve now if no delay
        }

        public virtual void ResolveAll()
        {
            if (is_resolving)
                return;
            is_resolving = true;
            while (CanResolve())
            {
                Resolve();
            }
            is_resolving = false;
        }
        public virtual bool IsResolving()
        {
            return is_resolving || resolve_delay > 0f;
        }
        public virtual void SetDelay(float delay)
        {
            if (!skip_delay)
            {
                resolve_delay = Mathf.Max(resolve_delay, delay);
            }
        }

        public virtual bool CanResolve()
        {
            if (resolve_delay > 0f)
                return false;   //Is waiting delay
            /*
            if (client.GetGameData().state == GameState.GameEnded)
                return false; //Cant execute anymore when game is ended
            if (client.GetGameData().selector != SelectorType.None)
                return false; //Waiting for player input, in the middle of resolve loop
            */
            return common_queue.Count > 0;
        }

        public virtual void Clear()
        {
            common_elem_pool.DisposeAll();
            common_queue.Clear();
        }

        public Queue<MscRefreshGameElement> GetCommonQueue()
        {
            return common_queue;
        }

        public class MscRefreshGameElement
        {
            public SerializedData sdata;
            public Action<SerializedData> callback;
        }
    }
}
