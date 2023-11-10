﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameEngine.Input;
using GameEngine.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.State
{
    internal abstract class BaseGameState
    {
        private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();
        private ContentManager _contentManager;
        protected int _viewportHeight;
        protected int _viewportWidth;

        protected InputManager InputManager { get; set; }

        public void Initialize(ContentManager contentManager, int viewportHeight, int viewportWidth)
        {
            _contentManager = contentManager;
            _viewportHeight = viewportHeight;
            _viewportWidth = viewportWidth;

            SetInputManager();
        }

        public abstract void LoadContent();

        protected abstract void SetInputManager();

        protected Texture2D LoadTexture(string textureName)
        {
            Texture2D texture = _contentManager.Load<Texture2D>($@"Graphics\{textureName}");

            return texture ?? _contentManager.Load<Texture2D>(FallbackTexture);
        }

        public void UnloadContent()
        {
            _contentManager.Unload();
        }

        public abstract void HandleInput(GameTime gameTime);

        public virtual void Update(GameTime gameTime) { }

        // event handlers
        public event EventHandler<BaseGameState> OnStateSwitched;
        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        public event EventHandler<Events> OnEventNotification;
        protected void NotifyEvent(Events eventType, object argument = null)
        {
            OnEventNotification?.Invoke(this, eventType);

            foreach (BaseGameObject gameObject in _gameObjects)
            {
                gameObject.OnNotify(eventType);
            }
        }

        protected void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        protected void RemoveGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects
                .OrderBy(gameObject => gameObject.ZIndex))
            {
                gameObject.Render(spriteBatch);
            }
        }

        private const string FallbackTexture = @"Graphics\Empty";
    }
}
