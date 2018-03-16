using System;

namespace Tests
{
    public class Marker
    {
        private readonly char playingCharacter;

        public Marker(char playingCharacter)
        {
            this.playingCharacter = playingCharacter;
        }

        internal bool Is(char candidateChar)
        {
            return playingCharacter == candidateChar;
        }
        public override string ToString()
        {
            return playingCharacter.ToString();
        }
    }
}