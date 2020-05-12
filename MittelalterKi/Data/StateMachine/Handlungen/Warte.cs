using Microsoft.Extensions.Logging;
using MittelalterKi.Data.StateMachine.Umgebung;
using System.Linq;
using System.Threading.Tasks;

namespace MittelalterKi.Data.StateMachine.Handlungen
{
    public class Warte : IndividuumsBasisHandlung
    {
        private static readonly System.Random rnd = new System.Random();

        public Warte(Individuum individuum, ILogger logger) : base(individuum, logger) { }

#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt. Ist einen enfache implementierung ohne asyncrone Vorgänge
        public override async Task<IHandlung> BerechneNächste(decimal zeitEinheiten = 1)
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        {
            var schlecht = ReduziereGrundBedürfnise(zeitEinheiten);

            if (schlecht.Any(b => b.Name == "Wasser"))
            {
                return new Trinken(individuum, this, logger);
            }
            if (schlecht.Any(b => b.Name == "Narung"))
            {
                return new Essen(individuum, this, logger);
            }

            var energi = individuum.Bedürfnise.FirstOrDefault(b => b.Name == "Energi");
            var narung = individuum.Bedürfnise.FirstOrDefault(b => b.Name == "Narung");
            if (energi != null && energi.Wert < energi.SollMax)
            {
                energi.Wert += rnd.Next(0, (int)(narung.Wert/10));
                if (energi.Wert > energi.SollMax)
                {
                    energi.SollMax = energi.Wert;
                }
            }

            var narungsLager = individuum.Bedürfnise.FirstOrDefault(b => b.Name == "NarungsLager");
            if (narungsLager != null)
            {
                if (rnd.Next(0, (int)(narungsLager.Wert / 10)) < 2)
                {
                    return new BestelleFeld(individuum, this, logger);
                }
            }

            return this;
        }
    }

    public class Bewegen: IndividuumsBasisSubHandlung
    {
        private readonly IHandlung beabsichtigt;
        private readonly IUmgebung ziel;

        public Bewegen(Individuum individuum, IHandlung vorherigeAktion, IHandlung beabsichtigt, IUmgebung ziel, ILogger logger) : base(individuum, vorherigeAktion, logger) {
            this.beabsichtigt = beabsichtigt;
            this.ziel = ziel;
        }

        public override Task<IHandlung> BerechneNächste(decimal zeitEinheiten = 1)
        {
            throw new System.NotImplementedException();
        }
    }
}
