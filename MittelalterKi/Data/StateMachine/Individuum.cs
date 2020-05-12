using Microsoft.Extensions.Logging;
using MittelalterKi.Data.StateMachine.Bedürfnise;
using MittelalterKi.Data.StateMachine.Fähigkeiten;
using MittelalterKi.Data.StateMachine.Handlungen;
using MittelalterKi.Data.StateMachine.Umgebung;
using MittelalterKi.Data.StateMachine.Zustände;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MittelalterKi.Data.StateMachine
{
    public abstract class Individuum
    {
        #region Identität
        private static int sID = 1;
        public int Id { get; } = sID++;

        // für KI ertmal nicht notwendig ;--)
        public string Bild = "";
        #endregion

        public decimal Y = 10;
        public decimal X = 10;
        public decimal Z = 0;

        public string BazeichnungAktuelleHandlung { get => atuelleHandlung.GetType().Name; }

        public IList<IZustand> Zustände { get; } = new List<IZustand>();
        public IList<IFähigkeit> Fähigkeiten { get; } = new List<IFähigkeit>();
        public IList<IBedürfnis> Bedürfnise { get; } = new List<IBedürfnis>();
        public IList<IHandlung> MöglicheHandlungen { get; } = new List<IHandlung>();

        public IReadOnlyList<IUmgebung> Umgebung { get; set; }

        protected IHandlung atuelleHandlung;
        protected readonly ILogger<Individuum> logger;

        public Individuum(ILogger<Individuum> logger)
        {
            this.logger = logger;
        }

        public async Task BerechneNächsteHandlung(decimal zeitEinheiten = 1)
        {
            if (Zustände.Any(z => z is Tod))
            {
                logger.LogTrace($"[{Id}] ist Tod und kann daher nichts mehr machen");
                return;
            }
            logger.LogTrace($"[{Id}].BerechneNächstenZustand({zeitEinheiten})");
            atuelleHandlung = await atuelleHandlung.BerechneNächste(zeitEinheiten);
        }

        public override string ToString()
        {
            return $"[{Id}]";
        }
    }
}
