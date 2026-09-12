namespace MoneyFlow.Model
{
    public class UserSettings
    {
        public int SettingId { get; set; }

        public int UserId { get; set; }

        public string? FontName { get; set; }

        public int FontSize { get; set; }

        public string? TextColor { get; set; }

        public string? BackgroundColor { get; set; }
    }
}