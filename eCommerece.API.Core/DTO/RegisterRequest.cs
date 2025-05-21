namespace eCommerece.API.DTO{
    public record RegisterRequest(string? Email, string? Password, string? PersonName, GenderOptions? Gender);
}