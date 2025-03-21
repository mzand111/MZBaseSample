using MZBase.Domain;

namespace MZBaseSample.Domain
{
    public class Country : Model<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
