﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameEngine.Objects;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.State
{
    internal abstract class BaseGameState
    {
        private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();

        public abstract void LoadContent(ContentManager contentManager);

        public abstract void UnloadContent(ContentManager contentManager);

        public abstract void HandleInput();

        public event EventHandler<BaseGameState> OnStateSwitched;

        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        protected void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects
                .OrderBy(gameObject => gameObject.zIndex))
            {
                gameObject.Render(spriteBatch);
            }
        }
    }
}
