using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<KundeDb>();
var app = builder.Build();

const string baseApi = "api/kunde";
var kunden = app.MapGroup(baseApi);

kunden.MapGet("/", GetAllKunden);
kunden.MapPost("/", CreateKunde);
kunden.MapPut("/{id}", UpdateKunde);
kunden.MapDelete("/{id}", DeleteKunde);

static async Task<IResult> GetAllKunden(KundeDb db)
{
    var kunden = await db.kunden.Include(k => k.adresse).ToArrayAsync();
    return TypedResults.Ok(kunden);
}

static async Task<IResult> CreateKunde(Kunde kunde, KundeDb db)
{
    db.Add(kunde);
    await db.SaveChangesAsync();
    
    return TypedResults.Created($"{baseApi}/{kunde.Id}", kunde);
}

static async Task<IResult> UpdateKunde(int id, Kunde inputKunde, KundeDb db)
{
    var kunde = await db.kunden.FindAsync(id);
    if (kunde is null) return TypedResults.NotFound();
    kunde.tel = inputKunde.tel;
    kunde.adresse = inputKunde.adresse;
    kunde.vorname = inputKunde.vorname;
    kunde.nachname = inputKunde.nachname;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteKunde(int id, KundeDb db)
{
    if (await db.kunden.FindAsync(id) is Kunde kunde)
    {
        db.kunden.Remove(kunde);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}

app.Run();
