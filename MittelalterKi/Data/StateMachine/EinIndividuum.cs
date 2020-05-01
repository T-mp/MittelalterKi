using Microsoft.Extensions.Logging;
using MittelalterKi.Data.StateMachine.Bedürfnise;
using MittelalterKi.Data.StateMachine.Fähigkeiten;
using MittelalterKi.Data.StateMachine.Handlungen;

namespace MittelalterKi.Data.StateMachine
{
    public class EinIndividuum: Individuum
    {
        private static readonly System.Random rnd = new System.Random();

        protected readonly IFähigkeitenQuelle fähigkeitenQuelle;

        public EinIndividuum(ILogger<EinIndividuum> logger, IFähigkeitenQuelle fähigkeitenQuelle = null) : base(logger)
        {
            Name = $"{nameof(EinIndividuum)} Nr.: {Id}";
            this.fähigkeitenQuelle = fähigkeitenQuelle;
            bedürfnise.Add(new Bedürfnis("Narung", rnd.Next(10, 100)) { Min = 10, SollMin = 20, SollMax = 100 });
            bedürfnise.Add(new Bedürfnis("Wasser", rnd.Next(20, 80)){ Min = 10, SollMin = 20, SollMax = 100 });
            bedürfnise.Add(new Bedürfnis("NarungsLager", rnd.Next(0, 500)) { SollMin = 50, SollMax = 1000 });
            bedürfnise.Add(new Bedürfnis("Energi", rnd.Next(0, 100)) { SollMin = 0, SollMax = 100 });

            state = new Warte(this, logger);
        }
    }
}
