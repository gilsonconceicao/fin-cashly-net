namespace FinCashly.API.seed
{
    public class SeedFinCashly
    {
        public string DbDestination { get; set; } = string.Empty;

        public string? SeedId { get; set; }

        // Account
        public string? Name { get; set; }
        public string? Type { get; set; }
        public decimal? Balance { get; set; }

        // Category
        public bool? IsDefault { get; set; }

        // Goal
        public string? Title { get; set; }
        public decimal? TargetAmount { get; set; }
        public decimal? CurrentAmount { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? IsCompleted { get; set; }

        // Transaction
        public string? AccountSeedId { get; set; }
        public string? CategorySeedId { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
    }
}

