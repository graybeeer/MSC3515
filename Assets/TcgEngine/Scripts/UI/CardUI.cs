using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TcgEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TcgEngine.Client;
//using static UnityEditor.Experimental.GraphView.Port;

namespace TcgEngine.UI
{
    /// <summary>
    /// Scripts to display all stats of a cardData, 
    /// is used by other script that display cards like BoardCard, and HandCard, CollectionCard..
    /// </summary>

    public class CardUI : MonoBehaviour, IPointerClickHandler
    {
        public Image card_image;
        public Image frame_image;
        public Image team_icon;
        public Image rarity_icon;
        public Image attack_icon;
        public Image hp_icon;
        public Image cost_icon;
        public Text attack;
        public Text hp;
        public Text cost;
        public Image[] card_arrow_icon = new Image[9];
        //public SpriteRenderer[] card_arrow = new SpriteRenderer[9];
        public Sprite[] full_card_arrow_icon = new Sprite[9];
        public Sprite[] empty_card_arrow_icon = new Sprite[9];

        public Text card_title;
        public Text card_text;

        public TraitUI[] stats;

        public UnityAction<CardUI> onClick;
        public UnityAction<CardUI> onClickRight;

        private CardData card;
        private VariantData variant;

        void Awake()
        {

        }

        public void SetCard(Card card)
        {
            if (card == null)
                return;

            SetCard(card.CardData, card.VariantData);

            if (cost != null)
                cost.text = card.GetMana().ToString();
            if (cost != null && card.CardData.IsDynamicManaCost())
                cost.text = "X";
            if (attack != null)
                attack.text = card.GetAttack().ToString();
            if (hp != null)
                hp.text = card.GetHP().ToString();
            //내가 추가한 이동방향 ui
            if (card_arrow_icon != null)
            {
                bool[] temp_check_curse = EffectCurseHaste.CheckCursed(card);
                bool[] temp_check_haste = EffectCurseHaste.CheckHasted(card);
                for (int i = 0; i < card_arrow_icon.Length; i++)
                {
                    //약화된 이동마커는 시각적으로 표시
                    if (!temp_check_curse[i])
                        card_arrow_icon[i].sprite = full_card_arrow_icon[i];
                    else card_arrow_icon[i].sprite = empty_card_arrow_icon[i];

                    //강화된 이동마커도 시각적으로 색깔 다르게 해서 표시
                    if (card.card_arrow[i])
                        card_arrow_icon[i].color = new Color(1f, 1f, 1f, 1f);
                    else if (temp_check_haste[i])
                        card_arrow_icon[i].color = new Color(0.7f, 1f, 0.7f, 1f);
                    else
                        card_arrow_icon[i].color = new Color(1f, 1f, 1f, 0f);

                    //아티팩트 이동마커는 다르다는걸 표시
                    if (card.CardData.IsArtifact())
                        card_arrow_icon[i].color = new Color(0.3f, 0.3f, 1f, card_arrow_icon[i].color.a);
                }
            }
            foreach (TraitUI stat in stats)
                stat.SetCard(card);
        }

