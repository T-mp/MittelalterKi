using MittelalterKi.Data.StateMachine.Materialien;
using System.Collections.Generic;
using System.Linq;

namespace MittelalterKi.Data.StateMachine.Umgebung
{
    public class Bananenstaude : IUmgebung
    {
        public Bananenstaude() { }

        public Bananenstaude(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        public decimal Y { get; set; } = 10;
        public decimal X { get; set; } = 10;
        public decimal Z { get; set; } = 0;

        public IList<IMaterial> Bietet { get; set; } = new List<IMaterial> { new Banane() };

        public string Bild
        {
            get
            {
                return $"🌴";
            }
        }
    }
}
