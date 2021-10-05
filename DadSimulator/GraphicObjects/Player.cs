using DadSimulator.Collider;
using DadSimulator.Interactable;
using DadSimulator.Misc;
using DadSimulator.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DadSimulator.GraphicObjects
{
    public class Player : IGraphicObject, IPosition
    {
        public Vector2 Position { get => m_movement.Position; }
        private const float m_interactionRadius = 20f;
        
        private readonly Texture2D m_texture;

        private readonly IInteractableCollection m_interactableCollection;
        private readonly IUiEngine m_ui;
        private readonly IUserCommand m_userCommand;

        private readonly PlayerMovement m_movement;

        private readonly char m_actionKey;

        internal struct UiRectInteractable
        {
            public Vector2 PositionInteractable;
            public RelativePosition RelativePosistion;
            public Color RectColor;
            public string TextHeadline;
            public string TextBox;
        }

        private readonly List<UiRectInteractable> m_uiRectsToDraw;


        public Player(Texture2D texture2D, Vector2 startPosition, 
            IUserCommand movement, ICollisionChecker collisionChecker, 
            IInteractableCollection interactableCollection, IUiEngine gui)
        {
            m_texture = texture2D;
            m_userCommand = movement;
            m_movement = new PlayerMovement(startPosition, movement, new RectangleCollider(texture2D), collisionChecker);
            m_interactableCollection = interactableCollection;
            m_ui = gui;
            m_uiRectsToDraw = new List<UiRectInteractable>();
            m_actionKey = movement.GetActionKey();
        }

        public void Initialize()
        {
        }

        public void Update(double elapsedTime)
        {
            m_uiRectsToDraw.Clear();

            m_movement.MovePlayer(elapsedTime);
            InteractWithObjects();
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
                RectColor = Color.Black,
                TextHeadline = interactable.GetName(),
                TextBox = text
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
                    boxParams.RectColor,
                    boxParams.TextHeadline,
                    boxParams.TextBox
                    );
            }
        }

        private void DrawPlayer(SpriteBatch batch)
        {
            batch.Draw(m_texture, Position, null, Color.Red);
        }

        public Vector2 GetPosition()
        {
            return Position;
        }
    }
}
