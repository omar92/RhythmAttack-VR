using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MidiPlayerTK
{

    public class MidiNote
    {
        /// <summary>
        /// Midi info : time
        /// </summary>
        public long AbsoluteQuantize;
        
        /// <summary>
        /// Midi info : midi note
        /// </summary>
        public int Midi;

        /// <summary>
        /// Midi info : velocity
        /// </summary>
        public int Velocity;

        /// <summary>
        /// Duration of the note in milliseconde
        /// </summary>
        public double Duration;

        /// <summary>
        /// Delay before playing the note in milliseconde
        /// </summary>
        public float Delay;

        /// <summary>
        /// Midi chanel from 1 to 16
        /// </summary>
        public float Chanel;

        /// <summary>
        /// Calculate in Audio component
        /// </summary>
        public float Pitch;

        /// <summary>
        /// Patch (preset) to play with this note
        /// </summary>
        public int Patch;

        /// <summary>
        /// True if not is a drum hit (chanel 10 or 16)
        /// </summary>
        public bool Drum;

    }
}
