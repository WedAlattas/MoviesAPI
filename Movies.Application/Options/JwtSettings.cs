namespace Movies.Application.Options
{
    public class JwtSettings
    {
        public string? Secret { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
