using System;
using System.Collections.Generic;
using System.Linq;

using Engine.Input;
using Engine.Objects;
using Engine.Sound;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.State
{
    public abstract class BaseGameState
    {
        private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();
        private ContentManager _contentManager;
        protected SoundManager _soundManager = new SoundManager();
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

        protected SoundEffect LoadMusic(string soundName)
        {
            return _contentManager.Load<SoundEffect>($@"Music\{soundName}");
        }

        protected SoundEffect LoadSound(string soundName)
        {
            return _contentManager.Load<SoundEffect>($@"Sounds\{soundName}");
        }

        public void UnloadContent()
        {
            _contentManager.Unload();
        }

        public abstract void HandleInput(GameTime gameTime);

        public abstract void UpdateGameState(GameTime gameTime);

        public void Update(GameTime gameTime)
        {
            UpdateGameState(gameTime);
            _soundManager.PlaySoundtrack();
        }

        // event handlers
        public event EventHandler<BaseGameState> OnStateSwitched;
        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        public event EventHandler<BaseGameStateEvent> OnEventNotification;
        protected void NotifyEvent(BaseGameStateEvent eventType, object argument = null)
        {
            OnEventNotification?.Invoke(this, eventType);

            foreach (BaseGameObject gameObject in _gameObjects)
            {
                gameObject.OnNotify(eventType);
            }

            _soundManager.OnNotify(eventType);
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
