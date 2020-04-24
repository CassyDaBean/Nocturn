using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Audio;
using Terraria.ModLoader.Exceptions;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using System;
using Microsoft.Xna.Framework.Audio;

using MP3Sharp;
using NVorbis;


namespace Nocturn.Sounds.Music
{
    public class AF : MusicStreaming
    {
        private VorbisReader reader;
        private float[] floatBuf;
        private int loopStart;
        private int loopEnd;

        public AF(string path) : base(path) { }

        protected override void PrepareStream()
        {
            reader = new VorbisReader(stream, true);
            sampleRate = reader.SampleRate;
            channels = (AudioChannels)reader.Channels;

            string[] comments = reader.Comments;
            for (int i = 0; i < comments.Length; i++)
            {
                if (comments[i].StartsWith("LOOPSTART"))
                    int.TryParse(comments[i].Split('=')[1], out loopStart);
                else if (comments[i].StartsWith("LOOPEND"))
                    int.TryParse(comments[i].Split('=')[1], out loopEnd);
            }
        }
        protected override void FillBuffer(byte[] buffer)
        {
            if (floatBuf == null)
                floatBuf = new float[buffer.Length / 2];

            int read = reader.ReadSamples(floatBuf, 0, floatBuf.Length);
            if (read < floatBuf.Length)
            {
                Reset();
            }
                if ((loopEnd > 0 && reader.DecodedPosition >= loopEnd) || read < floatBuf.Length)
                {
                    reader.DecodedPosition = loopStart;
                    reader.ReadSamples(floatBuf, read, floatBuf.Length - read);
                }

                Convert(floatBuf, buffer);
            }

        public override void Stop(AudioStopOptions options)
        {
            base.Stop(options);

            reader.Dispose();
            reader = null;
            floatBuf = null;
        }
        public override void Reset()
        {
            if (reader != null)
                reader.DecodedPosition = 0;
        }
        public static void Convert(float[] floatBuf, byte[] buffer)
        {
            for (int i = 0; i < floatBuf.Length; i++)
            {
                short val = (short)(floatBuf[i] * short.MaxValue);
                buffer[i * 2] = (byte)val;
                buffer[i * 2 + 1] = (byte)(val >> 8);
            }
        }
    }
    
}
