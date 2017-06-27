using Ash.Core;
using Ash.FSM;
using Tanks.Resources;
using Tanks.Components;
using Microsoft.Xna.Framework;
using Tanks.Components.StateComponents;
using Tanks.Enums;
using Tanks.Tools.Extensions;
using System.Collections.Generic;
using Tanks.Tools;
using System;
using static Tanks.Enums.Animations;

namespace Tanks._Game
{
    partial class EntityFactory
    {
        public Entity CreateTrack(Entity owner, Animations animation)
        {
            var sprite = new Sprite(textureManager.Get(Images.Instance.GameTextures), cellSize, Layers.Tracer);
            var ownerPosition = owner.Get<Position>();
            int delay = 1000;

            var tracerList = new Dictionary<Enum, Clip>();
            tracerList.Add(TraceHorisontal, new Clip(sprite, new Vector2(0, 108), 4, delay, true));
            tracerList.Add(TraceVertical, new Clip(sprite, new Vector2(120, 108), 4, delay, true));
            tracerList.Add(TraceDownLeft, new Clip(sprite, new Vector2(240, 108), 3, delay, true));
            tracerList.Add(TraceDownRight, new Clip(sprite, new Vector2(330, 108), 3, delay, true));
            tracerList.Add(TraceUpRight, new Clip(sprite, new Vector2(420, 108), 3, delay, true));
            tracerList.Add(TraceUpLeft, new Clip(sprite, new Vector2(510, 108), 3, delay, true));

            return CreateEntity()
                .Add(new Track())
                .Add(new Position(ownerPosition.X, ownerPosition.Y))
                .Add(new DeathOnTime(4016))
                .Add(new Display(tracerList[animation]));
        }
    }
}
