using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Models
{
    public class MidiModel
    {
        public readonly string[] InstrumentNames = Enum.GetNames(typeof(Instruments));

        //  private const int Chanel = 1;
        public int[] PlayModeMidi = { 0, 1 };

        public int playModeInstruments { get; set; }
        public MidiOut midiOut0 = new MidiOut(0);
        public MidiOut midiOut1 = new MidiOut(1);
        public MidiOut[] midiOutSelected;
        public MidiOut[] midiOutPlay;
        public int[,] midinoteNeck = new int[28, 6];

        public int SelectedModeMidi { get; set; }

        public MidiModel()
        {
            midiOutSelected = new MidiOut[2] { midiOut0, midiOut1 };
            midiOutPlay = new MidiOut[6] { midiOut0, midiOut0, midiOut0, midiOut0, midiOut0, midiOut0 };
            RecordingMidiNeck();
        }

        private void RecordingMidiNeck()
        {
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 28; i++)
                {
                    midinoteNeck[i, j] = midinote0[j] + i + 1;
                }
            }
        }

        public int[] midinote0 = new int[6] { 40, 45, 50, 55, 59, 64 };
        public int[] midinote1 = new int[6] { 0, 0, 0, 0, 0, 0 };

        //public int[] flag = new int[6] { 0, 0, 0, 0, 0, 0 };
        //public int[] flagnote = new int[6];
        //public int[] flagnote2 = new int[6];
        //public int[] lad = new int[6];
        //public int nf = 0;
        //public int flagmode;
        //public int flagbotn = 0;
        //
        // midi.Send(MidiMessage.StartNote(flagnote[k], 127, 1).RawData);
        //midi.Send(MidiMessage.StopNote(flagnote[i], 127, 1).RawData);
        public enum Instruments
        {
            AcousticPiano,
            BriteAcouPiano,
            ElectricGrandPiano,
            HonkyTonkPiano,
            ElecPiano1,
            ElecPiano2,
            Harsichord,
            Clavichord,
            Celesta,
            Glockenspiel,
            MusicBox,
            Vibraphone,
            Marimba,
            Xylophone,
            TubularBells,
            Dulcimer,
            DrawbarOrgan,
            PercOrgan,
            RockOrgan,
            ChurchOrgan,
            ReedOrgan,
            Accordian,
            Harmonica,
            TangoAccordian,
            AcousticGuitar,
            SteelAcousGuitar,
            ElJazzGuitar,
            ElectricGuitar,
            ElMutedGuitar,
            OverdrivenGuitar,
            DistortionGuitar,
            GuitarHarmonic,
            AcousticBass,
            ElBassFinger,
            ElBassPick,
            FretlessBass,
            SlapBass1,
            SlapBass2,
            SynthBass1,
            SynthBass2,
            Violin,
            Viola,
            Cello,
            ContraBass,
            TremeloStrings,
            PizzStrings,
            OrchStrings,
            Timpani,
            StringEns1,
            StringEns2,
            SynthStrings1,
            SynthStrings2,
            ChoirAahs,
            VoiceOohs,
            SynthVoice,
            OrchestraHit,
            Trumpet,
            Trombone,
            Tuba,
            MutedTrumpet,
            FrenchHorn,
            BrassSection,
            SynthBrass1,
            SynthBrass2,
            SopranoSax,
            AltoSax,
            TenorSax,
            BaritoneSax,
            Oboe,
            EnglishHorn,
            Bassoon,
            Clarinet,
            Piccolo,
            Flute,
            Recorder,
            PanFlute,
            BlownBottle,
            Shakuhachi,
            Whistle,
            Ocarina,
            Lead1Square,
            Lead2Sawtooth,
            Lead3Calliope,
            Lead4Chiff,
            Lead5Charang,
            Lead6Voice,
            Lead7Fifths,
            Lead8BassLd,
            Pad1NewAge,
            Pad2Warm,
            Pad3Polysynth,
            Pad4Choir,
            Pad5Bowed,
            Pad6Metallic,
            Pad7Halo,
            Pad8Sweep,
            FX1Rain,
            FX2Soundtrack,
            FX3Crystal,
            FX4Atmosphere,
            FX5Brightness,
            FX6Goblins,
            FX7Echoes,
            FX8SciFi,
            Sitar,
            Banjo,
            Shamisen,
            Koto,
            Kalimba,
            Bagpipe,
            Fiddle,
            Shanai,
            TinkerBell,
            Agogo,
            SteelDrums,
            Woodblock,
            TaikoDrum,
            MelodicTom,
            SynthDrum,
            ReverseCymbal,
            GuitarFretNoise,
            BreathNoise,
            Seashore,
            BirdTweet,
            Telephone,
            Helicopter,
            Applause,
            Gunshot,
            a111,
            a112,
            a113,
            a114,
            a115,
            a116,
            a117,
            a118,
            a119,
        }
    }
}