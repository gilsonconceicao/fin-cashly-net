namespace FinCashly.Domain.Settings; 
#nullable disable
public class FeatureFlagsSettings
{
    public bool EnableRunSeedFile { get; set; }
    public bool EnableRunMigrateDb { get; set; }
}