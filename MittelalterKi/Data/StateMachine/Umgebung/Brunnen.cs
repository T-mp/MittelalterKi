using MittelalterKi.Data.StateMachine.Materialien;
using System.Collections.Generic;

namespace MittelalterKi.Data.StateMachine.Umgebung
{
    public class Brunnen : IUmgebung
    {
        public Brunnen(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }
        public decimal Y { get; set; }
        public decimal X { get; set; }
        public decimal Z { get; set; }
        public IList<IMaterial> Bietet { get; set; } = new List<IMaterial> { new Wasser() };

        public string Bild { 
            get
            {
                return $"🌊";
            }
        }
    }
}
