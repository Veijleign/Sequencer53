using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BInrerface
{
    public abstract partial class Note
        {
            //public virtual float RootFrequency { get; }
            public virtual int Number { get; set; }
            public virtual float Frequency { get; set; }
            public static bool operator ==(Note n1, Note n2) => n1?.Number == n2?.Number;
            public static bool operator !=(Note n1, Note n2) => n1.Number == n2.Number;
            public override bool Equals(object obj)
            {
                if (obj == null) { return false; }
                Note n2 = obj as Note;
                if (n2 == null) { return false; }
                return (Number == n2.Number);

            }
            public override int GetHashCode()
            {
                return Number;
            }

        }
    public interface IXyidiEvent
    {
        


        public B.NoteLogTemper53 Note { get; set; }
        public int Velocity { get; set; }
        public float StartTime { get; set; }
        public float Duration { get; set; }

    }
    public interface IWorkWithXyidi
    {
        public void AddXyidiEvent(float startTime, float playTime, int noteNumber);
        public void RemoveXyidiEvent(float startTime, float playTime, int noteNumber);
        //public 
    }
    public interface IGUI
    {
        public void Start(List<IXyidiEvent> xyidiEvents);
        public void PrintError(string ErrorMessage);
    }
    public interface IAudio
    {
        public void SoundIt(List<List<IXyidiEvent>> xyidiEvents);
    }
}
