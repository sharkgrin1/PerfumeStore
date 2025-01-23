using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PerfumeWeb.Areas.Identity.Data;

public class PerfumeWebContext(DbContextOptions<PerfumeWebContext> options) : IdentityDbContext<User>(options);
