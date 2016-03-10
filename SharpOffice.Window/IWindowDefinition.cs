using SharpOffice.Core.Configuration;

namespace SharpOffice.Window
{
    public interface IWindowDefinition
    {
        Xwt.Window Window { get; }
        void Configure(IConfiguration configuration);
    }
}
