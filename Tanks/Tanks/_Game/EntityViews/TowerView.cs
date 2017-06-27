using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tanks.Components.StateComponents;
using static Tanks.Enums.Animations;
using Tanks.Managers;
using Tanks.Enums;
using Tanks.Components;
using Tanks.Resources;
using Microsoft.Xna.Framework;
using Tanks.Tools;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks._Game.EntityViews
{
    class TowerView : EntityView
    {
        Direction direction;
        float delay;

        public TowerView(float delay, Direction direction)
        {
            this.delay = delay;
            this.direction = direction;
        }

        public override Display GetView()
        {
            var animManager = new AnimationManager();
            var frameSize = new Vector2(30, 36);
            var sprite = new Sprite(textureManager.Get(Images.Instance.GameTextures), frameSize, Layers.Tank);

            animManager
                .Add(StandUp, new Clip(sprite, new Vector2(240, 72)))
                .Add(StandDown, new Clip(sprite, new Vector2(0, 72)))
                .Add(StandLeft, new Clip(sprite, new Vector2(120, 72)))
                .Add(StandRight, new Clip(sprite, new Vector2(360, 72)))
                .Add(TurnLeftDown, new Clip(sprite, new Vector2(0, 72), 5, delay, true, true))
                .Add(TurnLeftUp, new Clip(sprite, new Vector2(120, 72), 5, delay, true))
                .Add(TurnRightDown, new Clip(sprite, new Vector2(360, 72), 5, delay, true))
                .Add(TurnRightUp, new Clip(sprite, new Vector2(240, 72), 5, delay, true, true))
                .Add(TurnUpLeft, new Clip(sprite, new Vector2(120, 72), 5, delay, true, true))
                .Add(TurnUpRight, new Clip(sprite, new Vector2(240, 72), 5, delay, true))
                .Add(TurnDownLeft, new Clip(sprite, new Vector2(0, 72), 5, delay, true))
                .Add(TurnDownRight, new Clip(sprite, new Vector2(360, 72), 5, delay, true, true))
                .Add(TurnUpDown, new Clip(sprite, new Vector2(240, 72), 9, delay, true))
                .Add(TurnDownUp, new Clip(sprite, new Vector2(0, 72), 9, delay, true))
                .Add(TurnLeftRight, new Clip(sprite, new Vector2(120, 72), 9, delay, true))
                .Add(TurnRightLeft, new Clip(sprite, new Vector2(360, 72), 9, delay, true))
                .Change(AntEnum.Parse<Animations>(((int)direction).ToString()));

            return new Display(animManager, 0, -3);
        }
    }
}
