using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TcgEngine
{
    /// <summary>
    /// Resolve abilties and actions one by one, with an optional delay in between each
    /// </summary>

    public class ResolveQueue 
    {
        private Pool<CommonQueueElement> common_elem_pool = new Pool<CommonQueueElement>();
        private Pool<AbilityQueueElement> ability_elem_pool = new Pool<AbilityQueueElement>();
        private Pool<SecretQueueElement> secret_elem_pool = new Pool<SecretQueueElement>();
        private Pool<AttackQueueElement> attack_elem_pool = new Pool<AttackQueueElement>();
        private Pool<CallbackQueueElement> callback_elem_pool = new Pool<CallbackQueueElement>();
        
        private Queue<CommonQueueElement> common_queue = new Queue<CommonQueueElement>();
        private Queue<AbilityQueueElement> ability_queue = new Queue<AbilityQueueElement>();
        private Queue<SecretQueueElement> secret_queue = new Queue<SecretQueueElement>();
        private Queue<AttackQueueElement> attack_queue = new Queue<AttackQueueElement>();
        private Queue<CallbackQueueElement> callback_queue = new Queue<CallbackQueueElement>();

        //private Queue<int> animation_queue = new Queue<int>(); //추가- 임시 애니메이션 큐

        private Game game_data;
        private bool is_resolving = false;
        private float resolve_delay = 0f;
        private bool skip_delay = false;

        public ResolveQueue(Game data, bool skip)
        {
            game_data = data;
            skip_delay = skip;
        }

        public void SetData(Game data)
        {
            game_data = data;
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

        public virtual void AddAbility(AbilityData ability, Card caster, Card triggerer, Action<AbilityData, Card, Card> callback)
        {
            if (ability != null && caster != null)
            {
                //AbilityQueueElement elem = ability_elem_pool.Create();
                AbilityQueueElement elem = new AbilityQueueElement();
                common_elem_pool.Create(elem);
                elem.caster = caster;
                elem.triggerer = triggerer;
                elem.ability = ability;
                elem.callback = callback;
                //ability_queue.Enqueue(elem);
                common_queue.Enqueue(elem);
            }
        }

        public virtual void AddAttack(Card attacker, Card target, Action<Card, Card, bool> callback, bool skip_cost = false)
        {
            if (attacker != null && target != null)
            {
                //AttackQueueElement elem = attack_elem_pool.Create();
                AttackQueueElement elem = new AttackQueueElement();
                common_elem_pool.Create(elem);
                elem.attacker = attacker;
                elem.target = target;
                elem.ptarget = null;
                elem.skip_cost = skip_cost;
                elem.callback = callback;
                //attack_queue.Enqueue(elem);
                common_queue.Enqueue(elem);
            }
        }
        public virtual void AddMove(Card mover, Slot slot, Action<Card, Slot, bool> callback, bool skip_cost = false)
        {
            if(mover != null && slot != null)
            {
                MoveQueueElement elem = new MoveQueueElement();
                common_elem_pool.Create(elem);
                elem.mover = mover;
                elem.slot = slot;
                elem.skip_cost= skip_cost;
                elem.callback = callback;
                common_queue.Enqueue(elem);
            }
        }
        public virtual void AddAttack(Card attacker, Player target, Action<Card, Player, bool> callback, bool skip_cost = false)
        {
            if (attacker != null && target != null)
            {
                //AttackQueueElement elem = attack_elem_pool.Create();
                AttackQueueElement elem = new AttackQueueElement();
                common_elem_pool.Create(elem);
                elem.attacker = attacker;
                elem.target = null;
                elem.ptarget = target;
                elem.skip_cost = skip_cost;
                elem.pcallback = callback;
                //attack_queue.Enqueue(elem);
                common_queue.Enqueue(elem);
            }
        }

        public virtual void AddSecret(AbilityTrigger secret_trigger, Card secret, Card trigger, Action<AbilityTrigger, Card, Card> callback)
        {
            if (secret != null && trigger != null)
            {
                //SecretQueueElement elem = secret_elem_pool.Create();
                SecretQueueElement elem = new SecretQueueElement();
                common_elem_pool.Create(elem);
                elem.secret_trigger = secret_trigger;
                elem.secret = secret;
                elem.triggerer = trigger;
                elem.callback = callback;
                //secret_queue.Enqueue(elem);
                common_queue.Enqueue(elem);
            }
        }

        public virtual void AddCallback(Action callback)
        {
            if (callback != null)
            {
                //CallbackQueueElement elem = callback_elem_pool.Create();
                CallbackQueueElement elem = new CallbackQueueElement();
                common_elem_pool.Create(elem);
                elem.callback = callback;
                //callback_queue.Enqueue(elem);
                common_queue.Enqueue(elem);
            }
        }

        public virtual void Resolve()
        {
            if(common_queue.Count > 0)
            {
                CommonQueueElement elem = common_queue.Dequeue();
                if(elem is AbilityQueueElement elem1)
                {
                    //Resolve Ability
                    common_elem_pool.Dispose(elem1);
                    elem1.callback?.Invoke(elem1.ability, elem1.caster, elem1.triggerer);
                    //Debug.Log("movoo1");
                }
                else if(elem is SecretQueueElement elem2)
                {
                    common_elem_pool.Dispose(elem2);
                    elem2.callback?.Invoke(elem2.secret_trigger, elem2.secret, elem2.triggerer);
                    //Debug.Log("movoo2");
                }
                else if(elem is AttackQueueElement elem3)
                {
                    //Resolve Attack
                    common_elem_pool.Dispose(elem3);
                    if (elem3.ptarget != null)
                        elem3.pcallback?.Invoke(elem3.attacker, elem3.ptarget, elem3.skip_cost);
                    else
                        elem3.callback?.Invoke(elem3.attacker, elem3.target, elem3.skip_cost);
                    //Debug.Log("movoo3");
                }
                else if(elem is MoveQueueElement elem4)
                {
                    common_elem_pool.Dispose(elem4);
                    elem4.callback?.Invoke(elem4.mover, elem4.slot, elem4.skip_cost);
                    //Debug.Log("movoo4");
                }
                else if(elem is CallbackQueueElement elem5)
                {
                    common_elem_pool.Dispose(elem5);
                    elem5.callback.Invoke();
                    //Debug.Log("movoo5");
                }
                else
                {
                    Debug.Log("ddd");
                }
            }
            /*
            if (ability_queue.Count > 0)
            {
                //Resolve Ability
                AbilityQueueElement elem = ability_queue.Dequeue();
                ability_elem_pool.Dispose(elem);
                elem.callback?.Invoke(elem.ability, elem.caster, elem.triggerer);
                //Debug.Log("movoo1");
            }
            else if (secret_queue.Count > 0)
            {
                //Resolve Secret
                SecretQueueElement elem = secret_queue.Dequeue();
                secret_elem_pool.Dispose(elem);
                elem.callback?.Invoke(elem.secret_trigger, elem.secret, elem.triggerer);
                //Debug.Log("movoo2");
            }
            else if (attack_queue.Count > 0)
            {
                //Resolve Attack
                AttackQueueElement elem = attack_queue.Dequeue();
                attack_elem_pool.Dispose(elem);
                if (elem.ptarget != null)
                    elem.pcallback?.Invoke(elem.attacker, elem.ptarget, elem.skip_cost);
                else
                    elem.callback?.Invoke(elem.attacker, elem.target, elem.skip_cost);
                //Debug.Log("movoo3");
            }
            else if (callback_queue.Count > 0)
            {
                CallbackQueueElement elem = callback_queue.Dequeue();
                callback_elem_pool.Dispose(elem);
                elem.callback.Invoke();
                //Debug.Log("movoo4");
            }
            */
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
            if (game_data.state == GameState.GameEnded)
                return false; //Cant execute anymore when game is ended
            if (game_data.selector != SelectorType.None)
                return false; //Waiting for player input, in the middle of resolve loop
            return common_queue.Count > 0 || attack_queue.Count > 0 || ability_queue.Count > 0 || secret_queue.Count > 0 || callback_queue.Count > 0;
        }

        public virtual bool IsResolving()
        {
            return is_resolving || resolve_delay > 0f;
        }

        public virtual void Clear()
        {
            //실제로는 common만 작동
            common_elem_pool.DisposeAll();

            attack_elem_pool.DisposeAll();
            ability_elem_pool.DisposeAll();
            secret_elem_pool.DisposeAll();
            callback_elem_pool.DisposeAll();

            common_queue.Clear();

            attack_queue.Clear();
            ability_queue.Clear();
            secret_queue.Clear();
            callback_queue.Clear();
        }

        public Queue<AttackQueueElement> GetAttackQueue()
        {
            return attack_queue;
        }
        public Queue<AbilityQueueElement> GetAbilityQueue()
        {
            return ability_queue;
        }

        public Queue<SecretQueueElement> GetSecretQueue()
        {
            return secret_queue;
        }

        public Queue<CallbackQueueElement> GetCallbackQueue()
        {
            return callback_queue;
        }
        public Queue<CommonQueueElement> GetCommonQueue()
        {
            return common_queue;
        }
    }

    public class CommonQueueElement
    {
    }
    public class AbilityQueueElement : CommonQueueElement
    {
        public AbilityData ability;
        public Card caster;
        public Card triggerer;
        public Action<AbilityData, Card, Card> callback;
    }

    public class AttackQueueElement : CommonQueueElement
    {
        public Card attacker;
        public Card target;
        public Player ptarget;
        public bool skip_cost;
        public Action<Card, Card, bool> callback;
        public Action<Card, Player, bool> pcallback;
    }

    public class MoveQueueElement : CommonQueueElement
    {
        public Card mover;
        public Slot slot;
        public bool skip_cost;
        public Action<Card, Slot, bool> callback;
    }

    public class SecretQueueElement : CommonQueueElement
    {
        public AbilityTrigger secret_trigger;
        public Card secret;
        public Card triggerer;
        public Action<AbilityTrigger, Card, Card> callback;
    }

    public class CallbackQueueElement : CommonQueueElement
    {
        public Action callback;
    }



    
}