        public void SetCard(CardData cardData, VariantData variant)
        {
            if (cardData == null)
                return;

            this.card = cardData;
            this.variant = variant;

            if(card_image != null)
                card_image.sprite = cardData.GetFullArt(variant);
            if (frame_image != null)
                frame_image.sprite = variant.frame;
            if (card_title != null)
                card_title.text = cardData.GetTitle().ToUpper();
            if (card_text != null)
                card_text.text = cardData.GetText();

            if (attack_icon != null)
                attack_icon.enabled = false;
            if (attack != null)
                attack.enabled = false;
            
            if (hp_icon != null)
                hp_icon.enabled = cardData.IsArtifact() || cardData.IsEquipment();
            if (hp != null)
                hp.enabled = cardData.IsArtifact() || cardData.IsEquipment();
            if (cost_icon != null)
                cost_icon.enabled = !cardData.IsHero();
            if (cost != null)
                cost.enabled = !cardData.IsHero();

            if (cost != null)
                cost.text = cardData.mana.ToString();
            if (cost != null && cardData.IsDynamicManaCost())
                cost.text = "X";
            if (attack != null)
                attack.text = cardData.attack.ToString();
            if (hp != null)
                hp.text = cardData.hp.ToString();
            //내가 추가한 이동방향 ui

            if (card_arrow_icon != null)
            {
                foreach (Image arrow in card_arrow_icon)
                {
                    arrow.enabled = cardData.IsCanBeBoardCard();
                }
            }
            if (card_arrow_icon != null)
            {
                for (int i = 0; i < card_arrow_icon.Length; i++)
                {
                    if (cardData.card_arrow[i])
                        card_arrow_icon[i].color = new Color(1f, 1f, 1f, 1f);
                    else
                        card_arrow_icon[i].color = new Color(1f, 1f, 1f, 0f);

                    //아티팩트 이동마커는 다르다는걸 표시
                    if (cardData.IsArtifact())
                        card_arrow_icon[i].color = new Color(1f, 0.5f, 1f, card_arrow_icon[i].color.a);
                }
            }
            if (team_icon != null)
            {
                team_icon.sprite = cardData.team.icon;
                team_icon.enabled = team_icon.sprite != null;
            }

            if (rarity_icon != null)
            {
                rarity_icon.sprite = cardData.rarity.icon;
                rarity_icon.enabled = rarity_icon.sprite != null && !cardData.IsHero();
            }

            foreach (TraitUI stat in stats)
                stat.SetCard(cardData);

            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }

        public void SetHP(int hp_value)
        {
            if (hp != null)
                hp.text = hp_value.ToString();
        }

        public void SetMaterial(Material mat)
        {
            if (card_image != null)
                card_image.material = mat;
            if (frame_image != null)
                frame_image.material = mat;
            if (team_icon != null)
                team_icon.material = mat;
            if (rarity_icon != null)
                rarity_icon.material = mat;
            if (attack_icon != null)
                attack_icon.material = mat;
            if (hp_icon != null)
                hp_icon.material = mat;
            if (cost_icon != null)
                cost_icon.material = mat;
        }

        public void SetOpacity(float opacity)
        {
            if (card_image != null)
                card_image.color = new Color(card_image.color.r, card_image.color.g, card_image.color.b, opacity);
            if (frame_image != null)
                frame_image.color = new Color(frame_image.color.r, frame_image.color.g, frame_image.color.b, opacity);
            if (team_icon != null)
                team_icon.color = new Color(team_icon.color.r, team_icon.color.g, team_icon.color.b, opacity);
            if (rarity_icon != null)
                rarity_icon.color = new Color(rarity_icon.color.r, rarity_icon.color.g, rarity_icon.color.b, opacity);
            if (attack_icon != null)
                attack_icon.color = new Color(attack_icon.color.r, attack_icon.color.g, attack_icon.color.b, opacity);
            if (hp_icon != null)
                hp_icon.color = new Color(hp_icon.color.r, hp_icon.color.g, hp_icon.color.b, opacity);
            if (cost_icon != null)
                cost_icon.color = new Color(cost_icon.color.r, cost_icon.color.g, cost_icon.color.b, opacity);
            if (attack != null)
                attack.color = new Color(attack.color.r, attack.color.g, attack.color.b, opacity);
            if (hp != null)
                hp.color = new Color(hp.color.r, hp.color.g, hp.color.b, opacity);
            if (cost != null)
                cost.color = new Color(cost.color.r, cost.color.g, cost.color.b, opacity);
            if (card_title != null)
                card_title.color = new Color(card_title.color.r, card_title.color.g, card_title.color.b, opacity);
            if (card_text != null)
                card_text.color = new Color(card_text.color.r, card_text.color.g, card_text.color.b, opacity);
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (onClick != null)
                    onClick.Invoke(this);
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (onClickRight != null)
                    onClickRight.Invoke(this);
            }
        }

        public CardData GetCard()
        {
            return card;
        }

        public VariantData GetVariant()
        {
            return variant;
        }
    }
}
