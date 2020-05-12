namespace MittelalterKi.Data.StateMachine.Bedürfnise
{
    public class Bedürfnis : IBedürfnis
    {
        private readonly decimal gewicht;

        public Bedürfnis(string name, decimal staertWert = 50, decimal gewicht = 1)
        {
            Name = name;
            Wert = staertWert;
            this.gewicht = gewicht;
        }
        public decimal Wert { get; set; }
        public decimal SollMin { get; set; } = 10;
        public decimal SollMax { get; set; } = 100;

        public decimal? Min { get; set; } = null;
        public decimal? Max { get; set; } = null;

        public decimal Stärke
        {
            get
            {
                //Über Max => kein Bedarf
                if (Max!=null && Wert > Max) return 0;
                if (Wert > SollMax) return 0;

                var spanne = SollMax - SollMin;
                var sollDiv = (SollMax - Wert) / spanne;
                //Über SollMin => Moderater Bedarf
                if (Wert > SollMin) return sollDiv * gewicht;

                sollDiv *= 10;

                if (Min != null)
                {
                    if (Wert < Min)
                    {
                        sollDiv *= 100;
                    }
                }
                return sollDiv * gewicht;
            }
        }

        public string Name { get; }
    }

    public class GrundBedürfnis:Bedürfnis, IGrundBedürfnis {
        public GrundBedürfnis(string name, decimal staertWert = 50, decimal gewicht = 1) : base(name, staertWert, gewicht) { }
    }
}
