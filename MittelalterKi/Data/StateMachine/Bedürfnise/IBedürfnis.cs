using System.Threading.Tasks;

namespace MittelalterKi.Data.StateMachine.Bedürfnise
{
    public interface IBedürfnis
    {
        decimal Wert { get; set; }

        decimal SollMin { get; set; }
        decimal SollMax { get; set; }
        decimal? Min { get; set; }
        decimal? Max { get; set; }

        public decimal Stärke { get; }

        string Name { get; }
    }
    public interface IGrundBedürfnis: IBedürfnis { }
}
