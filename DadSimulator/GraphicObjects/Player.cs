using DadSimulator.Animation;
using DadSimulator.Collider;
using DadSimulator.Interactable;
using DadSimulator.Misc;
using DadSimulator.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DadSimulator.GraphicObjects
{
    public class Player : IGraphicObject, IPosition
    {
        public Vector2 Position { get => m_movement.Position; }
        private const float m_interactionRadius = 20f;
        
        private readonly ISpritesheet m_spritesheet;

        private readonly IInteractableCollection m_interactableCollection;
        private readonly IUiEngine m_ui;
        private readonly IUserCommand m_userCommand;

        private readonly PlayerMovement m_movement;
        private readonly char m_actionKey;

        private readonly TimeWarp m_timeWarp;

        private string m_animation;

        internal struct UiRectInteractable
        {
            public Vector2 PositionInteractable;
            public RelativePosition RelativePosistion;
            public Color RectColor;
            public Color BorderColor;
            public string TextHeadline;
            public string TextBox;
        }

        private readonly List<UiRectInteractable> m_uiRectsToDraw;


        public Player(ISpritesheet spritesheet, ICollider collider, Vector2 startPosition, 
            IUserCommand movement, ICollisionChecker collisionChecker, 
            IInteractableCollection interactableCollection, IUiEngine gui, TimeWarp timeWarp)
        {
            m_spritesheet = spritesheet;
            ConfigureSpritesheet();
            m_userCommand = movement;
            m_movement = new PlayerMovement(startPosition, movement, collider, collisionChecker);
            m_interactableCollection = interactableCollection;
            m_ui = gui;
            m_uiRectsToDraw = new List<UiRectInteractable>();
            m_actionKey = movement.GetActionKey();
            m_timeWarp = timeWarp;
        }

        private void ConfigureSpritesheet()
        {
            m_spritesheet.Color = Color.White;
            m_spritesheet.SetAnimation("idle-down");
        }

        public void Initialize()
        {
        }

        public void Update(double elapsedTime)
        {
            if (!m_timeWarp.WarpInProgress)
            {
                m_spritesheet.Update(elapsedTime);
                m_uiRectsToDraw.Clear();

                var movement = m_movement.MovePlayer(elapsedTime);
                SetAnimation(movement);
                InteractWithObjects();
            }
        }

        private void SetAnimation(Vector2 movement)
        {
            if (Vector2.Zero == movement)
            {
                SetIdleAnimation();
            }
            else if (Math.Abs(movement.X) > Math.Abs(movement.Y))
            {
                m_animation = movement.X > 0 ? "walk-right" : "walk-left";
            }
            else
            {
                m_animation = movement.Y > 0 ? "walk-down" : "walk-up";
            }
            m_spritesheet.SetAnimation(m_animation);
            m_spritesheet.FPS = m_animation.Contains("walk") ? 5 : 2;
        }

        private void SetIdleAnimation()
        {
            if (m_animation == null)
            {
                m_animation = "idle-down";
            }
            else if (m_animation.Contains("right"))
            {
                m_animation = "idle-right";
            }
            else if (m_animation.Contains("down"))
            {
                m_animation = "idle-down";
            }
            else if (m_animation.Contains("left"))
            {
                m_animation = "idle-left";
            }
            else if (m_animation.Contains("up"))
            {
                m_animation = "idle-up";
            }
        }

        private void InteractWithObjects()
        {
            if (m_interactableCollection != null)
            {
                var allInteractable = m_interactableCollection.GetInteractables();
                foreach (var interactable in allInteractable)
                {
                    var interactablePosition = interactable.GetPosition();
                    if (m_interactionRadius >= Vector2.Distance(Position, interactablePosition))
                    {
                        HandleInteraction(interactable, interactablePosition);
                    }
                }
            }
        }

        private void HandleInteraction(IInteractable interactable, Vector2 interactablePosition)
        {
            var text = AssembleInteractableText(interactable);

            var relativePos = Position.Y > interactablePosition.Y ? RelativePosition.Top : RelativePosition.Bottom;

            m_uiRectsToDraw.Add(new UiRectInteractable()
            {
                PositionInteractable = interactablePosition,
                RelativePosistion = relativePos,
                TextHeadline = interactable.GetName(),
                TextBox = text,
                RectColor = new Color(0, 0, 0, 200),
                BorderColor = Color.White
            });

            if (m_userCommand.IsActionKeyPressed())
            {
                interactable.ExecuteCommand();
            }
        }

        private string AssembleInteractableText(IInteractable interactable)
        {
            var text = $"State: {interactable.GetState()}";
            var command = interactable.GetCommand();

            if (!string.IsNullOrEmpty(command))
            {
                text += $"\n[{m_actionKey}] to {command}";
            }

            return text;
        }
        

        public void Draw(SpriteBatch batch)
        {
            DrawPlayer(batch);
            DrawInteractionUI();
        }

        private void DrawInteractionUI()
        {
            foreach (var boxParams in m_uiRectsToDraw)
            {
                m_ui.DrawRectangleInteractable(
                    boxParams.PositionInteractable,
                    boxParams.RelativePosistion,
                    boxParams.TextHeadline,
                    boxParams.TextBox,
                    boxParams.RectColor,
                    boxParams.BorderColor
                    );
            }
        }

        private void DrawPlayer(SpriteBatch batch)
        {
            m_spritesheet.Position = Position;
            m_spritesheet.Draw(batch);
        }

        public Vector2 GetPosition()
        {
            return Position;
        }
    }
}
