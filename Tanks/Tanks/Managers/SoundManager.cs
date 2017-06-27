using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Tanks._Game;

namespace Tanks.Managers
{
    public class SoundManager
    {
        Dictionary<string, SoundEffectInstance> sounds;
        ContentManager content;

        public SoundManager(ContentManager content)
        {
            sounds = new Dictionary<string, SoundEffectInstance>();
            this.content = content;
        }

        public void AddSound(string path)
        {
            var s = content.Load<SoundEffect>(path);
            sounds.Add(path, s.CreateInstance());
        }

        public void Play(string key)
        {
            if (GameSettings.Sounds)
            {
                sounds[key].Play();
            }
        }

        public void StopAll()
        {
            if (GameSettings.Sounds)
                foreach (var s in sounds)
                    if (s.Value.State == SoundState.Playing)
                        s.Value.Stop();
        }
    }
}
