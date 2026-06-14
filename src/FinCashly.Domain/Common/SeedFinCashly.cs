namespace FinCashly.API.seed
{
    public class SeedFinCashly
    {
        public string dbDestination { get; set; } = string.Empty;

        public string? seedId { get; set; }

        // Account
        public string? name { get; set; }
        public string? type { get; set; }
        public decimal? balance { get; set; }

        // Category
        public bool? isDefault { get; set; }

        // Goal
        public string? title { get; set; }
        public decimal? targetAmount { get; set; }
        public decimal? currentAmount { get; set; }
        public DateTime? deadline { get; set; }
        public bool? isCompleted { get; set; }

        // Transaction
        public string? accountSeedId { get; set; }
        public string? categorySeedId { get; set; }
        public decimal? amount { get; set; }
        public string? description { get; set; }
        public DateTime? date { get; set; }
    }
}

